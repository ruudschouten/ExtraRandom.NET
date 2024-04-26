using Xunit.Abstractions;

namespace ExtraRandom.TestHelper;

/// <summary>
/// Helper class for recording and printing out mapped values.
/// </summary>
public class ResultOutputHelper
{
    private readonly ITestOutputHelper _output;
    private readonly bool _recordAmountOfGeneratedNumbers;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResultOutputHelper"/> class.
    /// </summary>
    /// <param name="output"><see cref="ITestOutputHelper"/> instance.</param>
    /// <param name="recordAmountOfGeneratedNumbers">Whether to keep track of generated values.</param>
    protected ResultOutputHelper(ITestOutputHelper output, bool recordAmountOfGeneratedNumbers)
    {
        _output = output;
        _recordAmountOfGeneratedNumbers = recordAmountOfGeneratedNumbers;
    }

    /// <summary>
    /// Record a new value.
    /// </summary>
    /// <param name="number">Value generated.</param>
    /// <param name="generateHits">Dictionary use to store the generated values.</param>
    /// <typeparam name="T">The type that was generated.</typeparam>
    protected void RecordHit<T>(T number, ref SortedDictionary<T, int> generateHits)
        where T : notnull
    {
        if (!_recordAmountOfGeneratedNumbers)
            return;

        var exists = generateHits.ContainsKey(number);
        if (!exists)
        {
            generateHits.Add(number, 1);
            return;
        }

        generateHits[number]++;
    }

    /// <summary>
    /// Print out the generated values.
    /// </summary>
    /// <param name="generateHits">Dictionary which contains the values.</param>
    /// <typeparam name="T">Type of the values.</typeparam>
    protected void PrintHits<T>(ref SortedDictionary<T, int> generateHits)
        where T : notnull
    {
        if (!_recordAmountOfGeneratedNumbers)
            return;

        foreach (var hits in generateHits)
        {
            _output.WriteLine($"{hits.Key}: {hits.Value}");
        }
    }
}
