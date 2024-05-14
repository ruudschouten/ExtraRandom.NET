using System.Diagnostics.CodeAnalysis;
using ExtraRandom.Specialised;
using ExtraRandom.TestHelper;
using FluentAssertions;
using Xunit.Abstractions;

namespace ExtraRandom.UnitTests.Specialised;

[SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1600:Elements should be documented",
    Justification = "These are tests"
)]
#pragma warning disable S2699 // There is nothing to really test here, just need to see if it generates stuff.
public class BiasedRandomTest : ResultOutputHelper
{
    private const int Loops = 5_000;

    public BiasedRandomTest(ITestOutputHelper output)
        : base(output, recordAmountOfGeneratedNumbers: true) { }

    [Theory]
    [ClassData(typeof(PRNGRandoms))]
    public void BiasedTest_Long_Lower(IRandom rand)
    {
        Generate_Long(rand, Bias.Lower);
    }

    [Theory]
    [ClassData(typeof(PRNGRandoms))]
    public void BiasedTest_Long_Average(IRandom rand)
    {
        Generate_Long(rand, Bias.Average);
    }

    [Theory]
    [ClassData(typeof(PRNGRandoms))]
    public void BiasedTest_Long_Higher(IRandom rand)
    {
        Generate_Long(rand, Bias.Higher);
    }

    [Theory]
    [ClassData(typeof(PRNGRandoms))]
    public void BiasedTest_Long_GoldenRatio(IRandom rand)
    {
        Generate_Long(rand, Bias.GoldenRatio);
    }

    [Theory]
    [ClassData(typeof(PRNGRandoms))]
    public void BiasedTest_Double_Lower(IRandom rand)
    {
        Generate_Double(rand, Bias.Lower);
    }

    [Theory]
    [ClassData(typeof(PRNGRandoms))]
    public void BiasedTest_Double_Average(IRandom rand)
    {
        Generate_Double(rand, Bias.Average);
    }

    [Theory]
    [ClassData(typeof(PRNGRandoms))]
    public void BiasedTest_Double_Higher(IRandom rand)
    {
        Generate_Double(rand, Bias.Higher);
    }

    [Theory]
    [ClassData(typeof(PRNGRandoms))]
    public void BiasedTest_Double_GoldenRatio(IRandom rand)
    {
        Generate_Double(rand, Bias.GoldenRatio);
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
            @long.Should().BeInRange(min, max);

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
            @double.Should().BeInRange(min, max);

            RecordHit(@double, ref generateHits);
        }

        PrintHits(ref generateHits);
    }
}

#pragma warning restore S2699
