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
public class TimSortTest
{
    private const int EntriesToAdd = 10;

    [Fact]
    public void TimSort()
    {
        var unsortedList = new List<WeightedRandomEntry<string>>();

        var random = new RomuDuoJr(500);

        for (var i = 0; i < EntriesToAdd; i++)
        {
            unsortedList.Add(new WeightedRandomEntry<string>($"{i}", random.NextByte()));
        }

        unsortedList.TimSort();

        var s = "";
    }

    [Fact]
    public void TimSort_Worked()
    {
        var unsortedList = new List<int>
        {
            19,
            22,
            19,
            22,
            24,
            25,
            17,
            11,
            22,
            23,
            28,
            23,
            0,
            1,
            12,
            9,
            13,
            27,
            15
        };

        unsortedList.TimSort();

        var s = "";
    }
}
#pragma warning restore S2699
