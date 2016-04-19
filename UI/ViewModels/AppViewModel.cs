using System;
using System.Collections.Generic;
using GraphAlgorithms;
using GraphDataLayer;
using UI.Infrastructure;
using UI.Models;

namespace UI.ViewModels
{
    public class AppViewModel : ViewModel
    {
        private NamedGraph graph;
        private GraphInfo graphInfo;
        private int selectedVerticeIndex = -1;
        private bool isMenuOpened;
        private bool isGraphLoaded;
        private bool isVerticeSelected;
        private bool isModalOpened;
        private int[] visitedPath;
        private string status;

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

        public GraphInfo GraphInfo
        {
            get { return graphInfo; }
            set
            {
                graphInfo = value;
                OnPropertyChanged();
                IsGraphLoaded = graphInfo != null;
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
            IsVerticeSelected = SelectedVerticeIndex != -1;
            VerticeInformationModel = IsVerticeSelected
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

        public bool IsGraphLoaded
        {
            get { return isGraphLoaded; }
            set
            {
                isGraphLoaded = value;
                OnPropertyChanged();
            }
        }

        public bool IsVerticeSelected
        {
            get { return isVerticeSelected; }
            set
            {
                isVerticeSelected = value;
                OnPropertyChanged();
            }
        }

        public bool IsModalOpened
        {
            get { return isModalOpened; }
            set
            {
                isModalOpened = value;
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

        public string Status
        {
            get { return status; }
            set
            {
                status = value;
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
            CommandEventBinder.SelectCycleCommand.OnExecute += SelectCycle;
            CommandEventBinder.SelectPathCommand.OnExecute += SelectPath;
            CommandEventBinder.RefreshCommand.OnExecute += RefreshGraph;
            CommandEventBinder.CloseMenuCommand.OnExecute += CloseMenu;
            CommandEventBinder.ShowCyclesModalCommand.OnExecute += ShowCycles;
            CommandEventBinder.CloseCyclesModalCommand.OnExecute += CloseModal;
            CommandEventBinder.ShowPathModalCommand.OnExecute += ShowPath;
            CommandEventBinder.ClosePathModalCommand.OnExecute += CloseModal;
        }

        private void LoadGraph(object parameter)
        {
            CommandEventBinder.CloseMenuCommand.Execute();
            //var fileName = Navigator.OpenFile();
            //if(fileName == null)
            //    return;
            //graph = new ExcelImporter<AdjacencyGraph>().GetGraphs(fileName)[0];
            //GraphInfo = new GraphInfo(graph);
            graph = (NamedGraph) new AdjacencyGraph(12)
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
            GraphInfo = new GraphInfo(graph);
            Status = "Граф успешно загружен";
        }

        private void CloseMenu(object parameter)
        {
            IsMenuOpened = false;
        }

        private void ShowCycles(object parameter)
        {
            IsModalOpened = true;
            var model = new CycleSelectionViewModel(graphInfo);
            CommandEventBinder.CloseMenuCommand.Execute();
            Navigator.OpenCycleModal(model);
        }

        private void CloseModal(object o)
        {
            IsModalOpened = false;
        }

        private void SelectCycle(object index)
        {
            Navigator.CloseCycleModal();
            var selectedCycleIndex = (int) index;
            if (selectedCycleIndex == -1)
            {
                Status = "Цикл не был выбран.";
                return;
            }
            if (graphInfo != null)
            {
                var cycle = new List<int>(graphInfo.Cycles[selectedCycleIndex]);
                cycle.Add(cycle[0]);
                VisitedPath = cycle.ToArray();
                Status = "Цикл успешно отображен";
            }
        }

        private void ShowPath(object o)
        {
            IsModalOpened = true;
            var modal = new PathSelectionViewModel(GraphInfo);
            CommandEventBinder.CloseMenuCommand.Execute();
            Navigator.OpenPathModal(modal);
        }

        private void SelectPath(object o)
        {
            Navigator.ClosePathModal();
            var selectedIndexes = (Tuple<int, int>)o;
            if(selectedIndexes.Item1 == -1 || selectedIndexes.Item2 == -1)
            {
                Status = "Не выбрана начальная или конечная вершина";
                return;
            }
            if(selectedIndexes.Item1 == selectedIndexes.Item2)
            {
                Status = "Начальная и конечная вершины совпадают";
                return;
            }
            VisitedPath = graphInfo.Graph.FindPath(selectedIndexes.Item1, selectedIndexes.Item2);
            Status = VisitedPath != null ? "Путь успешно построен" : "Между вершинами нет пути";
        }

        private void RefreshGraph(object o)
        {
            CommandEventBinder.CloseMenuCommand.Execute();
            GraphInfo = null;
            GraphInfo = new GraphInfo(graph);
            Status = "Граф обновлен.";
        }

        public override void Dispose()
        {
            CommandEventBinder.LoadGraphCommand.OnExecute -= LoadGraph;
            CommandEventBinder.CloseMenuCommand.OnExecute -= CloseMenu;
            CommandEventBinder.ShowCyclesModalCommand.OnExecute -= ShowCycles;
            CommandEventBinder.SelectCycleCommand.OnExecute -= SelectCycle;
            CommandEventBinder.ShowPathModalCommand.OnExecute -= ShowPath;
            CommandEventBinder.SelectPathCommand.OnExecute -= SelectPath;
            CommandEventBinder.RefreshCommand.OnExecute -= RefreshGraph;
        }
    }
}