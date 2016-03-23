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


        private void DefineCycleOrSegmentOnFirstIteration(int[] cycle)
        {
            if (IsNewCycle(cycle))
                cycles.Add(cycle);
        }

        private bool IsNewCycle(int[] cycle)
        {
            var comparer = new CycleComparer();
            return cycles.All(intse => !comparer.Equals(cycle, intse));
        }
    }
}