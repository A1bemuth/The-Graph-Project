using System;
using System.Threading;
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
            FirstReciprocity = graphInfo.Graph.CalcFirstReciprocity();
            SecondReciprocity = graphInfo.Graph.CalcSecondReciprocity();
            Prestige = graphInfo.Graph.GetGraphPrestige();
            Influence = graphInfo.Graph.GetGraphInfluence();
            IndegreeStandartDeviation = graphInfo.Graph.GetIndegreesStandartDeviation();
            OutdegreeStandartDeviation = graphInfo.Graph.GetOutdegreesStandartDeviation();
            Density = graphInfo.Density;
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
            set { Set(value); }
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