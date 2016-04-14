using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using GraphAlgorithms;
using GraphDataLayer;
using UI.Annotations;

namespace UI.ViewModels
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private IGraph graph;
        private double clusteringCoef;
        private List<int[]> cycles;
        private int selectedCycleIndex = -1;
        private IEnumerable<int> selectedCycle;

        public event PropertyChangedEventHandler PropertyChanged;

        public IGraph Graph
        {
            get { return graph; }
            set
            {
                graph = value;
                OnPropertyChanged(nameof(Graph));
                AnalyzeGraph();
            }
        }

        public double ClusteringCoefficient
        {
            get { return clusteringCoef; }
            private set
            {
                clusteringCoef = value;
                OnPropertyChanged(nameof(ClusteringCoefficient));
            }
        }

        public List<int[]> Cycles
        {
            get { return cycles; }
            set
            {
                cycles = value;
                OnPropertyChanged(nameof(Cycles));
            }

        }

        public int SelectedCycleIndex
        {
            get { return selectedCycleIndex; }
            set
            {
                selectedCycleIndex = value;
                OnPropertyChanged(nameof(SelectedCycleIndex));
                SelectedCycle = cycles[selectedCycleIndex];
            }
        }

        public IEnumerable<int> SelectedCycle
        {
            get { return selectedCycle; }
            set
            {
                selectedCycle = value;
                OnPropertyChanged(nameof(SelectedCycle));
            }
        }

        public Command LoadGraphCommand { get; }

        public AppViewModel()
        {
            LoadGraphCommand = new Command(LoadGraph);
        }

        private void LoadGraph(object parameter)
        {
            Graph = new AdjacencyGraph(12)
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

        private void AnalyzeGraph()
        {
            if(Graph == null)
                return;
            ClusteringCoefficient = graph.ClusteringCoefficient();
            Cycles = graph.FindCycles();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}