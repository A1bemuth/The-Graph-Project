using System.Collections.Generic;

namespace GraphAlgorithms
{
    public class CycleComparer : IEqualityComparer<int[]>
    {
        private int[] firstCycle;
        private int[] secondCycle;

        public bool Equals(int[] first, int[] second)
        {
            if (ThereAreNullCollection(first, second))
                return false;

            if (ReferenceEquals(first, second))
                return true;

            firstCycle = first;
            secondCycle = second;

            if (firstCycle.Length != secondCycle.Length)
                return false;

            var startIndex = FindFirstCycleStartIndexInSecondCycle();
            if (startIndex == -1)
                return false;

            return IsCyclesEqual(startIndex);
        }

        private bool ThereAreNullCollection(IEnumerable<int> first, IEnumerable<int> second)
        {
            return first == null || second == null;
        }

        private int FindFirstCycleStartIndexInSecondCycle()
        {
            return secondCycle.IndexOf(v => v == firstCycle[0]);
        }

        private bool IsCyclesEqual(int startIndex)
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
                var hash = 0;
                for (var i = 0; i < cycle.Length; i++)
                {
                    hash += cycle[i] ^ (i + 1);
                }
                return hash;
            }
        }
    }
}