namespace ExtraSort.MergeSorts;

/// <summary>
/// Sorting algorithm that uses a power function to determine where a section of a merge should be ordered.
/// </summary>
public class PowerSort : MergeSortVariant
{
    private const int MinRunLen = 16;

    /// <inheritdoc />
    public override int EndOffset => 1;

    /// <inheritdoc />
    public override void Sort<T>(ref readonly IList<T> list, int start, int end)
    {
        var n = end - start + 1;
        var lgnPlus2 = Log2(n) + 2;
        var runStack = new Stack<Run>(lgnPlus2);
        var buffer = new T[n];

        var currentRun = FindRun(list, start, end);

        // Iterate over the list to be sorted.
        while (currentRun.End < end)
        {
            var nextRun = FindRun(list, currentRun.End + 1, end);
            var power = NodePower(start, end, currentRun, nextRun);

            while (runStack.Count > power)
            {
                var leftRun = runStack.Pop();
                MergeRuns(list, leftRun.Start, leftRun.End + 1, currentRun.End, buffer);
                currentRun.Start = leftRun.Start;
            }

            runStack.Push(currentRun);
            currentRun = nextRun;
        }

        while (runStack.Count > 0)
        {
            var leftRun = runStack.Pop();
            MergeRuns(list, leftRun.Start, leftRun.End + 1, end, buffer);
        }
    }

    private static Run FindRun<T>(IList<T> list, int start, int end)
        where T : IComparable<T>
    {
        var runEnd = ExtendAndReverseRunRight(list, start, end);
        var runLength = runEnd - start + 1;
        if (runLength >= MinRunLen)
            return new Run(start, runEnd);

        runEnd = Math.Min(end, start + MinRunLen - 1);
        InsertionSort.Sort(list, start, runEnd, runLength);

        return new Run(start, runEnd);
    }

    private static int NodePower(int left, int right, Run first, Run second)
    {
        var n = (right - left + 1);
        var l = (long)first.Start + second.Start - ((long)left << 1);
        var r = (long)second.Start + second.End + 1 - ((long)left << 1);
        var a = (int)((l << 30) / n);
        var b = (int)((r << 30) / n);

        return int.LeadingZeroCount(a ^ b);
    }

    private static int Log2(int n)
    {
        if (n == 0)
            throw new ArgumentException("log2(0) is undefined", nameof(n));
        return 31 - int.LeadingZeroCount(n);
    }
}
