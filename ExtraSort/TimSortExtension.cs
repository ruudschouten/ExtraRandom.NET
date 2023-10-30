namespace ExtraSort;

/// <summary>
/// Extension method for <see cref="IList{T}"/> to add TimSort.
/// </summary>
/// <remarks>Based on: https://www.baeldung.com/cs/timsort.</remarks>
public static class TimSortExtension
{
    private const int Threshold = 8;

    /// <summary>
    /// Perform a TimSort on the <paramref name="list"/>.
    /// </summary>
    /// <param name="list">List to perform the TimSort on.</param>
    /// <typeparam name="T">Type of elements in the <paramref name="list"/>.</typeparam>
    public static void TimSort<T>(this IList<T> list)
        where T : IComparable<T>
    {
        // Calculate the run lenght.
        var runLength = CalculateRunLength(list);

        // Perform insertion sort on each run
        for (var start = 0; start < list.Count; start += runLength)
        {
            var end = Math.Min(list.Count, start + runLength);
            list.InsertionSort(start, end);
        }

        // Recursively merge adjacent runs.
        Merge(list, runLength);
    }

    private static void Merge<T>(IList<T> list, int runLength)
        where T : IComparable<T>
    {
        var count = list.Count;

        for (var size = runLength; size < count; size = 2 * size)
        {
            for (var left = 0; left < count; left += 2 * size)
            {
                var mid = left + size - 1;
                var right = Math.Min(left + (2 * size) - 1, count - 1);
                if (mid < right)
                {
                    list = list.MergeSort(left, mid, right);
                }
            }
        }
    }

    private static int CalculateRunLength<T>(ICollection<T> list)
    {
        var length = list.Count;
        var remainder = 0;

        while (length > Threshold)
        {
            // If length is odd.
            if (length % 2 != 0)
                remainder = 1;

            length = (int)Math.Floor((decimal)(length / 2.0));
        }

        return length + remainder;
    }
}
