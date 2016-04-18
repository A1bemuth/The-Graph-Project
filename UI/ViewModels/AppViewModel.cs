using GraphDataLayer;
using UI.Infrastructure;
using UI.Models;

namespace UI.ViewModels
{
    public class AppViewModel : ViewModel
    {
        private GraphInfo graphInfo;
        private int selectedVerticeIndex = -1;
        private bool isMenuOpened;
        private int[] visitedPath;

        private GraphInformationViewModel graphInformationViewModel;
        private VerticeInformationViewModel verticeInformationViewModel;

        public GraphInformationViewModel GraphInformationModel
        {
            get { return graphInformationViewModel; }
            private set
            {
                graphInformationViewModel = value;
                OnPropertyChanged();
            }
        }

        public VerticeInformationViewModel VerticeInformationModel
        {
            get { return verticeInformationViewModel; }
            private set
            {
                verticeInformationViewModel = value;
                OnPropertyChanged();
            }
        }

        public GraphInfo Graph
        {
            get { return graphInfo; }
            set
            {
                graphInfo = value;
                OnPropertyChanged();
                GraphInformationModel = new GraphInformationViewModel(graphInfo);
            }
        }

        public int SelectedVerticeIndex
        {
            get { return selectedVerticeIndex; }
            set
            {
                selectedVerticeIndex = value;
                OnPropertyChanged();
                SelectedVerticeIndexChange();
            }
        }

        private void SelectedVerticeIndexChange()
        {
            VerticeInformationModel = SelectedVerticeIndex != -1
                ? new VerticeInformationViewModel(SelectedVerticeIndex, graphInfo)
                : new VerticeInformationViewModel();
        }

        public bool IsMenuOpened
        {
            get { return isMenuOpened; }
            set
            {
                isMenuOpened = value;
                OnPropertyChanged();
            }
        }

        public int[] VisitedPath
        {
            get { return visitedPath; }
            set
            {
                visitedPath = value;
                OnPropertyChanged();
            }
        }

        public AppViewModel()
        {
            GraphInformationModel = new GraphInformationViewModel();
            VerticeInformationModel = new VerticeInformationViewModel();
        }

        public void BindEvent()
        {
            CommandEventBinder.LoadGraphCommand.OnExecute += LoadGraph;
            CommandEventBinder.CloseMenuCommand.OnExecute += CloseMenu;
            CommandEventBinder.ShowCyclesCommand.OnExecute += ShowCycles;
            CommandEventBinder.SelectCycleCommand.OnExecute += SelectCycle;
        }

        private void LoadGraph(object parameter)
        {
            CommandEventBinder.CloseMenuCommand.Execute(null);
            var graph = (NamedGraph) new AdjacencyGraph(12)
                .AddArrow(0, 6)
                .AddArrow(0, 7)
                .AddArrow(1, 0)
                .AddArrow(1, 3)
                .AddArrow(1, 4)
                .AddArrow(2, 3)
                .AddArrow(2, 11)
                .AddArrow(3, 11)
                .AddArrow(4, 11)
                .AddArrow(5, 11)
                .AddArrow(6, 5)
                .AddArrow(7, 1)
                .AddArrow(7, 2)
                .AddArrow(8, 11)
                .AddArrow(9, 10)
                .AddArrow(9, 11)
                .AddArrow(10, 11)
                .AddArrow(11, 0)
                .AddArrow(11, 1)
                .AddArrow(11, 2)
                .AddArrow(11, 3)
                .AddArrow(11, 4)
                .AddArrow(11, 5)
                .AddArrow(11, 6)
                .AddArrow(11, 7)
                .AddArrow(11, 8)
                .AddArrow(11, 9)
                .AddArrow(11, 10);
            Graph = new GraphInfo(graph);
        }

        private void CloseMenu(object parameter)
        {
            IsMenuOpened = false;
        }

        private void ShowCycles(object parameter)
        {
            var model = new CycleSelectionViewModel(graphInfo);
            CommandEventBinder.CloseMenuCommand.Execute(null);
            Navigator.OpenCycleModal(model);
        }

        private void SelectCycle(object index)
        {
            Navigator.CloseCycleModal();
            var selectedCycleIndex = (int) index;
            if(selectedCycleIndex == -1)
                return;
            VisitedPath = graphInfo.Cycles[selectedCycleIndex];
        }

        public override void Dispose()
        {
            CommandEventBinder.LoadGraphCommand.OnExecute -= LoadGraph;
            CommandEventBinder.CloseMenuCommand.OnExecute -= CloseMenu;
            CommandEventBinder.ShowCyclesCommand.OnExecute -= ShowCycles;
            CommandEventBinder.SelectCycleCommand.OnExecute -= SelectCycle;
        }
    }
}