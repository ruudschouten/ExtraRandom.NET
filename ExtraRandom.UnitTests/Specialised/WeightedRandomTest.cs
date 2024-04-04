using ExtraRandom.PRNG;
using ExtraRandom.Specialised;
using ExtraRandom.TestHelper;
using FluentAssertions;
using Xunit.Abstractions;

namespace ExtraRandom.UnitTests.Specialised;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1600:Elements should be documented",
    Justification = "These are tests"
)]
#pragma warning disable S2699 // There is nothing to really test here, just need to see if it generates stuff.
public class WeightedRandomTest
{
    private readonly ITestOutputHelper _output;

    public WeightedRandomTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [ClassData(typeof(PRNGTestData))]
    public void Weighted_Rolls_Test(IRandom rand)
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

    [Theory]
    [ClassData(typeof(PRNGTestData))]
    public void Weighted_Speed_Test(IRandom rand)
    {
        const int rolls = 1_000_000;

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
            weightedRandom.Next();
        }
    }

    [Theory]
    [ClassData(typeof(PRNGTestData))]
    public void Weighted_Adding_Test(IRandom rand)
    {
        const int numbersToAdd = 1_000_000;

        var weightedRandom = new WeightedRandom<string>(rand);

        for (var i = 0; i < numbersToAdd; i++)
        {
            weightedRandom.Add($"{i}", rand.NextByte());
        }

        var firstRoll = weightedRandom.NextWithWeight(out var roll);
        _output.WriteLine($"{firstRoll} - {roll}");
    }

    [Theory]
    [ClassData(typeof(SortFunctionData<WeightedRandomEntry<string>>))]
    public void Weighted_Sort_Test(
        string name,
        Func<IList<WeightedRandomEntry<string>>, IList<WeightedRandomEntry<string>>> sortFunction
    )
    {
        _output.WriteLine($"Sorting with {name}");

        var weightedRandom = new WeightedRandom<string>(new RomuDuoJr(500));
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

        var sorted = weightedRandom.ManualSort(sortFunction);

        sorted
            .Should()
            .ContainInOrder(
                new WeightedRandomEntry<string>("0", 0),
                new WeightedRandomEntry<string>("1", 1),
                new WeightedRandomEntry<string>("5", 5),
                new WeightedRandomEntry<string>("10", 10),
                new WeightedRandomEntry<string>("17", 17),
                new WeightedRandomEntry<string>("20", 20),
                new WeightedRandomEntry<string>("25", 25),
                new WeightedRandomEntry<string>("45", 45),
                new WeightedRandomEntry<string>("50", 50),
                new WeightedRandomEntry<string>("125", 125)
            );
    }
}
#pragma warning restore S2699
