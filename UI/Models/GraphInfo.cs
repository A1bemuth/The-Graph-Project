using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GraphAlgorithms;
using GraphDataLayer;
using UI.Infrastructure;

namespace UI.Models
{
    public class GraphInfo : PropertyNotifier
    {
        private Task searchingTask;

        public NamedGraph Graph { get; }
        public int VerticeCount { get; set; }
        public int ArrowCount { get; set; }
        public double ClusteringCoef { get; set; }
        public double Density { get; set; }

        public bool IsCycleSearchingComplete
        {
            get { return Get<bool>(); }
            private set { Set(value);}
        }

        public List<int[]> Cycles
        {
            get { return Get<List<int[]>>(); }
            private set { Set(value);}
        }

        public int CyclesCount
        {
            get { return Get<int>(); }
            private set { Set(value);}
        }

        public GraphInfo()
        {
        }

        public GraphInfo(NamedGraph graph)
        {
            Graph = graph;
            Cycles = new List<int[]>();
        }

        public void StartSearching()
        {
            searchingTask = Task.Run(async () =>
            {
                IsCycleSearchingComplete = false;
                Cycles = await Graph.FindCyclesAsync(new Progress<int[]>(ints => CyclesCount++), CancellationToken.None);
                IsCycleSearchingComplete = true;
            });
        }
    }
}