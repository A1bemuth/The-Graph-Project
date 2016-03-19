using System;
using System.Collections.Generic;

namespace GraphAlgorithms
{
    public static class LinqExtension
    {
        public static IEnumerable<int> IndexesForColumn<T>(this T[][] matrix, int columnIndex, Func<T, bool> predicate)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));
            if(matrix.Length == 0)
                throw new ArgumentException("Matrix's first dimension must be more 0.");
            if (matrix[0].Length - 1 < columnIndex)
                throw new ArgumentOutOfRangeException(nameof(columnIndex), "Index is beyond matrix dimension.");

            for (var i = 0; i < matrix.Length; i++)
            {
                if (predicate.Invoke(matrix[i][columnIndex]))
                    yield return i;
            }
        }
    }

}