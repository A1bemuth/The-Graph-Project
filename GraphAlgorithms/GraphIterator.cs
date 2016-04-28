using System;
using System.Collections.Generic;
using System.Linq;
using GraphDataLayer;

namespace GraphAlgorithms
{
    public class GraphIterator
    {
        private readonly Graph graph;
        private readonly bool[] visitedVertices;
        private readonly bool[] verticesInSequence;
        private readonly LinkedList<int> currentSequence;

        internal event Action<int[]> CycleDetected;

        internal GraphIterator(Graph graph)
        {
            this.graph = graph;
            visitedVertices = new bool[graph.VerticesCount];
            verticesInSequence = new bool[graph.VerticesCount];
            currentSequence = new LinkedList<int>();
        }

        internal void Iterate()
        {
            while (ThereAreNotVisitedVertices())
            {
                var vertex = visitedVertices.IndexOf(v => !v);
                InspectVertex(vertex);
            }
        }

        private bool ThereAreNotVisitedVertices()
        {
            return visitedVertices.Any(v => !v);
        }

        private void InspectVertex(int vertex)
        {
            if (IsVertexInSequence(vertex))
            {
                var previousIndex = FindPreviousIndex(vertex);
                var cycle = currentSequence.Skip(previousIndex).ToArray();
                OnCycleDetected(cycle);
            }
            else
            {
                IncludeVertexInSequence(vertex);
                foreach (var neighbor in graph.GetNeighbours(vertex))
                {
                    InspectVertex(neighbor);
                }
                ExcludeVertexFromSequence(vertex);
            }
        }

        private bool IsVertexInSequence(int vertex)
        {
            return verticesInSequence[vertex];
        }

        private void IncludeVertexInSequence(int vertex)
        {
            visitedVertices[vertex] = true;
            verticesInSequence[vertex] = true;
            currentSequence.AddLast(vertex);
        }

        private int FindPreviousIndex(int vertex)
        {
            return currentSequence
                .IndexOf(v => v == vertex);
        }

        private void OnCycleDetected(int[] cycle)
        {
            CycleDetected?.Invoke(cycle);
        }

        private void ExcludeVertexFromSequence(int vertex)
        {
            currentSequence.RemoveLast();
            verticesInSequence[vertex] = false;
        }
    }
}