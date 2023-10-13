namespace ExtraSort;

/// <summary>
/// Extension method for <see cref="IList{T}"/> to add MergeSort.
/// </summary>
public static class MergeSortExtension
{
    /// <summary>
    /// Perform a MergeSort on the <paramref name="list"/>.
    /// </summary>
    /// <param name="list">List to perform the MergeSort on.</param>
    /// <typeparam name="T">Type of elements in the <paramref name="list"/>.</typeparam>
    /// <returns>The <paramref name="list"/> sorted.</returns>
    public static IList<T> MergeSort<T>(this IList<T> list)
        where T : IComparable<T>
    {
        var sorted = Sort(list, 0, list.Count - 1);
        return sorted;
    }

    private static IList<T> Sort<T>(this IList<T> list, int start, int end)
        where T : IComparable<T>
    {
        // When there is just one element left, return a new collection with just this element.
        if (start >= end)
        {
            return new[] { list[start] };
        }

        var middle = (end + start) / 2;
        var left = list.Sort(start, middle);
        var right = list.Sort(middle + 1, end);
        return Merge(left, right);
    }

    private static IList<T> Merge<T>(IList<T> left, IList<T> right)
        where T : IComparable<T>
    {
        var leftCount = left.Count;
        var rightCount = right.Count;
        var merged = new T[leftCount + rightCount];

        var leftIndex = 0;
        var rightIndex = 0;
        var mergedIndex = 0;

        // Traverse both collections simultaneously and store the smallest element of both to the merged collection.
        while (leftIndex < leftCount && rightIndex < rightCount)
        {
            var compared = left[leftIndex].CompareTo(right[rightIndex]);
            merged[mergedIndex++] = compared < 0 ? left[leftIndex++] : right[rightIndex++];
        }

        // Append any leftovers from either list.
        while (leftIndex < leftCount)
        {
            merged[mergedIndex++] = left[leftIndex++];
        }

        while (rightIndex < rightCount)
        {
            merged[mergedIndex++] = right[rightIndex++];
        }

        return merged;
    }
}