using System.Collections.Generic;
using System.Linq;
using GraphAlgorithms;
using GraphDataLayer;
using UI.Infrastructure;

namespace UI.ViewModels
{
    public class VerticeInformationViewModel : PropertyNotifier
    {
        public VerticeInformationViewModel() { }

        public VerticeInformationViewModel(int selectedIndex, NamedGraph graph, IEnumerable<int[]> cycles)
        {
            Index = selectedIndex;
            VerticeName = graph[Index];
            ClusteringCoefficient = graph.ClusteringCoefficientFor(Index);
            VerticePerstige = graph.GetPrestigeFor(Index);
            VerticeInfluence = graph.GetInfluenceFor(Index);
            if (cycles != null)
                IncludedInCyclesCount = cycles.Count(c => c.Any(v => v.Equals(selectedIndex)));
        }

        public int Index
        {
            get { return Get<int>(); }
            set { Set(value); }
        }

        public string VerticeName
        {
            get { return Get<string>(); }
            private set { Set(value); }
        }

        public int IncludedInCyclesCount
        {
            get { return Get<int>(); }
            set { Set(value); }
        }

        public double ClusteringCoefficient
        {
            get { return Get<double>(); }
            set { Set(value); }
        }

        public double VerticePerstige
        {
            get { return Get<double>(); }
            set { Set(value); }
        }

        public double VerticeInfluence
        {
            get { return Get<double>(); }
            set { Set(value); }
        }
    }
}