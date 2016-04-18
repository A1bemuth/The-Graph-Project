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

        public VerticeInformationViewModel() { }

        public VerticeInformationViewModel(int selectedIndex, GraphInfo graphInfo)
        {
            Index = selectedIndex;
            ClusteringCoefficient = graphInfo.Graph.ClusteringCoefficientFor(Index);
            IncludedInCyclesCount = graphInfo.Cycles.Count(c => c.Contains(Index));
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


        public override void Dispose()
        {
        }
    }
}