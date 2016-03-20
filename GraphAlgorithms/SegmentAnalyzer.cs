using System.Collections.Generic;

namespace GraphAlgorithms
{
    public class SegmentAnalyzer
    {
        private readonly short[][] incedenceMatrix;

        internal SegmentAnalyzer(short[][] incedenceMatrix)
        {
            this.incedenceMatrix = incedenceMatrix;
        }

        internal IEnumerable<int[]> CheckSegment(IEnumerable<int> segment)
        {
            return new List<int[]>();
        }
    }
}