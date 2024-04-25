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
        for (var index = start + 1; index < end; index++)
        {
            var temp = list[index];
            var secondaryIndex = index - 1;
            while (secondaryIndex >= start && list[secondaryIndex].CompareTo(temp) > 0)
            {
                list[secondaryIndex + 1] = list[secondaryIndex];
                secondaryIndex--;
            }

            list[secondaryIndex + 1] = temp;
        }
    }
}
