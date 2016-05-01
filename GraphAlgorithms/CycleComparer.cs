using System.Collections.Generic;
using System.Linq;

namespace GraphAlgorithms
{
    public class CycleComparer : IEqualityComparer<int[]>
    {
        public bool Equals(int[] first, int[] second)
        {
            if (ThereAreNullCollection(first, second))
                return false;

            if (ReferenceEquals(first, second))
                return true;

            if (first.Length != second.Length)
                return false;

            var startIndex = FindFirstCycleStartIndexInSecondCycle(first, second);
            if (startIndex == -1)
                return false;

            return IsCyclesEqual(first, second, startIndex);
        }

        private bool ThereAreNullCollection(IEnumerable<int> first, IEnumerable<int> second)
        {
            return first == null || second == null;
        }

        private int FindFirstCycleStartIndexInSecondCycle(int[] firstCycle, int[] secondCycle)
        {
            return secondCycle.IndexOf(v => v == firstCycle[0]);
        }

        private bool IsCyclesEqual(int[] firstCycle, int[] secondCycle, int startIndex)
        {
            var cyclesLength = firstCycle.Length;
            for (int i = 0, j = startIndex; i < cyclesLength; i++)
            {
                if (!firstCycle[i].Equals(secondCycle[j]))
                    return false;
                j = j == cyclesLength - 1 ? 0 : j + 1;
            }
            return true;
        }

        public int GetHashCode(int[] cycle)
        {
            unchecked
            {
                return cycle.Sum() + cycle.Length;
            }
        }
    }
}