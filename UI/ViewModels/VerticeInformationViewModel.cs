using GraphAlgorithms;
using GraphDataLayer;
using UI.Infrastructure;

namespace UI.ViewModels
{
    public class VerticeInformationViewModel : PropertyNotifier
    {
        public VerticeInformationViewModel() { }

        public VerticeInformationViewModel(int selectedIndex, NamedGraph graph)
        {
            Index = selectedIndex;
            VerticeName = graph[Index];
            ClusteringCoefficient = graph.ClusteringCoefficientFor(Index);
            //IncludedInCyclesCount = graph.Cycles.Count(c => c.Contains(Index));
            VerticePerstige = graph.GetPrestigeFor(Index);
            VerticeInfluence = graph.GetInfluenceFor(Index);
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