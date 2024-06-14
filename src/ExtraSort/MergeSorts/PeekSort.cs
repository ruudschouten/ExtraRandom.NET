namespace ExtraSort.MergeSorts;

/// <summary>
/// Peek sort.
/// </summary>
public class PeekSort : MergeSortVariant
{
    private const int InsertionSortThreshold = 10;

    /// <inheritdoc />
    public override int EndOffset => 1;

    /// <inheritdoc />
    public override void Sort<T>(ref readonly IList<T> list, int start, int end)
    {
        var n = end - start + 1;
        var buffer = new T[n];
        Sort(list, new Run(start, end), new Run(start, end), buffer);
    }

    private void Sort<T>(IList<T> list, Run current, Run next, IList<T> buffer)
        where T : IComparable<T>
    {
        if (next.Start == current.End || next.End == current.Start)
            return;

        if (current.End - current.Start + 1 <= InsertionSortThreshold)
        {
            InsertionSort.Sort(list, current.Start, current.End, next.Start - current.Start + 1);
            return;
        }

        var mid = current.Start + ((current.End - current.Start) >> 1);
        if (mid <= next.Start)
        {
            // |XXXXXXXX|XX     X|
            HandleLeftRun(list, current, next, buffer);
        }
        else if (mid >= next.End)
        {
            // |XX     X|XXXXXXXX|
            HandleRightRun(list, current, next, buffer);
        }
        else
        {
            // |XX     x|xxxx   X| or |XX   xxx|x      X|
            HandleMiddleRun(list, current, next, mid, buffer);
        }
    }

    private void HandleMiddleRun<T>(IList<T> list, Run current, Run next, int mid, IList<T> buffer)
        where T : IComparable<T>
    {
        var middleRun = FindMiddleRun(list, current, next, mid);

        if (middleRun.Start == current.Start && middleRun.End == current.End)
            return;

        if (mid - middleRun.Start < middleRun.End - mid)
        {
            // |XX     x|xxxx   X|
            Sort(
                list,
                new Run(current.Start, middleRun.Start - 1),
                new Run(next.Start, middleRun.Start - 1),
                buffer
            );
            Sort(
                list,
                new Run(middleRun.Start, current.End),
                new Run(middleRun.End, next.End),
                buffer
            );
            MergeRuns(list, current.Start, middleRun.Start, current.End, buffer);
        }
        else
        {
            // |XX   xxx|x      X|
            Sort(
                list,
                new Run(current.Start, middleRun.End),
                new Run(next.Start, middleRun.Start),
                buffer
            );
            Sort(
                list,
                new Run(middleRun.End + 1, current.End),
                new Run(middleRun.End + 1, next.End),
                buffer
            );
            MergeRuns(list, current.Start, middleRun.End + 1, current.End, buffer);
        }
    }

    private void HandleRightRun<T>(IList<T> list, Run current, Run next, IList<T> buffer)
        where T : IComparable<T>
    {
        Sort(list, new Run(current.Start, next.End - 1), new Run(next.Start, next.End - 1), buffer);
        MergeRuns(list, current.Start, next.End, current.End, buffer);
    }

    private void HandleLeftRun<T>(IList<T> list, Run current, Run next, IList<T> buffer)
        where T : IComparable<T>
    {
        Sort(list, new Run(next.Start + 1, current.End), new Run(next.Start + 1, next.End), buffer);
        MergeRuns(list, current.Start, next.Start + 1, current.End, buffer);
    }

    private static Run FindMiddleRun<T>(IList<T> list, Run current, Run next, int mid)
        where T : IComparable<T>
    {
        int startRun,
            endRun;
        var middleRunStart = current.Start == next.Start ? current.Start : next.Start + 1;
        var middleRunEnd = current.End == next.End ? current.End : next.End - 1;

        var isEndOfNextRun = mid + 1 == next.End;
        // find middle run
        if (list[mid].CompareTo(list[mid + 1]) <= 0)
        {
            startRun = ExtendWeaklyIncreasingRunLeft(list, mid, middleRunStart);
            endRun = isEndOfNextRun
                ? mid
                : ExtendWeaklyIncreasingRunRight(list, mid + 1, middleRunEnd);
        }
        else
        {
            startRun = ExtendStrictlyDecreasingRunLeft(list, mid, middleRunStart);
            endRun = isEndOfNextRun
                ? mid
                : ExtendStrictlyDecreasingRunRight(list, mid + 1, middleRunEnd);
            ReverseRange(list, startRun, endRun);
        }

        return new Run(startRun, endRun);
    }
}
