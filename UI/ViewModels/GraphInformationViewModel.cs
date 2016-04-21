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
        private double prestige;
        private double influence;
        private double indegreeStandartDeviation;
        private double outdegreeStandartDeviation;


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
            Prestige = graphInfo.Graph.GetGraphPrestige();
            Influence = graphInfo.Graph.GetGraphInfluence();
            IndegreeStandartDeviation = graphInfo.Graph.GetIndegreesStandartDeviation();
            OutdegreeStandartDeviation = graphInfo.Graph.GetOutdegreesStandartDeviation();

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

        public double Prestige
        {
            get { return prestige; }
            set
            {
                prestige = value;
                OnPropertyChanged();
            }
        }

        public double Influence
        {
            get { return influence; }
            set
            {
                influence = value;
                OnPropertyChanged();
            }
        }

        public double IndegreeStandartDeviation
        {
            get { return indegreeStandartDeviation; }
            set
            {
                indegreeStandartDeviation = value;
                OnPropertyChanged();
            }
        }

        public double OutdegreeStandartDeviation
        {
            get { return outdegreeStandartDeviation; }
            set
            {
                outdegreeStandartDeviation = value;
                OnPropertyChanged();
            }
        }
        public override void Dispose()
        {
        }
    }
}