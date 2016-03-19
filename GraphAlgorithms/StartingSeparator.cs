using System.Collections.Generic;
using System.Linq;

namespace GraphAlgorithms
{
    internal class StartingSeparator
    {
        private readonly int[][] incedenceMatrix;
        private readonly bool[] visitedVertex;
        private readonly List<int> currentPath;

        internal List<int[]> Paths { get; }
        internal List<int[]> Segments { get; }

        internal StartingSeparator(int[][] incedenceMatrix)
        {
            this.incedenceMatrix = incedenceMatrix;
            visitedVertex = new bool[incedenceMatrix.Length];
            Paths = new List<int[]>();
            Segments = new List<int[]>();
            currentPath = new List<int>();
        }

        internal void Separate()
        {
            InspectVertex(0);
        }

        private void InspectVertex(int vertexIndex)
        {
            currentPath.Add(vertexIndex);
            if (visitedVertex[vertexIndex])
            {
                Paths.Add(currentPath.ToArray());
            }
            else
            {
                visitedVertex[vertexIndex] = true;
                var outgoingArcs = incedenceMatrix[vertexIndex].IndexesOf(v => v == 1).Reverse();
                foreach (var outgoingArc in outgoingArcs)
                {
                    var nextVertex = incedenceMatrix.IndexInColumnOf(outgoingArc, v => v == -1);
                    InspectVertex(nextVertex);
                }
            }
        }
    }
}