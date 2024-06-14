namespace ExtraSort;

/// <summary>
/// Insertion sort.
/// </summary>
public readonly record struct InsertionSort : ISortAlgorithm
{
    /// <inheritdoc />
    public int EndOffset => 0;

    /// <inheritdoc />
    public void Sort<T>(ref readonly IList<T> list, int start, int end)
        where T : IComparable<T>
    {
        for (var index = start + 1; index < end; index++)
        {
            var temp = list[index];
            var secondaryIndex = index - 1;
            while (secondaryIndex >= start && list[secondaryIndex].CompareTo(temp) > 0)
            {
                list[secondaryIndex + 1] = list[secondaryIndex];
                secondaryIndex--;
            }

            list[secondaryIndex + 1] = temp;
        }
    }

    /// <summary>
    /// Perform an insertion sort on a list with an offset which will skip over the first <paramref name="offset"/> elements, after <paramref name="left"/>.
    /// This is useful if you know the first <paramref name="offset"/> elements are already sorted.
    /// </summary>
    public static void Sort<T>(IList<T> list, int left, int right, int offset)
        where T : IComparable<T>
    {
        for (var index = left + offset; index <= right; ++index)
        {
            var temp = list[index];
            var secondaryIndex = index - 1;
            while (temp.CompareTo(list[secondaryIndex]) < 0)
            {
                list[secondaryIndex + 1] = list[secondaryIndex];
                --secondaryIndex;
                if (secondaryIndex < left)
                    break;
            }

            list[secondaryIndex + 1] = temp;
        }
    }
}
