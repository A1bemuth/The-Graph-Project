using System.Linq;
using GraphAlgorithms;
using UI.Infrastructure;
using UI.Models;

namespace UI.ViewModels
{
    public class VerticeInformationViewModel : PropertyNotifier
    {
        public VerticeInformationViewModel() { }

        public VerticeInformationViewModel(int selectedIndex, GraphInfo graphInfo)
        {
            Index = selectedIndex;
            ClusteringCoefficient = graphInfo.Graph.ClusteringCoefficientFor(Index);
            IncludedInCyclesCount = graphInfo.Cycles.Count(c => c.Contains(Index));
            VerticePerstige = graphInfo.Graph.GetPrestigeFor(Index);
            VerticeInfluence = graphInfo.Graph.GetInfluenceFor(Index);
        }

        public int Index
        {
            get { return Get<int>(); }
            set { Set(value); }
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