using System.Collections.Generic;

namespace GraphAlgorithms
{
    internal class StartingSeparator
    {
        internal IEnumerable<int[]> Paths { get; }
        internal IEnumerable<int[]> Segments { get; }

        internal StartingSeparator(int[][] incedenceMatrix)
        {
            Paths = new List<int[]>();
            Segments = new List<int[]>();
        }

        internal void Separate()
        {
        }
    }
}