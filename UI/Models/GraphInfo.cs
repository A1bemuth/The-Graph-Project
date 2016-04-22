using System.Collections.Generic;
using GraphDataLayer;

namespace UI.Models
{
    public class GraphInfo
    {
        public NamedGraph Graph { get; }
        public int VerticeCount { get; set; }
        public int ArrowCount { get; set; }
        public double ClusteringCoef { get; set; }
        public List<int[]> Cycles { get; set; }

        public GraphInfo()
        {
        }

        public GraphInfo(NamedGraph graph)
        {
            Graph = graph;
        }
    }
}