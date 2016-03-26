using System;
using System.Collections.Generic;
using System.Linq;
using GraphDataLayer;

namespace GraphAlgorithms
{
    public class GraphIterator
    {
        private readonly IGraph graph;
        private readonly bool[] visitedVertices;
        private readonly bool[] verticesInSequence;
        private readonly List<int> currentSequence;

        internal event Action<int[]> CycleDetected;

        internal GraphIterator(IGraph graph)
        {
            this.graph = graph;
            visitedVertices = new bool[graph.VerticesCount];
            verticesInSequence = new bool[graph.VerticesCount];
            currentSequence = new List<int>();
        }

        internal void IterateAllGraph()
        {
            while (ThereAreNotVisitedVertices())
            {
                var vertex = visitedVertices.IndexesOf(v => !v).First();
                InspectVertex(vertex);
            }
        }

        private bool ThereAreNotVisitedVertices()
        {
            return visitedVertices.Any(v => !v);
        }

        private void InspectVertex(int vertex)
        {
            IncludeInSequence(vertex);
            foreach (var neighbor in graph.GetNeighbours(vertex))
            {
                var previousIndex = FindPreviousIndex(neighbor);
                if (IsCycle(previousIndex))
                {
                    var cycle = currentSequence.Skip(previousIndex).ToArray();
                    OnVisitVisitedVertex(cycle);
                }
                else
                {
                    InspectVertex(neighbor);
                }
            }
            ExcludeLastVertexFromSequence(vertex);
        }

        private void IncludeInSequence(int vertex)
        {
            visitedVertices[vertex] = true;
            verticesInSequence[vertex] = true;
            currentSequence.Add(vertex);
        }

        private int FindPreviousIndex(int vertex)
        {
            return currentSequence
                .IndexOf((v, index) => v == vertex && index < currentSequence.Count - 1);
        }

        private bool IsCycle(int previousVertexIndex)
        {
            return previousVertexIndex != -1;
        }

        private void OnVisitVisitedVertex(int[] cycle)
        {
            CycleDetected?.Invoke(cycle);
        }

        private void ExcludeLastVertexFromSequence(int vertex)
        {
            currentSequence.RemoveAt(currentSequence.Count - 1);
            verticesInSequence[vertex] = false;
        }
    }
}