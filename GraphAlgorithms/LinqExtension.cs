using System;
using System.Collections.Generic;

namespace GraphAlgorithms
{
    public static class LinqExtension
    {
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
    }
}