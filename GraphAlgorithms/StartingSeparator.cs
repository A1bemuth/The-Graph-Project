using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphAlgorithms
{
    internal class StartingSeparator
    {
        private readonly int[][] incedenceMatrix;
        private readonly bool[] visitedVertex;
        private readonly Stack<int> vertexSquence;

        internal IEnumerable<int[]> Paths { get; }
        internal IEnumerable<int[]> Segments { get; }

        internal StartingSeparator(int[][] incedenceMatrix)
        {
            this.incedenceMatrix = incedenceMatrix;
            visitedVertex = new bool[incedenceMatrix.Length];
            Paths = new List<int[]>();
            Segments = new List<int[]>();
            vertexSquence = new Stack<int>();
        }

        internal void Separate()
        {
            vertexSquence.Push(0);
            while (vertexSquence.Count != 0)
            {
                InspectVertex(vertexSquence.Peek());
            }
        }

        private void InspectVertex(int vertexIndex)
        {
            visitedVertex[vertexIndex] = true;
            var outgoingArcs = incedenceMatrix[vertexIndex].IndexesOf(v => v == 1);
            foreach (var outgoingArc in outgoingArcs)
            {
                var vartexNeighbors  = incedenceMatrix
                    .IndexesForColumn(outgoingArc, v => v == -1)
                    .Reverse();
            }

        }
    }
}