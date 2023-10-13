namespace ExtraSort;

/// <summary>
/// Extension method for <see cref="IList{T}"/> to add Insertion Sort.
/// </summary>
public static class InsertionSortExtension
{
    /// <summary>
    /// Perform a InsertionSort on the <paramref name="list"/>.
    /// </summary>
    /// <param name="list">List to perform the MergeSort on.</param>
    /// <typeparam name="T">Type of elements in the <paramref name="list"/>.</typeparam>
    public static void InsertionSort<T>(this IList<T> list)
        where T : IComparable<T>
    {
        for (var i = 1; i < list.Count; i++)
        {
            var j = i;
            while (j > 0 && list[j - 1].CompareTo(list[j]) > 0)
            {
                // Swap elements.
                (list[j - 1], list[j]) = (list[j], list[j - 1]);
                j--;
            }
        }
    }
}
