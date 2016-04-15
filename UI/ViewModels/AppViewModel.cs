using System.ComponentModel;
using System.Runtime.CompilerServices;
using GraphDataLayer;
using UI.Annotations;
using UI.Infrastructure;

namespace UI.ViewModels
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private IGraph graph;
        private int selectedVerticeIndex = -1;

        public event PropertyChangedEventHandler PropertyChanged;
        public Command LoadGraphCommand { get; }
        public GraphInformationViewModel GraphInformationModel { get; }
        public VerticeInformationViewModel VerticeInformationModel { get; }

        public IGraph Graph
        {
            get { return graph; }
            set
            {
                graph = value;
                OnPropertyChanged(nameof(Graph));
                GraphInformationModel.AnalyzeGraph(value);
            }
        }

        public int SelectedVerticeIndex
        {
            get { return selectedVerticeIndex; }
            set
            {
                selectedVerticeIndex = value;
                OnPropertyChanged(nameof(SelectedVerticeIndex));
                VerticeInformationModel.UpdateVerticeInformation(this);
            }
        }

        public AppViewModel()
        {
            LoadGraphCommand = new Command(LoadGraph);
            GraphInformationModel = new GraphInformationViewModel();
            VerticeInformationModel = new VerticeInformationViewModel();
        }

        private void LoadGraph(object parameter)
        {
            Graph = new AdjacencyListGraph(8)
                .AddArrow(0, 1)
                .AddArrow(1, 2)
                .AddArrow(2, 0)
                .AddArrow(3, 1)
                .AddArrow(3, 2)
                .AddArrow(5, 2)
                .AddArrow(4, 3)
                .AddArrow(3, 4)
                .AddArrow(4, 5)
                .AddArrow(6, 5)
                .AddArrow(5, 6)
                .AddArrow(7, 4);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}