namespace ExtraSort;

/// <summary>
/// Extension method for <see cref="IList{T}"/> by add Binary Insertion Sort.
/// </summary>
public static class BinaryInsertionSortExtension
{
    /// <summary>
    /// Perform a Binary Insertion Sort on the <paramref name="list"/>.
    /// </summary>
    /// <param name="list">List to perform the Binary Insertion Sort on.</param>
    /// <typeparam name="T">Type of elements in the <paramref name="list"/>.</typeparam>
    public static void BinaryInsertionSort<T>(this IList<T> list)
        where T : IComparable<T>
    {
        list.BinaryInsertionSort(0, list.Count);
    }

    /// <summary>
    /// Perform a partial Binary Insertion Sort on a section of the <paramref name="list"/>.
    /// </summary>
    /// <param name="list">List to perform the Insertion Sort on.</param>
    /// <param name="start">Starting index for the range.</param>
    /// <param name="end">Exclusive end index to stop sorting on.</param>
    /// <typeparam name="T">Type of elements in the <paramref name="list"/>.</typeparam>
    public static void BinaryInsertionSort<T>(this IList<T> list, int start, int end)
        where T : IComparable<T>
    {
        for (var index = start; index < end; index++)
        {
            var value = list[index];
            var pos = list.BinarySearch(value, start, index - 1);

            for (var j = index - 1; j >= pos; --j)
            {
                list[j + 1] = list[j];
            }

            list[pos] = value;
        }
    }
}
