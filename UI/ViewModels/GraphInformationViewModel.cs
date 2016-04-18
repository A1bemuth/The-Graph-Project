using UI.Infrastructure;
using UI.Models;

namespace UI.ViewModels
{
    public class GraphInformationViewModel : ViewModel
    {
        private int verticeCount;
        private int arrowCount;
        private double clusteringCoef;
        private int cyclesCount;


        public GraphInformationViewModel() { }

        public GraphInformationViewModel(GraphInfo graphInfo)
        {
            if(graphInfo == null)
                return;
            VerticeCount = graphInfo.VerticeCount;
            ArrowCount = graphInfo.ArrowCount;
            ClusteringCoefficient = graphInfo.ClusteringCoef;
            CyclesCount = graphInfo.Cycles.Count;
        }
        public int VerticeCount
        {
            get { return verticeCount; }
            set
            {
                verticeCount = value;
                OnPropertyChanged();
            }
        }

        public int ArrowCount
        {
            get { return arrowCount; }
            set
            {
                arrowCount = value;
                OnPropertyChanged();
            }
        }

        public double ClusteringCoefficient
        {
            get { return clusteringCoef; }
            private set
            {
                clusteringCoef = value;
                OnPropertyChanged();
            }
        }

        public int CyclesCount
        {
            get { return cyclesCount; }
            set
            {
                cyclesCount = value;
                OnPropertyChanged();
            }

        }

        public override void Dispose()
        {
        }
    }
}