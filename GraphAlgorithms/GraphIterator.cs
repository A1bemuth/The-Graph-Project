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
        private int currentVertex;

        internal event Action<GraphIteratorEventArgs> PreviouslyHitVerticeVisited;

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
            if (IsCurrentVertexVisited(vertex))
            {
                OnVisitVisitedVertex(vertex);
            }
            else
            {
                JumpToNextVertex(vertex);
            }
        }

        private bool IsCurrentVertexVisited(int vertex)
        {
            return verticesInSequence[vertex];
        }

        private void JumpToNextVertex(int vertex)
        {
            IncludeInSequence(vertex);

            foreach (var arcIndex in FindOutgoingArcIndexes(vertex))
            {
                var nextVertex = FindeNextVertex(arcIndex);
                InspectVertex(nextVertex);
            }
            ExcludeLastVertexFromSequence(vertex);
        }

        private void IncludeInSequence(int vertex)
        {
            visitedVertices[vertex] = true;
            verticesInSequence[vertex] = true;
            currentSequence.Add(vertex);
        }

        private IEnumerable<int> FindOutgoingArcIndexes(int vertex)
        {
            return incedenceMatrix[vertex].IndexesOf(v => v == 1);
        }

        private int FindeNextVertex(int outgoingArcIndex)
        {
            return incedenceMatrix.IndexInColumnOf(outgoingArcIndex, v => v == -1);
        }

        private void ExcludeLastVertexFromSequence(int vertex)
        {
            currentSequence.RemoveAt(currentSequence.Count - 1);
            verticesInSequence[vertex] = false;
        }

        private void OnVisitVisitedVertex(int vertex)
        {
            var eventArgs = new GraphIteratorEventArgs(visitedVertices, currentSequence, vertex);
            PreviouslyHitVerticeVisited?.Invoke(eventArgs);
        }
    }
}