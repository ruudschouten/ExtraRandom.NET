namespace ExtraSort;

/// <summary>
/// Interface for a sort algorithm.
/// </summary>
public interface ISortAlgorithm
{
    /// <summary>
    /// Get or set the end offset, which some algorithms require, or cause out of bounds exceptions.
    /// </summary>
    int EndOffset { get; }

    /// <summary>
    /// Algorithm to sort the <paramref name="list"/>.
    /// </summary>
    void Sort<T>(ref readonly IList<T> list, int start, int end)
        where T : IComparable<T>;
}