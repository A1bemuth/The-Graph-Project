using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphAlgorithms
{
    internal class StartingSeparator
    {
        private readonly int[][] incedenceMatrix;
        private readonly bool[] visitedVertex;

        internal IEnumerable<int[]> Paths { get; }
        internal IEnumerable<int[]> Segments { get; }

        internal StartingSeparator(int[][] incedenceMatrix)
        {
            this.incedenceMatrix = incedenceMatrix;
            visitedVertex = new bool[incedenceMatrix.Length];
            Paths = new List<int[]>();
            Segments = new List<int[]>();
        }

        internal void Separate()
        {
            visitedVertex[0] = true;
            var outgoingArcs = incedenceMatrix[0].Where(v => v == 1);
            foreach (var outgoingArc in outgoingArcs)
            {
                var test = incedenceMatrix.IndexesForColumn(outgoingArc, v => v == -1);
            }
        }
    }
}