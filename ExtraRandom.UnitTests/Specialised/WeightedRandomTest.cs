using ExtraRandom.PRNG;
using ExtraRandom.Specialised;
using ExtraSort.TestHelper;
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
public class WeightedRandomTest(ITestOutputHelper output)
{
    [Theory]
    [ClassData(typeof(SortFunctionData<WeightedRandomEntry<string>>))]
    public void Weighted_Sort_Test(
        string name,
        Func<IList<WeightedRandomEntry<string>>, IList<WeightedRandomEntry<string>>> sortFunction
    )
    {
        output.WriteLine($"Sorting with {name}");

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