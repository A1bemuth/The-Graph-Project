using System.Collections.Generic;
using System.Linq;

namespace GraphAlgorithms
{
    public class CycleComparer : IEqualityComparer<IEnumerable<int>>
    {
        public bool Equals(IEnumerable<int> first, IEnumerable<int> second)
        {
            if (first == null)
                return false;

            if (second == null)
                return false;

            if (ReferenceEquals(first, second))
                return true;

            var firstArray = first as int[] ?? first.ToArray();
            var secondArray = second as int[] ?? second.ToArray();

            if (firstArray.Length != secondArray.Length)
                return false;

            var startIndex = secondArray.IndexOf(v => v == firstArray[0]);
            if (startIndex == -1)
                return false;

            var cyclesLength = firstArray.Length;
            for (int i = 0, j = startIndex; i < cyclesLength; i++)
            {
                if (!firstArray[i].Equals(secondArray[j]))
                    return false;
                j = j == cyclesLength - 1 ? 0 : j + 1;
            }
            return true;
        }

        public int GetHashCode(IEnumerable<int> obj)
        {
            throw new System.NotImplementedException();
        }
    }
}