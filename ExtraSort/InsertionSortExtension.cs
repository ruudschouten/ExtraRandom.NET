namespace ExtraSort;

/// <summary>
/// Extension method for <see cref="IList{T}"/> to add Insertion Sort.
/// </summary>
public static class InsertionSortExtension
{
    /// <summary>
    /// Perform a Insertion Sort on the <paramref name="list"/>.
    /// </summary>
    /// <param name="list">List to perform the Insertion Sort on.</param>
    /// <typeparam name="T">Type of elements in the <paramref name="list"/>.</typeparam>
    public static void InsertionSort<T>(this IList<T> list)
        where T : IComparable<T>
    {
        list.InsertionSort(0, list.Count);
    }

    /// <summary>
    /// Perform a partial Insertion Sort on a section of the <paramref name="list"/>.
    /// </summary>
    /// <param name="list">List to perform the Insertion Sort on.</param>
    /// <param name="start">Starting index for the range.</param>
    /// <param name="end">Exclusive end index for the range.</param>
    /// <typeparam name="T">Type of elements in the <paramref name="list"/>.</typeparam>
    public static void InsertionSort<T>(this IList<T> list, int start, int end)
        where T : IComparable<T>
    {
        for (var i = start + 1; i < end; i++)
        {
            var temp = list[i];
            var j = i - 1;
            while (j >= start && list[j].CompareTo(temp) > 0)
            {
                list[j + 1] = list[j];
                j--;
            }

            list[j + 1] = temp;
        }
    }
}
