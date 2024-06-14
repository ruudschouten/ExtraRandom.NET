namespace ExtraSort.MergeSorts;

/// <summary>
/// Sorting algorithms which are variants on Merge Sort.
/// </summary>
public abstract class MergeSortVariant : ISortAlgorithm
{
    /// <inheritdoc />
    public abstract int EndOffset { get; }

    /// <inheritdoc />
    public abstract void Sort<T>(ref readonly IList<T> list, int start, int end)
        where T : IComparable<T>;

    /// <summary>
    /// Merges runs <c>list[startX...startY-1]</c> and <c>list[startY...endY]</c> in-place into <c>list[startX...endY]</c>
    /// by copying the shorter run into <paramref name="temp"/> and merging back into <paramref name="list"/>.
    /// B.length must be at least min(m-l,r-m+1)
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the temp count is smaller than the endY value + 1.</exception>
    protected static void MergeRuns<T>(
        IList<T> list,
        int startX,
        int startY,
        int endY,
        IList<T> temp
    )
        where T : IComparable<T>
    {
        --startY; // mismatch in convention with Sedgewick
        int i,
            j;

        if (temp.Count < endY + 1)
            throw new InvalidOperationException("MergeRuns: temp.Count < endY + 1");

        for (i = startY + 1; i > startX; --i)
            temp[i - 1] = list[i - 1];
        for (j = startY; j < endY; ++j)
            temp[endY + startY - j] = list[j + 1];
        for (var k = startX; k <= endY; ++k)
            list[k] = temp[j].CompareTo(temp[i]) < 0 ? temp[j--] : temp[i++];
    }

    /// <summary>
    /// Determine the length of a run.
    /// If the value at list[i] is smaller than the value at list[i + 1], then the run is ascending.
    /// Otherwise, the run is descending.
    /// </summary>
    /// <returns>Index at which a new run should end.</returns>
    protected static int ExtendAndReverseRunRight<T>(IList<T> list, int i, int right)
        where T : IComparable<T>
    {
        var j = i;
        if (j == right)
            return j;
        if (list[j].CompareTo(list[++j]) > 0)
        {
            ExtendStrictlyDecreasingRunRight(list, i, right);
            ReverseRange(list, i, j);
        }
        else
        {
            ExtendWeaklyIncreasingRunRight(list, i, right);
        }

        return j;
    }

    /// <summary>
    /// Extends a weakly increasing run to the left in a list.
    /// </summary>
    /// <returns>Start index of the run.</returns>
    protected static int ExtendWeaklyIncreasingRunLeft<T>(IList<T> list, int i, int left)
        where T : IComparable<T>
    {
        while (i > left && list[i - 1].CompareTo(list[i]) <= 0)
            --i;
        return i;
    }

    /// <summary>
    /// Extends a weakly increasing run to the right in a list.
    /// </summary>
    /// <returns>End index of the run.</returns>
    protected static int ExtendWeaklyIncreasingRunRight<T>(IList<T> list, int i, int right)
        where T : IComparable<T>
    {
        while (i < right && list[i + 1].CompareTo(list[i]) >= 0)
            ++i;
        return i;
    }

    /// <summary>
    /// Extends a strictly increasing run to the left in a list.
    /// </summary>
    /// <returns>Start index of the run.</returns>
    protected static int ExtendStrictlyDecreasingRunLeft<T>(IList<T> list, int i, int left)
        where T : IComparable<T>
    {
        while (i > left && list[i - 1].CompareTo(list[i]) > 0)
            --i;
        return i;
    }

    /// <summary>
    /// Extends a strictly increasing run to the right in a list.
    /// </summary>
    /// <returns>End index of the run.</returns>
    protected static int ExtendStrictlyDecreasingRunRight<T>(IList<T> list, int i, int right)
        where T : IComparable<T>
    {
        while (i < right && list[i + 1].CompareTo(list[i]) < 0)
            ++i;
        return i;
    }

    /// <summary>
    /// Reverses a range in a list.
    /// </summary>
    protected static void ReverseRange<T>(IList<T> list, int start, int end)
    {
        while (start < end)
        {
            var t = list[start];
            list[start++] = list[end];
            list[end--] = t;
        }
    }
}
