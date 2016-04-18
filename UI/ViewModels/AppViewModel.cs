using System.ComponentModel;
using System.Runtime.CompilerServices;
using GraphDataLayer;
using UI.Annotations;
using UI.Infrastructure;

namespace UI.ViewModels
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private NamedGraph graph;
        private int selectedVerticeIndex = -1;

        public event PropertyChangedEventHandler PropertyChanged;
        public Command LoadGraphCommand { get; }
        public GraphInformationViewModel GraphInformationModel { get; }
        public VerticeInformationViewModel VerticeInformationModel { get; }

        public NamedGraph Graph
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
            Graph = (NamedGraph) new AdjacencyGraph(12)
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
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}