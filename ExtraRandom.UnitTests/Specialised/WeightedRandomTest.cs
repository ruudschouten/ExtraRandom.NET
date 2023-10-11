using ExtraRandom.PRNG;
using ExtraRandom.Specialised;
using ExtraRandom.UnitTests.Util;
using Xunit.Abstractions;

namespace ExtraRandom.UnitTests;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1600:Elements should be documented",
    Justification = "These are tests"
)]
public class WeightedRandomTest
{
    private readonly ITestOutputHelper _output;

    public WeightedRandomTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [ClassData(typeof(PRNGTestData))]
#pragma warning disable S2699 // There is nothing to really test here, just need to see if it generates stuff.
    public void WeightedTest(IRandom rand)
#pragma warning restore S2699
    {
        const int rolls = 1_000_000;
        var valuesToPicks = new Dictionary<string, int>
        {
            { "0", 0 },
            { "1", 0 },
            { "5", 0 },
            { "10", 0 },
            { "17", 0 },
            { "20", 0 },
            { "25", 0 },
            { "45", 0 },
            { "50", 0 },
            { "125", 0 }
        };

        var weightedRandom = new WeightedRandom<string>(rand);
        weightedRandom.Add("1", 1);
        weightedRandom.Add("10", 10);
        weightedRandom.Add("50", 50);
        weightedRandom.Add("5", 5);
        weightedRandom.Add("25", 25);
        weightedRandom.Add("125", 125);
        weightedRandom.Add("20", 20);
        weightedRandom.Add("45", 45);
        weightedRandom.Add("0", 0);
        weightedRandom.Add("17", 17);

        for (var i = 0; i < rolls; i++)
        {
            valuesToPicks[weightedRandom.Next()]++;
        }

        foreach (var valuesToPick in valuesToPicks)
        {
            _output.WriteLine($"{valuesToPick.Key} - {valuesToPick.Value}");
        }
    }
}
