using ExtraUtil;

namespace ExtraSort;

/// <summary>
/// TimSort.
/// </summary>
/// <remarks>
/// <para>
/// Based on: https://www.baeldung.com/cs/timsort.
/// </para>
/// </remarks>
public readonly record struct TimSort : ISortAlgorithm
{
    private const int Threshold = 8;

    /// <inheritdoc />
    public int EndOffset => 0;

    /// <inheritdoc />
    public void Sort<T>(ref readonly IList<T> list, int start, int end)
        where T : IComparable<T>
    {
        // Calculate the run length.
        var runLength = CalculateRunLength(list);

        // Perform insertion sort on each run
        for (var startRun = start; startRun < end; startRun += runLength)
        {
            // ENHANCEMENT: Run this parallel.
            var endRun = Math.Min(list.Count, startRun + runLength);
            list.Sort(new BinaryInsertionSort(), startRun, endRun);
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
                    list = MergeSort(list, left, mid, right);
                }

                // ENHANCEMENT: Apply galloping somewhere in/after the merge.
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

    /// <summary>
    /// <para>Perform a partial MergeSort where you have to specify the middle index as well.</para>
    /// </summary>
    /// <param name="list">List to perform the Merge Sort on.</param>
    /// <param name="start">Starting index for the range.</param>
    /// <param name="middle">Middle index.</param>
    /// <param name="end">Inclusive end index for the range.</param>
    /// <typeparam name="T">Type of elements in the <paramref name="list"/>.</typeparam>
    /// <returns>The <paramref name="list"/> sorted.</returns>
    private static IList<T> MergeSort<T>(IList<T> list, int start, int middle, int end)
        where T : IComparable<T>
    {
        var left = list.Slice(start, middle).ToList();
        var right = list.Slice(middle + 1, end).ToList();

        var leftIndex = 0;
        var rightIndex = 0;

        while (leftIndex < left.Count && rightIndex < right.Count)
        {
            var leftValue = left[leftIndex];
            var rightValue = right[rightIndex];
            if (leftValue.CompareTo(rightValue) < 0)
            {
                list[start + leftIndex + rightIndex] = leftValue;
                leftIndex++;
            }
            else
            {
                list[start + leftIndex + rightIndex] = rightValue;
                rightIndex++;
            }
        }

        for (; leftIndex < left.Count; leftIndex++)
        {
            list[start + leftIndex + rightIndex] = left[leftIndex];
        }

        for (; rightIndex < right.Count; rightIndex++)
        {
            list[start + leftIndex + rightIndex] = right[rightIndex];
        }

        return list;
    }
}
