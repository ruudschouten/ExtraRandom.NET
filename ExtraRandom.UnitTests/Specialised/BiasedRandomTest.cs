using System.Diagnostics.CodeAnalysis;
using ExtraRandom.Specialised;
using ExtraRandom.UnitTests.Util;
using Xunit.Abstractions;

namespace ExtraRandom.UnitTests.Specialised;

[SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1600:Elements should be documented",
    Justification = "These are tests"
)]
public class BiasedRandomTest
{
    private const int Loops = 500;

    private readonly ITestOutputHelper _output;

    public BiasedRandomTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [ClassData(typeof(PRNGTestData))]
#pragma warning disable S2699 // There is nothing to really test here, just need to see if it generates stuff.
    public void BiasedTest(IRandom rand)
#pragma warning restore S2699
    {
        var generateHits = new SortedDictionary<long, int>();
        const long min = 0;
        const long max = 30;

        var biased = new BiasedRandom(rand, Bias.Average, 5);
        for (var i = 0; i < Loops; i++)
        {
            var @long = biased.NextLong(min, max);
            Assert.InRange(@long, min, max);

            RecordHit(@long, ref generateHits);
        }

        PrintHits(ref generateHits);
    }

    private static void RecordHit<T>(T number, ref SortedDictionary<T, int> generateHits)
        where T : notnull
    {
        var exists = generateHits.ContainsKey(number);
        if (!exists)
        {
            generateHits.Add(number, 1);
            return;
        }

        generateHits[number]++;
    }

    private void PrintHits<T>(ref SortedDictionary<T, int> generateHits)
        where T : notnull
    {
        foreach (var hits in generateHits)
        {
            _output.WriteLine($"{hits.Key}: {hits.Value}");
        }
    }
}
