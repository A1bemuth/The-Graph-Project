using System;
using System.Collections.Generic;
using System.Linq;
using GraphDataLayer;

namespace GraphAlgorithms
{
    public class PathSearcher
    {
        private readonly IGraph graph;
        private readonly bool[] visitedVertices;
        private readonly int[] distance;
        private readonly int[] perent;
        private readonly Queue<int> verticesSequence;
        private int startVertice;
        private int endVertice;


        public PathSearcher(IGraph graph)
        {
            if(graph == null)
                throw new ArgumentNullException(nameof(graph));

            this.graph = graph;
            visitedVertices = new bool[graph.VerticesCount];
            distance = Enumerable.Repeat(graph.VerticesCount, graph.VerticesCount).ToArray();
            perent = Enumerable.Repeat(-1, graph.VerticesCount).ToArray();
            verticesSequence = new Queue<int>(graph.VerticesCount);
        }

        public int[] FindPath(int from, int to)
        {
            if(from < 0 || from > graph.VerticesCount -1)
                throw new ArgumentException("Start vertice invalid");
            if(to < 0 || to > graph.VerticesCount - 1)
                throw new ArgumentException("End vertice invalid");
            if(from == to)
                throw new ArgumentException("Vertices don't have to be equals");

            startVertice = from;
            endVertice = to;
            distance[to] = 0;
            verticesSequence.Enqueue(to);
            CalculateDistanceBetweenAllVertices();
            return CreatePath();
        }

        private void CalculateDistanceBetweenAllVertices()
        {
            while (IsVerticeSequenceNotEmpty())
            {
                var currentVertice = verticesSequence.Dequeue();
                AnalizeDistanceBetweenIncomingVerticeAnd(currentVertice);
                visitedVertices[currentVertice] = true;
            }
        }

        private int[] CreatePath()
        {
            var path = new List<int> { startVertice };
            var currentVertice = perent[startVertice];
            while (true)
            {
                if (currentVertice == -1)
                    return null;
                path.Add(currentVertice);
                if (currentVertice == endVertice)
                    return path.ToArray();
                currentVertice = perent[currentVertice];
            }
        }

        private bool IsVerticeSequenceNotEmpty()
        {
            return verticesSequence.Count != 0;
        }

        private void AnalizeDistanceBetweenIncomingVerticeAnd(int currentVertice)
        {
            foreach (var incoming in graph.GetIncomingVertex(currentVertice))
            {
                if (IsVisitedVertice(incoming))
                    continue;
                var pathViaCurrentVertice = distance[currentVertice] + 1;
                if (IsPathLongerThenViaVertice(incoming, pathViaCurrentVertice))
                {
                    distance[incoming] = pathViaCurrentVertice;
                    perent[incoming] = currentVertice;
                }
                verticesSequence.Enqueue(incoming);
            }
        }

        private bool IsVisitedVertice(int vertice)
        {
            return visitedVertices[vertice];
        }

        private bool IsPathLongerThenViaVertice(int incomingVertice, int pathViaCurrentVertice)
        {
            return distance[incomingVertice] > pathViaCurrentVertice;
        }
    }
}