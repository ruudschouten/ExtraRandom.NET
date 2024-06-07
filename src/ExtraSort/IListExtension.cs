namespace ExtraSort;

/// <summary>
/// Contains extension methods for <see cref="IList{T}"/> for sorting algorithms.
/// </summary>
// ReSharper disable once InconsistentNaming
public static class IListExtension
{
    /// <summary>
    /// Sorts the list using the provided algorithm.
    /// </summary>
    /// <param name="list">list to sort.</param>
    /// <param name="algorithm">sorting algorithm to use.</param>
    /// <returns>A sorted list.</returns>
    public static void Sort<T>(this IList<T> list, ISortAlgorithm algorithm) where T : IComparable<T>
    {
        algorithm.Sort(ref list, 0, list.Count - algorithm.EndOffset);
    }

    /// <summary>
    /// Partially sorts the list using the provided algorithm and indices.
    /// </summary>
    /// <param name="list">list to sort.</param>
    /// <param name="algorithm">sorting algorithm to use.</param>
    /// <param name="start">index to start sorting on.</param>
    /// <param name="end">index to stop sorting on</param>
    /// <returns>A sorted list.</returns>
    public static void Sort<T>(this IList<T> list, ISortAlgorithm algorithm, int start, int end)
        where T : IComparable<T>
    {
        algorithm.Sort(ref list, start, end);
    }
}