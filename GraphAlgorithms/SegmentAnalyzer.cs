using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphAlgorithms
{
    public class SegmentAnalyzer
    {
        private readonly GraphIterator iterator;
        private readonly List<int[]> Cycles;
        public bool iterationComplete;

        internal SegmentAnalyzer(short[][] incedenceMatrix)
        {
            iterator = new GraphIterator(incedenceMatrix);
            iterator.VisitVisitedVertex += DefineCycle;
            iterator.SequenceEnded += arg => iterationComplete = true;
            Cycles = new List<int[]>();
        }

        private void DefineCycle(GraphIteratorEventArgs args)
        {
            var previousVertexIndex = FindPreviousIndex(args.CurrentSequence, args.CurrentVertex);
            if (IsCycle(previousVertexIndex))
            {
                var cycle = args.CurrentSequence.Skip(previousVertexIndex);
                Cycles.Add(cycle.ToArray());
            }
        }

        internal IEnumerable<int[]> CheckSegment(int[] segment)
        {
            iterator.Iterate(segment);
            while (!iterationComplete)
            {
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            }
            return Cycles;
        }

        private int FindPreviousIndex(List<int> currentSequence, int currentVertex)
        {
            return currentSequence
                .IndexOf((v, index) => v == currentVertex && index < currentSequence.Count - 1);
        }

        private bool IsCycle(int previousVertexIndex)
        {
            return previousVertexIndex != -1;
        }
    }
}