using System;
using System.Collections.Generic;
using GraphDataLayer;

namespace GraphAlgorithms
{
    public static class GraphExtension
    {
        public static List<int[]> FindCycles(this IGraph graph)
        {
            if(graph == null)
                throw new ArgumentNullException(nameof(graph));

            var searcher = new CyclesSearcher();
            return searcher.FindCycles(graph);
        }

        public static double ClusteringCoefficient(this IGraph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            var coefCalculator = new ClusteringCoefficient(graph);
            return coefCalculator.GetClusteringCoefficientForGraph();
        }

        public static double ClusteringCoefficientFor(this IGraph graph, int verticeIndex)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));
            if(verticeIndex < 0)
                throw new ArgumentException("Индекс вершины отрицательный.");

            var coefCalculator = new ClusteringCoefficient(graph);
            return coefCalculator.GetClusteringCoefficientForVertex(verticeIndex);
        }

    }
}