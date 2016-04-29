using System;
using System.Collections.Generic;
using System.Linq;
using GraphDataLayer;

namespace GraphAlgorithms
{
    public class DegreeCentrality
    {
        public DegreeCentrality(Graph graph)
        {
            this.graph = graph;
            this.vertices = Enumerable.Range(0, graph.VerticesCount).ToList();
        }

        public int GetVerticeIndegreeCentrality(int index)
        {
            return graph.GetIncomingVertex(index).Count;
        }

        public int GetVerticeOutdegreeCentrality(int index)
        {
            return graph.GetNeighbours(index).Count;
        }

        public double GetGraphIndegreeCentrality()
        {
            return Centrality(GetVerticeIndegreeCentrality);
        }

        public double GetGraphOutdegreeCentrality()
        {
            return Centrality(GetVerticeOutdegreeCentrality);
        }

        public double GetIndegreeStandartDeviation()
        {
            return GetStandartDeviation(GetVerticeIndegreeCentrality);
        }

        public double GetOutdegreeStandartDeviation()
        {
            return GetStandartDeviation(GetVerticeOutdegreeCentrality);
        }

        private double GetStandartDeviation(Func<int, int> getVerticeCentrality )
        {
            var avg = MeanCentrality(getVerticeCentrality);
            var dispersion = (double) 1/vertices.Count * vertices.Sum(c => Math.Pow(c - avg, 2));
            return Math.Round(Math.Sqrt(dispersion), 4);
        }

        private double MeanCentrality(Func<int, int> getVerticeCentrality)
        {
            return vertices.Average();
        }

        private double Centrality(Func<int, int> getVerticeCentrality)
        {
            var verticesCount = graph.VerticesCount;
            var centralities = vertices
                .Select(getVerticeCentrality)
                .ToList();

            var mostCentral = centralities.Max();
            var sum = centralities.Sum(c => mostCentral - c);
            var denominator = (verticesCount - 1)*(verticesCount - 2);

            return Math.Round((double) sum/denominator, 4);
        }

        public double Density()
        {
            if (graph.VerticesCount == 1)
                return 0;
            var denominator = graph.VerticesCount*(graph.VerticesCount - 1);

            return Math.Round((double)graph.ArrowsCount/denominator, 4);
        }

        private List<int> vertices;

        private readonly Graph graph;
    }
}
