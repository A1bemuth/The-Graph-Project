using System;
using System.Collections.Generic;
using GraphDataLayer;

namespace GraphAlgorithms
{
    public static class GraphExtension
    {
        public static List<int[]> FindCycles(this Graph graph)
        {
            if(graph == null)
                throw new ArgumentNullException(nameof(graph));

            var searcher = new CyclesSearcher();
            return searcher.FindCycles(graph);
        }

        public static double ClusteringCoefficient(this Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            var coefCalculator = new ClusteringCoefficient(graph);
            return coefCalculator.GetClusteringCoefficientForGraph();
        }

        public static double ClusteringCoefficientFor(this Graph graph, int verticeIndex)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));
            if(verticeIndex < 0)
                throw new ArgumentException("Индекс вершины отрицательный.");

            var coefCalculator = new ClusteringCoefficient(graph);
            return coefCalculator.GetClusteringCoefficientForVertex(verticeIndex);
        }

        public static int[] FindPath(this Graph graph, int from, int to)
        {
            if(graph == null)
                throw new ArgumentNullException(nameof(graph));

            var searcher = new PathSearcher(graph);
            return searcher.FindPath(from, to);
        }

        public static double CalcFirstReciprocity(this Graph graph)
        {
            var calculator = new Reciprocity(graph);
            return calculator.GetFirstReciprocity();
        }

        public static double CalcSecondReciprocity(this Graph graph)
        {
            var calculator = new Reciprocity(graph);
            return calculator.GetSecondReciprocity();
        }
    }
}