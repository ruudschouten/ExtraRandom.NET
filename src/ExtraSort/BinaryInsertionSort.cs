using ExtraUtil;

namespace ExtraSort;

/// <summary>
/// Binary insertion sort algorithm.
/// </summary>
public record BinaryInsertionSort : ISortAlgorithm
{
    /// <inheritdoc />
    public int EndOffset => 0;

    /// <inheritdoc />
    public void Sort<T>(ref readonly IList<T> list, int start, int end)
        where T : IComparable<T>
    {
        for (var index = start; index < end; index++)
        {
            var value = list[index];
            var pos = list.BinarySearch(value, start, index - 1);

            for (var j = index - 1; j >= pos; --j)
            {
                list[j + 1] = list[j];
            }

            list[pos] = value;
        }
    }
}
