using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using GraphDataLayer;

namespace GraphAlgorithms
{
    public class GraphIterator
    {
        private readonly Graph graph;
        private readonly bool[] visitedVertices;
        //private readonly bool[] verticesInSequence;
        //private readonly LinkedList<int> currentSequence;

        internal event Action<int[]> CycleDetected;

        internal GraphIterator(Graph graph)
        {
            this.graph = graph;
            visitedVertices = new bool[graph.VerticesCount];
            //verticesInSequence = new bool[graph.VerticesCount];
            //currentSequence = new LinkedList<int>();
        }

        internal void Iterate()
        {
            while (ThereAreNotVisitedVertices())
            {
                var vertex = visitedVertices.IndexOf(v => !v);
                var currentSequence = ImmutableList<int>.Empty;
                var verticeInSequence = new bool[graph.VerticesCount].ToImmutableArray();
                InspectVertex(vertex, currentSequence, verticeInSequence);
            }
        }

        private bool ThereAreNotVisitedVertices()
        {
            return visitedVertices.Any(v => !v);
        }

        private void InspectVertex(int vertex, ImmutableList<int> currentSequence, ImmutableArray<bool> verticesInSequence)
        {
            if (verticesInSequence[vertex])
            {
                var previousIndex = currentSequence.IndexOf(vertex);
                var cycle = currentSequence.Skip(previousIndex).ToArray();
                if (cycle.Length > 2)
                    OnCycleDetected(cycle);
            }
            else
            {
                var currentSequenceWithVertex = currentSequence.Add(vertex);
                verticesInSequence = verticesInSequence.SetItem(vertex, true);
                visitedVertices[vertex] = true;
//                foreach (var neighbor in graph.GetNeighbours(vertex))
//                {
//                    InspectVertex(neighbor);
//                }

                var tasks = graph
                    .GetNeighbours(vertex)
                    .Select(n => Task.Run(() => InspectVertex(n, currentSequenceWithVertex, verticesInSequence)))
                    .ToArray();
                Task.WaitAll(tasks);
            }
        }

//        private bool IsVertexInSequence(int vertex)
//        {
//            return verticesInSequence[vertex];
//        }

//        private void IncludeVertexInSequence(int vertex)
//        {
//            visitedVertices[vertex] = true;
//            verticesInSequence[vertex] = true;
//            currentSequence.AddLast(vertex);
//        }

//        private int FindPreviousIndex(int vertex)
//        {
//            return currentSequence
//                .IndexOf(v => v == vertex);
//        }

        private void OnCycleDetected(int[] cycle)
        {
            CycleDetected?.Invoke(cycle);
        }

//        private void ExcludeVertexFromSequence(int vertex)
//        {
//            currentSequence.RemoveLast();
//            verticesInSequence[vertex] = false;
//        }
    }
}