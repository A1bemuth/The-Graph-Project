using System.Collections.Generic;

namespace GraphAlgorithms
{
    public class CyclesSearcher
    {
        private readonly SearchCyclesIterator cyclesIterator;

        public CyclesSearcher(short[][] incedenceMatrix)
        {
            cyclesIterator = new SearchCyclesIterator(incedenceMatrix);
        }

        public IEnumerable<int[]> FindCycles()
        {
            cyclesIterator.Iterate();
            return cyclesIterator.Cycles;
        }
    }
}