using GraphAlgorithms;
using UI.Infrastructure;
using UI.Models;

namespace UI.ViewModels
{
    public class GraphInformationViewModel : PropertyNotifier
    {
        public GraphInformationViewModel()
        {
        }

        public GraphInformationViewModel(GraphInfo graphInfo)
        {
            if (graphInfo == null)
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
            get { return Get<int>(nameof(VerticeCount)); }
            set { Set(nameof(VerticeCount), value); }
        }

        public int ArrowCount
        {
            get { return Get<int>(nameof(ArrowCount)); }
            set { Set(nameof(ArrowCount), value); }
        }

        public double ClusteringCoefficient
        {
            get { return Get<double>(nameof(ClusteringCoefficient)); }
            set { Set(nameof(ClusteringCoefficient), value); }
        }

        public int CyclesCount
        {
            get { return Get<int>(nameof(CyclesCount)); }
            set { Set(nameof(CyclesCount), value); }
        }

        public double FirstReciprocity
        {
            get { return Get<double>(nameof(FirstReciprocity)); }
            set { Set(nameof(FirstReciprocity), value); }
        }

        public double SecondReciprocity
        {
            get { return Get<double>(nameof(SecondReciprocity)); }
            set { Set(nameof(SecondReciprocity), value); }
        }

        public double Prestige
        {
            get { return Get<double>(nameof(Prestige)); }
            set { Set(nameof(Prestige), value); }
        }

        public double Influence
        {
            get { return Get<double>(nameof(Influence)); }
            set { Set(nameof(Influence), value); }
        }

        public double IndegreeStandartDeviation
        {
            get { return Get<double>(nameof(IndegreeStandartDeviation)); }
            set { Set(nameof(IndegreeStandartDeviation), value); }
        }

        public double OutdegreeStandartDeviation
        {
            get { return Get<double>(nameof(OutdegreeStandartDeviation)); }
            set { Set(nameof(OutdegreeStandartDeviation), value); }
        }
    }
}