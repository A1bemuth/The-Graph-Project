using System.ComponentModel;
using System.Runtime.CompilerServices;
using GraphDataLayer;
using UI.Annotations;

namespace UI.ViewModels
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private IGraph graph;

        public event PropertyChangedEventHandler PropertyChanged;

        public IGraph Graph
        {
            get { return graph; }
            set
            {
                graph = value;
                OnPropertyChanged(nameof(Graph));
            }
        }

        public Command LoadGraphCommand { get; }

        public AppViewModel()
        {
            LoadGraphCommand = new Command(LoadGraph);
        }

        private void LoadGraph(object parameter)
        {
            Graph = new AdjacencyGraph(13)
                .AddArrow(0, 1)
                .AddArrow(0, 5)
                .AddArrow(5, 4)
                .AddArrow(4, 2)
                .AddArrow(2, 0)
                .AddArrow(2, 3)
                .AddArrow(3, 2)
                .AddArrow(3, 5)
                .AddArrow(4, 3)
                .AddArrow(4, 11)
                .AddArrow(11, 12)
                .AddArrow(12, 9)
                .AddArrow(9, 10)
                .AddArrow(10, 12)
                .AddArrow(9, 11)
                .AddArrow(0, 6)
                .AddArrow(6, 9)
                .AddArrow(7, 6)
                .AddArrow(7, 8)
                .AddArrow(8, 7);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}