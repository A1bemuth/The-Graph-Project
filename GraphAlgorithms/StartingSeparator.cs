using System.Collections.Generic;
using System.Linq;

namespace GraphAlgorithms
{
    internal class StartingSeparator
    {
        private readonly int[][] incedenceMatrix;
        private readonly bool[] visitedVertices;
        private readonly List<int> currentSequence;
        private int currentVertex;

        internal List<int[]> Cycles { get; }
        internal List<int[]> Segments { get; }

        internal StartingSeparator(int[][] incedenceMatrix)
        {
            this.incedenceMatrix = incedenceMatrix;
            visitedVertices = new bool[incedenceMatrix.Length];
            Cycles = new List<int[]>();
            Segments = new List<int[]>();
            currentSequence = new List<int>();
        }

        internal void Separate()
        {
            while (ThereAreNotVisitedVertices())
            {
                currentVertex = visitedVertices.IndexesOf(v => !v).First();
                InspectVertex();
            }
        }

        private bool ThereAreNotVisitedVertices()
        {
            return visitedVertices.Any(v => !v);
        }

        private void InspectVertex()
        {
            if (IsCurrentVertexVisited())
            {
                DefineCycleOrSegment();
            }
            else
            {
                JumpToNextVertex();
            }
        }

        private void DefineCycleOrSegment()
        {
            var previousVertexIndex = FindPreviousIndexOf();
            if (IsCycle(previousVertexIndex))
            {
                var cycle = currentSequence.Skip(previousVertexIndex);
                Cycles.Add(cycle.ToArray());
            }
            else
            {
                var segment = new List<int>(currentSequence) { currentVertex };
                Segments.Add(segment.ToArray());
            }
        }

        private void JumpToNextVertex()
        {
            IncludeInSequence();
            foreach (var arcIndex in FindOutgoingArcIndexes())
            {
                currentVertex = FindeNextVertex(arcIndex);
                InspectVertex();
            }
            ExcludeLastVertexFromSequence();

        }

        private bool IsCurrentVertexVisited()
        {
            return visitedVertices[currentVertex];
        }

        private int FindPreviousIndexOf()
        {
            return currentSequence
                .IndexOf((v, index) => v == currentVertex && index < currentSequence.Count - 1);
        }

        private bool IsCycle(int previousVertexIndex)
        {
            return previousVertexIndex != -1;
        }

        private void IncludeInSequence()
        {
            visitedVertices[currentVertex] = true;
            currentSequence.Add(currentVertex);
        }

        private IEnumerable<int> FindOutgoingArcIndexes()
        {
            return incedenceMatrix[currentVertex].IndexesOf(v => v == 1);
        }

        private int FindeNextVertex(int outgoingArcIndex)
        {
            return incedenceMatrix.IndexInColumnOf(outgoingArcIndex, v => v == -1);
        }

        private void ExcludeLastVertexFromSequence()
        {
            currentSequence.RemoveAt(currentSequence.Count - 1);
        }
    }
}