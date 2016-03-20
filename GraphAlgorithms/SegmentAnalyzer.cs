using System.Collections.Generic;
using System.Linq;

namespace GraphAlgorithms
{
    public class SegmentAnalyzer
    {
        private readonly short[][] incedenceMatrix;
        private readonly bool[] visitedVertices;
        private List<int> currentSequence;
        private readonly List<int[]> Cycles;
        private int currentVertex;

        internal SegmentAnalyzer(short[][] incedenceMatrix)
        {
            this.incedenceMatrix = incedenceMatrix;
            visitedVertices = new bool[incedenceMatrix.Length];
            Cycles = new List<int[]>();
        }

        internal IEnumerable<int[]> CheckSegment(int[] segment)
        {
            currentSequence = new List<int>(segment.Take(segment.Length - 1));
            foreach (var i in currentSequence)
            {
                visitedVertices[i] = true;
            }
            currentVertex = segment.Last();

            InspectVertex();

            return Cycles;
        }

        private void InspectVertex()
        {
            if (visitedVertices[currentVertex])
            {
                var previousVertexIndex = currentSequence
                    .IndexOf((v, index) => v == currentVertex && index < currentSequence.Count - 1);
                if (previousVertexIndex != -1)
                {
                    var cycle = currentSequence.Skip(previousVertexIndex);
                    Cycles.Add(cycle.ToArray());
                }
            }
            else
            {
                JumpToNextVertex();
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