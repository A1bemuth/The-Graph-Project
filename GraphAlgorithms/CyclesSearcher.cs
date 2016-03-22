using System.Collections.Generic;
using System.Linq;

namespace GraphAlgorithms
{
    public class CyclesSearcher
    {
        private short[][] incedenceMatrix;

        internal List<int[]> Cycles { get; private set; }

        public short[][] IncedenceMatrix
        {
            get { return incedenceMatrix; }
            set
            {
                incedenceMatrix = value;
                Cycles = new List<int[]>();
            }
        }

        public CyclesSearcher()
        {
            Cycles = new List<int[]>();
        }

        public CyclesSearcher(short[][] incedenceMatrix) : this()
        {
            IncedenceMatrix = incedenceMatrix;
        }

        public void FindCycles()
        {
            var startingIterator = new GraphIterator(IncedenceMatrix);
            startingIterator.VisitVisitedVertex += DefineCycleOrSegmentOnFirstIteration;
            startingIterator.IterateAllGraph();
        }


        private void DefineCycleOrSegmentOnFirstIteration(GraphIteratorEventArgs args)
        {
            var previousVertexIndex = FindPreviousIndex(args.CurrentSequence, args.CurrentVertex);
            if (IsCycle(previousVertexIndex))
            {
                var cycle = args.CurrentSequence.Skip(previousVertexIndex);
                Cycles.Add(cycle.ToArray());
            }
            else
            {
                var segment = new List<int>(args.CurrentSequence) { args.CurrentVertex };
                AnalyzeSegment(segment);
            }
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

        private void AnalyzeSegment(IEnumerable<int> segment)
        {
            var segmentIterator = new GraphIterator(IncedenceMatrix);
            segmentIterator.VisitVisitedVertex += DefineCycleOnTwoIteration;
            segmentIterator.IterateSegment(segment.ToArray());
        }

        private void DefineCycleOnTwoIteration(GraphIteratorEventArgs args)
        {
            var previousVertexIndex = FindPreviousIndex(args.CurrentSequence, args.CurrentVertex);
            if (IsCycle(previousVertexIndex))
            {
                var cycle = args.CurrentSequence.Skip(previousVertexIndex).ToArray();
                if (IsNewCycle(cycle))
                    Cycles.Add(cycle);
            }
        }

        private bool IsNewCycle(int[] cycle)
        {
            var comparer = new CycleComparer();
            return Cycles.All(intse => !comparer.Equals(cycle, intse));
        }
    }
}