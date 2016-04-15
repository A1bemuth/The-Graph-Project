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
        private GraphInformationViewModel graphInformationViewModel = new GraphInformationViewModel();

        public event PropertyChangedEventHandler PropertyChanged;

        public IGraph Graph
        {
            get { return graph; }
            set
            {
                graph = value;
                OnPropertyChanged(nameof(Graph));
                graphInformationViewModel.AnalyzeGraph(value);
            }
        }

        public GraphInformationViewModel GraphInformationModel
        {
            get { return graphInformationViewModel; }
            set
            {
                graphInformationViewModel = value;
                OnPropertyChanged(nameof(GraphInformationModel));
            }
        }

        public Command LoadGraphCommand { get; }

        public AppViewModel()
        {
            LoadGraphCommand = new Command(LoadGraph);
        }

        private void LoadGraph(object parameter)
        {
            Graph = new AdjacencyListGraph(4)
                .AddArrow(0, 3)
                .AddArrow(0, 1)
                .AddArrow(1, 2)
                .AddArrow(2, 0);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}