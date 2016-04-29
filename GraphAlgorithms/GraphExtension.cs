using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

        public static async Task<List<int[]>> FindCyclesAsync(this Graph graph, IProgress<int[]> progress, CancellationToken ct)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            var searcher = new CyclesSearcher();
            return await searcher.FindCyclesAsync(graph, progress, ct);
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

        public static double GetPrestigeFor(this Graph graph, int vertice)
        {
            var calculator = new DegreeCentrality(graph);
            return calculator.GetVerticeIndegreeCentrality(vertice);
        }

        public static double GetInfluenceFor(this Graph graph, int vertice)
        {
            var calculator = new DegreeCentrality(graph);
            return calculator.GetVerticeOutdegreeCentrality(vertice);
        }

        public static double GetGraphPrestige(this Graph graph)
        {
            var calculator = new DegreeCentrality(graph);
            return calculator.GetGraphIndegreeCentrality();
        }

        public static double GetGraphInfluence(this Graph graph)
        {
            var calculator = new DegreeCentrality(graph);
            return calculator.GetGraphOutdegreeCentrality();
        }

        public static double GetIndegreesStandartDeviation(this Graph graph)
        {
            var calculator = new DegreeCentrality(graph);
            return calculator.GetIndegreeStandartDeviation();
        }

        public static double GetOutdegreesStandartDeviation(this Graph graph)
        {
            var calculator = new DegreeCentrality(graph);
            return calculator.GetOutdegreeStandartDeviation();
        }

        public static double GetDensity(this Graph graph)
        {
            var calculator = new DegreeCentrality(graph);
            return calculator.Density();
        }
    }
}