using System.Linq;
using GraphAlgorithms;
using UI.Infrastructure;
using UI.Models;

namespace UI.ViewModels
{
    public class VerticeInformationViewModel : ViewModel
    {
        private int index;
        private double clusteringCoef;
        private int includedInCyclesCount;
        private double verticePrestige;
        private double verticeInfluence;

        public VerticeInformationViewModel() { }

        public VerticeInformationViewModel(int selectedIndex, GraphInfo graphInfo)
        {
            Index = selectedIndex;
            ClusteringCoefficient = graphInfo.Graph.ClusteringCoefficientFor(Index);
            IncludedInCyclesCount = graphInfo.Cycles.Count(c => c.Contains(Index));
            VerticePerstige = graphInfo.Graph.GetPrestigeFor(index);
            VerticeInfluence = graphInfo.Graph.GetInfluenceFor(index);
        }

        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                OnPropertyChanged();
            }
        }

        public int IncludedInCyclesCount
        {
            get { return includedInCyclesCount; }
            set
            {
                includedInCyclesCount = value;
                OnPropertyChanged();
            }
        }

        public double ClusteringCoefficient
        {
            get { return clusteringCoef; }
            set
            {
                clusteringCoef = value;
                OnPropertyChanged();
            }
        }

        public double VerticePerstige
        {
            get { return verticePrestige; }
            set
            {
                verticePrestige = value;
                OnPropertyChanged();
            }
        }

        public double VerticeInfluence
        {
            get { return verticeInfluence; }
            set
            {
                verticeInfluence = value;
                OnPropertyChanged();
            }
        }


        public override void Dispose()
        {
        }
    }
}