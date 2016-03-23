using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphAlgorithms
{
    public class GraphIterator
    {
        private readonly short[][] incedenceMatrix;
        private readonly bool[] visitedVertices;
        private readonly bool[] verticesInSequence;
        private readonly List<int> currentSequence;

        internal event Action<int[]> PreviouslyHitVerticeVisited;

        internal GraphIterator(short[][] incedenceMatrix)
        {
            this.incedenceMatrix = incedenceMatrix;
            visitedVertices = new bool[incedenceMatrix.Length];
            verticesInSequence = new bool[incedenceMatrix.Length];
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
            foreach (var neighbor in FindNeighbors(vertex))
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

        private IEnumerable<int> FindNeighbors(int vertex)
        {
            return FindOutgoingArcIndexes(vertex).Select(FindeNextVertex);
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
            PreviouslyHitVerticeVisited?.Invoke(cycle);
        }

        private void ExcludeLastVertexFromSequence(int vertex)
        {
            currentSequence.RemoveAt(currentSequence.Count - 1);
            verticesInSequence[vertex] = false;
        }

        private IEnumerable<int> FindOutgoingArcIndexes(int vertex)
        {
            return incedenceMatrix[vertex].IndexesOf(v => v == 1);
        }

        private int FindeNextVertex(int outgoingArcIndex)
        {
            return incedenceMatrix.IndexInColumnOf(outgoingArcIndex, v => v == -1);
        }
    }
}