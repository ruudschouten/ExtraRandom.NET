using System.Diagnostics.CodeAnalysis;
using ExtraRandom.PRNG;
using ExtraRandom.Specialised;

namespace ExtraSort.UnitTests;

[SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1600:Elements should be documented",
    Justification = "These are tests"
)]
#pragma warning disable S2699 // There is nothing to really test here, just need to see if it generates stuff.
public class InsertionSortTest
{
    private const int EntriesToAdd = 50_000;

    [Fact]
    public void InsertionSort()
    {
        var unsortedList = new List<WeightedRandomEntry<string>>();

        var random = new RomuDuoJr(500);

        for (var i = 0; i < EntriesToAdd; i++)
        {
            unsortedList.Add(new WeightedRandomEntry<string>($"{i}", random.NextByte()));
        }

        unsortedList.InsertionSort();

        var s = "";
    }
}

#pragma warning restore S2699
