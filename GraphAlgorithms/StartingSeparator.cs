using System.Collections.Generic;
using System.Linq;

namespace GraphAlgorithms
{
    internal class StartingSeparator
    {
        private readonly int[][] incedenceMatrix;
        private readonly bool[] visitedVertex;
        private List<int> currentPath;

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
            while (visitedVertex.Any(v => !v))
            {
                InspectVertex(visitedVertex.IndexesOf(v => !v).First());
            }
        }

        private void InspectVertex(int vertexIndex)
        {
            currentPath.Add(vertexIndex);
            if (visitedVertex[vertexIndex])
            {
                var startPathIndex = currentPath
                    .IndexOf((v, index) => v == vertexIndex && index < currentPath.Count - 1);
                if (startPathIndex == -1)
                {
                    Segments.Add(currentPath.ToArray());
                }
                else
                {
                    var cycle = currentPath.Skip(startPathIndex);
                    Paths.Add(cycle.ToArray());
                }
                currentPath = new List<int>();
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