using System.Collections;
using ExtraSort;

namespace ExtraRandom.UnitTests.Util;

/// <summary>
/// Test data containing all sorting algorithms.
/// </summary>
/// <typeparam name="T">Type of elements in the list that is to be sorted.</typeparam>
public class SortFunctionData<T> : IEnumerable<object[]>
    where T : IComparable<T>
{
    /// <summary>
    /// Method used to determine what sort function to use.
    /// </summary>
    /// <returns>A collection of sorts which are used by xUnit.</returns>
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            "Merge Sort",
            new Func<IList<T>, IList<T>>(x => x.MergeSort())
        };
        yield return new object[]
        {
            "Tim Sort",
            new Func<IList<T>, IList<T>>(x =>
            {
                x.TimSort();
                return x;
            })
        };
        yield return new object[]
        {
            "Insertion Sort",
            new Func<IList<T>, IList<T>>(x =>
            {
                x.InsertionSort();
                return x;
            })
        };
        yield return new object[]
        {
            "Binary Insertion Sort",
            new Func<IList<T>, IList<T>>(x =>
            {
                x.BinaryInsertionSort();
                return x;
            })
        };
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
