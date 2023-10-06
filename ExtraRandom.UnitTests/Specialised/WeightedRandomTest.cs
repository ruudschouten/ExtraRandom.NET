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

    [Fact]
    public void WeightedTest()
    {
        var weightedRandom = new WeightedRandom<string>(500);
        weightedRandom.Add("Tiny", 1);
        weightedRandom.Add("Small", 10);
        weightedRandom.Add("Big", 50);

        const int rolls = 100_000;
        var valuesToPicks = new Dictionary<string, int>
        {
            { "Big", 0 },
            { "Small", 0 },
            { "Tiny", 0 }
        };
        for (var i = 0; i < rolls; i++)
        {
            var roll = weightedRandom.Next();
            valuesToPicks[roll!.Value]++;
        }

        foreach (var valuesToPick in valuesToPicks)
        {
            _output.WriteLine($"{valuesToPick.Key} - {valuesToPick.Value}");
        }
    }
}
