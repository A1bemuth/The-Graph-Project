using System;

namespace GraphDataLayer
{
    internal static class ArrayExtensions
    {
        internal static void AddWithGrowth<T>(this T[] arr, T element)
        {
            var initialLength = arr.Length;
            Array.Resize(ref arr, initialLength * 2);
            arr[initialLength] = element;
        }
    }
}
