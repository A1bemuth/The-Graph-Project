using GraphAlgorithms;
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
        private double firstReciprocity;
        private double secondReciprocity;


        public GraphInformationViewModel() { }

        public GraphInformationViewModel(GraphInfo graphInfo)
        {
            if(graphInfo == null)
                return;
            VerticeCount = graphInfo.VerticeCount;
            ArrowCount = graphInfo.ArrowCount;
            ClusteringCoefficient = graphInfo.ClusteringCoef;
            CyclesCount = graphInfo.Cycles.Count;
            FirstReciprocity = graphInfo.Graph.CalcFirstReciprocity();
            SecondReciprocity = graphInfo.Graph.CalcSecondReciprocity();
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

        public double FirstReciprocity
        {
            get { return firstReciprocity; }
            set
            {
                firstReciprocity = value;
                OnPropertyChanged();
            }
        }

        public double SecondReciprocity
        {
            get { return secondReciprocity; }
            set
            {
                secondReciprocity = value;
                OnPropertyChanged();
            }
        }

        public override void Dispose()
        {
        }
    }
}