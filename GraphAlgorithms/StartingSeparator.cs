using System.Collections.Generic;
using System.Linq;

namespace GraphAlgorithms
{
    internal class StartingSeparator
    {
        private readonly int[][] incedenceMatrix;
        private readonly bool[] visitedVertex;
        private readonly List<int> currentSequence;

        internal List<int[]> Cycles { get; }
        internal List<int[]> Segments { get; }

        internal StartingSeparator(int[][] incedenceMatrix)
        {
            this.incedenceMatrix = incedenceMatrix;
            visitedVertex = new bool[incedenceMatrix.Length];
            Cycles = new List<int[]>();
            Segments = new List<int[]>();
            currentSequence = new List<int>();
        }

        internal void Separate()
        {
            while (visitedVertex.Any(v => !v))
            {
                InspectVertex(visitedVertex.IndexesOf(v => !v).First());
            }
        }

        private void InspectVertex(int vertexIndex)
        {
            if (visitedVertex[vertexIndex])
            {
                var startPathIndex = currentSequence
                    .IndexOf((v, index) => v == vertexIndex && index < currentSequence.Count - 1);
                if (startPathIndex == -1)
                {
                    var segment = new List<int>(currentSequence) {vertexIndex};
                    Segments.Add(segment.ToArray());
                }
                else
                {
                    var cycle = currentSequence.Skip(startPathIndex);
                    Cycles.Add(cycle.ToArray());
                }
            }
            else
            {
                visitedVertex[vertexIndex] = true;
                currentSequence.Add(vertexIndex);
                var outgoingArcs = incedenceMatrix[vertexIndex].IndexesOf(v => v == 1);
                foreach (var outgoingArc in outgoingArcs)
                {
                    var nextVertex = incedenceMatrix.IndexInColumnOf(outgoingArc, v => v == -1);
                    InspectVertex(nextVertex);
                }
                currentSequence.RemoveAt(currentSequence.Count - 1);
            }

        }
    }
}