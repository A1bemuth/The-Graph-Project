using System;
using System.Collections.Generic;

namespace GraphAlgorithms
{
    public static class LinqExtension
    {
        public static IEnumerable<int> IndexesInColumn<T>(this T[][] matrix, int columnIndex, Func<T, bool> predicate)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));
            if(predicate == null)
                throw new ArgumentNullException(nameof(predicate));
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

        public static int IndexInColumnOf<T>(this T[][] matrix, int columnIndex, Func<T, bool> predicate)
        {
            if(matrix == null)
                throw new ArgumentNullException(nameof(matrix));
            if(predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            if(matrix.Length == 0)
                throw new ArgumentException("Matrix's first dimension must be more 0.");
            if (matrix[0].Length - 1 < columnIndex)
                throw new ArgumentOutOfRangeException(nameof(columnIndex), "Index is beyond matrix dimension.");

            for (var i = 0; i < matrix.Length; i++)
            {
                if (predicate.Invoke(matrix[i][columnIndex]))
                    return i;
            }
            return -1;
        }

        public static int IndexOf<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));
            if(predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var index = 0;
            foreach (var item in items)
            {
                if (predicate.Invoke(item))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public static int IndexOf<T>(this IEnumerable<T> items, Func<T, int, bool> predicate)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var index = 0;
            foreach (var item in items)
            {
                if (predicate.Invoke(item, index))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public static IEnumerable<int> IndexesOf<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));
            if(predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var index = 0;
            foreach (var item in items)
            {
                if (predicate.Invoke(item))
                {
                    yield return index;
                }
                index++;
            }
        }
    }
}