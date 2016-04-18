using System.Collections.Generic;
using GraphAlgorithms;
using GraphDataLayer;

namespace UI.Models
{
    public class GraphInfo
    {
        public NamedGraph Graph { get; }

        public int VerticeCount { get; }
        public int ArrowCount { get; }
        public double ClusteringCoef { get; }
        public List<int[]> Cycles { get; }

        public GraphInfo(NamedGraph graph)
        {
            Graph = graph;
            VerticeCount = graph.VerticesCount;
            ArrowCount = graph.ArrowsCount;
            ClusteringCoef = graph.ClusteringCoefficient();
            Cycles = graph.FindCycles();
        }
    }
}