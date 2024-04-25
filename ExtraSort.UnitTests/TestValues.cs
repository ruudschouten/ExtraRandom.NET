namespace ExtraSort.UnitTests;

/// <summary>
/// Class which contains data values for testing sorting algorithms.
/// </summary>
public static class TestValues
{
    /// <summary>
    /// Collection of numbers that are not sorted.
    /// </summary>
    public static readonly IList<int> UnsortedNumbers = new[]
    {
        19,
        22,
        19,
        22,
        24,
        25,
        17,
        11,
        22,
        23,
        28,
        23,
        0,
        1,
        12,
        9,
        13,
        27,
        15
    };

    /// <summary>
    /// Collection of numbers that are sorted.
    /// </summary>
    public static readonly IReadOnlyList<int> SortedNumbers = new[]
    {
        0,
        1,
        9,
        11,
        12,
        13,
        15,
        17,
        19,
        19,
        22,
        22,
        22,
        23,
        23,
        24,
        25,
        27,
        28
    };
}
