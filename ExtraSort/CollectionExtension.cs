namespace ExtraSort;

/// <summary>
/// Contains extension methods for Collections.
/// </summary>
public static class CollectionExtension
{
    /// <summary>
    /// Take a slice of the <paramref name="collection"/>, starting with the <paramref name="startIndex"/> until the <paramref name="endIndex"/>
    /// </summary>
    /// <param name="collection">Collection to take a slice from.</param>
    /// <param name="startIndex">
    /// Inclusive index to start from.
    /// <para>If lower than 0, is set to 0.</para>
    /// </param>
    /// <param name="endIndex">
    /// Inclusive index to end on.
    /// <para>
    /// If higher than amount of items in <paramref name="collection"/>, is set to the amount of items.</para>
    /// </param>
    /// <typeparam name="T">Type of the entity inside the <paramref name="collection"/>.</typeparam>
    /// <returns>A slice from <paramref name="collection"/>.</returns>
    /// <example>
    /// In this example; <c>list</c> has 10 elements.
    /// <code>
    /// list.Slice(0, 4);           // TAKES: 0, 1, 2, 3, 4   || SKIPS: 5, 6, 7, 8, 9
    /// list.Slice(1, 4);           // TAKES: 1, 2, 3, 4      || SKIPS: 0, 5, 6, 7, 8, 9
    /// list.Slice(5, 7);           // TAKES: 5, 6, 7         || SKIPS: 0, 1, 2, 3, 4, 8, 9
    /// list.Slice(7, list.Count);  // TAKES: 7, 8, 9         || SKIPS: 0, 1, 2, 3, 4, 5, 6
    /// </code>
    /// </example>
    public static IEnumerable<T> Slice<T>(this IList<T> collection, int startIndex, int endIndex)
    {
        if (startIndex < 0)
        {
            startIndex = 0;
        }

        if (endIndex > collection.Count)
        {
            endIndex = collection.Count;
        }

        var itemsToSkip = startIndex;
        var itemsToTake = (endIndex + 1) - itemsToSkip;

        return collection.Skip(itemsToSkip).Take(itemsToTake);
    }

    /// <summary>
    /// Perform a binary search to find the index of the <paramref name="item"/> in the <paramref name="list"/>.
    /// </summary>
    /// <param name="list">List to search in.</param>
    /// <param name="item">Item to search the index for.</param>
    /// <typeparam name="T">Type of the entity inside the <paramref name="list"/>.</typeparam>
    /// <remarks>This will only work correctly on a sorted list.</remarks>
    /// <returns>Index of the <paramref name="item"/>.
    /// <para>
    /// If the <paramref name="item"/> could not be found, an index is returned in which the <paramref name="item"/> should be at.
    /// </para>
    /// </returns>
    public static int BinarySearch<T>(this IList<T> list, T item)
        where T : IComparable<T>
    {
        return BinarySearch(list, item, 0, list.Count - 1);
    }

    /// <summary>
    /// Perform a binary search to find the index of the <paramref name="item"/> in the <paramref name="list"/>, between the given indexes.
    /// </summary>
    /// <param name="list">List to search in.</param>
    /// <param name="item">Item to search the index for.</param>
    /// <param name="start">Start index to search from.</param>
    /// <param name="end">End index to stop searching on.</param>
    /// <typeparam name="T">Type of the entity inside the <paramref name="list"/>.</typeparam>
    /// <remarks>This will only work correctly on a sorted list.</remarks>
    /// <returns>Index of the <paramref name="item"/>.
    /// <para>
    /// If the <paramref name="item"/> could not be found, an index is returned in which the <paramref name="item"/> should be at.
    /// </para>
    /// </returns>
    public static int BinarySearch<T>(this IList<T> list, T item, int start, int end)
        where T : IComparable<T>
    {
        if (start == end)
        {
            if (list[start].CompareTo(item) > 0)
                return start;
            return start + 1;
        }

        if (start > end)
            return start;

        var middle = (start + end) / 2;

        return list[middle].CompareTo(item) switch
        {
            < 0 => list.BinarySearch(item, middle + 1, end),
            > 0 => list.BinarySearch(item, start, middle - 1),
            _ => middle
        };
    }
}
