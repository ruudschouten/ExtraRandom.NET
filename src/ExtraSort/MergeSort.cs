using System.Collections;
using ExtraUtil;

namespace ExtraSort;

/// <summary>
/// Merge sort algorithm.
/// </summary>
public record MergeSort : ISortAlgorithm
{
    /// <inheritdoc />
    public int EndOffset => 1;

    /// <inheritdoc />
    public void Sort<T>(ref readonly IList<T> list, int start, int end) where T : IComparable<T>
    {
        Sort(list, start, end);
    }

    private static void Sort<T>(IList<T> list, int start, int end) where T : IComparable<T>
    {
        // When there is just one element left, return a new collection with just this element.
        if (start >= end)
        {
            return;
        }

        var middle = (end + start) / 2;
        var left = list.Slice(start, middle).ToList();
        var right = list.Slice(middle + 1, end).ToList();

        Sort(left, 0, left.Count - 1);
        Sort(right, 0, right.Count - 1);

        Merge(list, left, right);
    }

    private static void Merge<T>(IList<T> list, IList<T> left, IList<T> right)
        where T : IComparable<T>
    {
        var leftCount = left.Count;
        var rightCount = right.Count;

        var leftIndex = 0;
        var rightIndex = 0;
        var mergedIndex = 0;

        // Traverse both collections simultaneously and store the smallest element of both to the merged collection.
        while (leftIndex < leftCount && rightIndex < rightCount)
        {
            var compared = left[leftIndex].CompareTo(right[rightIndex]);
            list[mergedIndex++] = compared < 0 ? left[leftIndex++] : right[rightIndex++];
        }

        // Append any leftovers from either list.
        while (leftIndex < leftCount)
        {
            list[mergedIndex++] = left[leftIndex++];
        }

        while (rightIndex < rightCount)
        {
            list[mergedIndex++] = right[rightIndex++];
        }
    }
}