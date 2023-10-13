using Xunit.Abstractions;

namespace ExtraRandom.UnitTests.Util;

public class ResultOutputHelper
{
    private readonly ITestOutputHelper _output;
    private bool _recordAmountOfGeneratedNumbers;

    public ResultOutputHelper(ITestOutputHelper output, bool recordAmountOfGeneratedNumbers)
    {
        _output = output;
        _recordAmountOfGeneratedNumbers = recordAmountOfGeneratedNumbers;
    }

    public void RecordHit<T>(T number, ref SortedDictionary<T, int> generateHits)
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

    public void PrintHits<T>(ref SortedDictionary<T, int> generateHits)
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
