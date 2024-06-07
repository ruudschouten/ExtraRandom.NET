namespace ExtraSort;

/// <summary>
/// Insertion sort.
/// </summary>
public record InsertionSort : ISortAlgorithm
{
    /// <inheritdoc />
    public int EndOffset => 0;
    /// <inheritdoc />
    public void Sort<T>(ref readonly IList<T> list, int start, int end) where T : IComparable<T>
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
}