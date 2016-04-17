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
        private Queue<int> verticesSicunce;


        public PathSearcher(IGraph graph)
        {
            if(graph == null)
                throw new ArgumentNullException(nameof(graph));

            this.graph = graph;
            visitedVertices = new bool[graph.VerticesCount];
            distance = Enumerable.Repeat(graph.VerticesCount, graph.VerticesCount).ToArray();
            perent = Enumerable.Repeat(-1, graph.VerticesCount).ToArray();
            verticesSicunce = new Queue<int>(graph.VerticesCount);
        }

        public int[] FindPath(int from, int to)
        {
            distance[to] = 0;
            verticesSicunce.Enqueue(to);

            while (verticesSicunce.Count != 0)
            {
                var vertice = verticesSicunce.Dequeue();
                foreach (var incoming in graph.GetIncomingVertex(vertice))
                {
                    if(visitedVertices[incoming])
                        continue;
                    var pathViaCurrentVertice = distance[vertice] + 1;
                    if (distance[incoming] > pathViaCurrentVertice)
                    {
                        distance[incoming] = pathViaCurrentVertice;
                        perent[incoming] = vertice;
                    }
                    verticesSicunce.Enqueue(incoming);
                }
                visitedVertices[vertice] = true;
            }

            var path = new List<int> { from };
            var currentVertice = perent[from];
            while (true)
            {
                if (currentVertice == -1)
                    return null;
                path.Add(currentVertice);
                if (currentVertice == to)
                    return path.ToArray();
                currentVertice = perent[currentVertice];
            }
        }
    }
}