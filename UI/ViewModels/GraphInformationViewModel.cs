using System;
using System.Collections.Generic;
using System.Threading;
using GraphAlgorithms;
using GraphDataLayer;
using UI.Infrastructure;

namespace UI.ViewModels
{
    public class GraphInformationViewModel : PropertyNotifier
    {
        private readonly CancellationTokenSource tokenSource;

        public GraphInformationViewModel()
        {
        }

        public GraphInformationViewModel(Graph graph)
        {
            if (graph == null)
                return;
            tokenSource = new CancellationTokenSource();
            
            Graph = graph;
            VerticeCount = graph.VerticesCount;
            ArrowCount = graph.ArrowsCount;
            ClusteringCoefficient = graph.ClusteringCoefficient();
            FirstReciprocity = graph.CalcFirstReciprocity();
            SecondReciprocity = graph.CalcSecondReciprocity();
            Prestige = graph.GetGraphPrestige();
            Influence = graph.GetGraphInfluence();
            IndegreeStandartDeviation = graph.GetIndegreesStandartDeviation();
            OutdegreeStandartDeviation = graph.GetOutdegreesStandartDeviation();
            Density = graph.GetDensity();

            graph.FindCyclesAsync(new Progress<int[]>(ints => CyclesCount++), tokenSource.Token)
                .ContinueWith(task => Cycles = task.Result);
        }

        public void StopSearch()
        {
            tokenSource?.Cancel();
        }

        public Graph Graph
        {
            get { return Get<Graph>(); }
            private set { Set(value); }
        }

        public int VerticeCount
        {
            get { return Get<int>(); }
            set { Set(value); }
        }

        public int ArrowCount
        {
            get { return Get<int>(); }
            set { Set(value); }
        }

        public double ClusteringCoefficient
        {
            get { return Get<double>(); }
            set { Set(value); }
        }

        public int CyclesCount
        {
            get { return Get<int>(); }
            private set { Set(value); }
        }

        public List<int[]> Cycles
        {
            get { return Get<List<int[]>>(); }
            private set { Set(value); }
        }

        public double FirstReciprocity
        {
            get { return Get<double>(); }
            set { Set(value); }
        }

        public double SecondReciprocity
        {
            get { return Get<double>(); }
            set { Set(value); }
        }

        public double Prestige
        {
            get { return Get<double>(); }
            set { Set(value); }
        }

        public double Influence
        {
            get { return Get<double>(); }
            set { Set(value); }
        }

        public double IndegreeStandartDeviation
        {
            get { return Get<double>(); }
            set { Set(value); }
        }

        public double OutdegreeStandartDeviation
        {
            get { return Get<double>(); }
            set { Set(value); }
        }

        public double Density
        {
            get { return Get<double>(); }
            set { Set(value);}
        }
    }
}