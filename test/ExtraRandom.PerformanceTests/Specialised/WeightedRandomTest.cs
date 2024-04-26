using System.Diagnostics.CodeAnalysis;
using ExtraRandom.Specialised;
using ExtraRandom.TestHelper;
using Xunit.Abstractions;

namespace ExtraRandom.PerformanceTests.Specialised;

[SuppressMessage(
    "Blocker Code Smell",
    "S2699:Tests should include assertions",
    Justification = "These are performance tests."
)]
public class WeightedRandomTest(ITestOutputHelper output)
{
    private const int Loops = 100_000;

    [Theory]
    [ClassData(typeof(PRNGRandoms))]
    public void Weightedy_Adding_Test(IRandom rand)
    {
        var weightedRandom = new WeightedRandom<string>(rand);

        for (var i = 0; i < Loops; i++)
        {
            weightedRandom.Add($"{i}", rand.NextByte());
        }

        var firstRoll = weightedRandom.NextWithWeight(out var roll);
        output.WriteLine($"{firstRoll} - {roll}");
    }

    [Theory]
    [ClassData(typeof(PRNGRandoms))]
    public void Weighted_Speed_Test(IRandom rand)
    {
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

        for (var i = 0; i < Loops; i++)
        {
            weightedRandom.Next();
        }
    }

    [Theory]
    [ClassData(typeof(PRNGRandoms))]
    public void Weighted_Rolls_Test(IRandom rand)
    {
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

        for (var i = 0; i < Loops; i++)
        {
            valuesToPicks[weightedRandom.Next()]++;
        }
    }
}
