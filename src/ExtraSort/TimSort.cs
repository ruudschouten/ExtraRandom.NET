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
        for (var startPos = start; startPos < list.Count; startPos += runLength)
        {
            // ENHANCEMENT: Run this parallel.
            var endPos = Math.Min(list.Count, startPos + runLength);
            list.Sort(new BinaryInsertionSort(), startPos, endPos);
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
