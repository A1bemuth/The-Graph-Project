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
        private ICollection<string> cycles;

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

        public ICollection<string> Cycles
        {
            get { return cycles; }
            private set
            {
                cycles = value;
                OnPropertyChanged(nameof(Cycles));
            }
        }

        public Command LoadGraphCommand { get; }

        public AppViewModel()
        {
            LoadGraphCommand = new Command(LoadGraph);
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

        private void AnalyzeGraph()
        {
            if(Graph == null)
                return;
            ClusteringCoefficient = graph.ClusteringCoefficient();
            Cycles = graph.FindCycles().Select(c => string.Join(",", c)).ToArray();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}