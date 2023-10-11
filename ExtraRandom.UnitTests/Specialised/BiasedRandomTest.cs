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
    private const int Loops = 5_000;

    private readonly ITestOutputHelper _output;

    public BiasedRandomTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [ClassData(typeof(PRNGTestData))]
#pragma warning disable S2699 // There is nothing to really test here, just need to see if it generates stuff.
    public void BiasedTest_Long_Lower(IRandom rand)
#pragma warning restore S2699
    {
        Generate_Long(rand, Bias.Lower);
    }

    [Theory]
    [ClassData(typeof(PRNGTestData))]
#pragma warning disable S2699 // There is nothing to really test here, just need to see if it generates stuff.
    public void BiasedTest_Long_Average(IRandom rand)
#pragma warning restore S2699
    {
        Generate_Long(rand, Bias.Average);
    }

    [Theory]
    [ClassData(typeof(PRNGTestData))]
#pragma warning disable S2699 // There is nothing to really test here, just need to see if it generates stuff.
    public void BiasedTest_Long_Higher(IRandom rand)
#pragma warning restore S2699
    {
        Generate_Long(rand, Bias.Higher);
    }

    [Theory]
    [ClassData(typeof(PRNGTestData))]
#pragma warning disable S2699 // There is nothing to really test here, just need to see if it generates stuff.
    public void BiasedTest_Double_Lower(IRandom rand)
#pragma warning restore S2699
    {
        Generate_Double(rand, Bias.Lower);
    }

    [Theory]
    [ClassData(typeof(PRNGTestData))]
#pragma warning disable S2699 // There is nothing to really test here, just need to see if it generates stuff.
    public void BiasedTest_Double_Average(IRandom rand)
#pragma warning restore S2699
    {
        Generate_Double(rand, Bias.Average);
    }

    [Theory]
    [ClassData(typeof(PRNGTestData))]
#pragma warning disable S2699 // There is nothing to really test here, just need to see if it generates stuff.
    public void BiasedTest_Double_Higher(IRandom rand)
#pragma warning restore S2699
    {
        Generate_Double(rand, Bias.Higher);
    }

    private void Generate_Long(IRandom rand, Bias bias)
    {
        var generateHits = new SortedDictionary<long, int>();
        const long min = 0;
        const long max = 30;

        var biased = new BiasedRandom(rand, bias, 5);
        for (var i = 0; i < Loops; i++)
        {
            var @long = biased.NextLong(min, max);
            Assert.InRange(@long, min, max);

            RecordHit(@long, ref generateHits);
        }

        PrintHits(ref generateHits);
    }

    private void Generate_Double(IRandom rand, Bias bias)
    {
        var generateHits = new SortedDictionary<double, int>();
        const double min = 0;
        const double max = 30;

        var biased = new BiasedRandom(rand, bias, 5);
        for (var i = 0; i < Loops; i++)
        {
            var @double = biased.NextDouble(min, max);
            Assert.InRange(@double, min, max);

            RecordHit(@double, ref generateHits);
        }

        PrintHits(ref generateHits);
    }

    private void RecordHit<T>(T number, ref SortedDictionary<T, int> generateHits)
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
