using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphAlgorithms
{
    public class CyclesSearcher
    {
        private List<int[]> cycles;
        public short[][] IncedenceMatrix { get; set; }

        public CyclesSearcher()
        {
        }

        public CyclesSearcher(short[][] incedenceMatrix) : this()
        {
            IncedenceMatrix = incedenceMatrix;
        }

        public IEnumerable<int[]> FindCycles()
        {
            if(IncedenceMatrix == null)
                throw new NullReferenceException("The incedence matrix property is null");

            cycles = new List<int[]>();
            var startingIterator = new GraphIterator(IncedenceMatrix);
            startingIterator.PreviouslyHitVerticeVisited += DefineCycleOrSegmentOnFirstIteration;
            startingIterator.IterateAllGraph();
            return cycles;
        }

        public IEnumerable<int[]> FindCycles(short[][] incedenceMatrix)
        {
            IncedenceMatrix = incedenceMatrix;
            return FindCycles();
        }


        private void DefineCycleOrSegmentOnFirstIteration(GraphIteratorEventArgs args)
        {
            var previousVertexIndex = FindPreviousIndex(args.CurrentSequence, args.CurrentVertex);
            if (IsCycle(previousVertexIndex))
            {
                var cycle = args.CurrentSequence.Skip(previousVertexIndex);
                cycles.Add(cycle.ToArray());
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
            segmentIterator.PreviouslyHitVerticeVisited += DefineCycleOnTwoIteration;
            segmentIterator.IterateSegment(segment.ToArray());
        }

        private void DefineCycleOnTwoIteration(GraphIteratorEventArgs args)
        {
            var previousVertexIndex = FindPreviousIndex(args.CurrentSequence, args.CurrentVertex);
            if (IsCycle(previousVertexIndex))
            {
                var cycle = args.CurrentSequence.Skip(previousVertexIndex).ToArray();
                if (IsNewCycle(cycle))
                    cycles.Add(cycle);
            }
        }

        private bool IsNewCycle(int[] cycle)
        {
            var comparer = new CycleComparer();
            return cycles.All(intse => !comparer.Equals(cycle, intse));
        }
    }
}