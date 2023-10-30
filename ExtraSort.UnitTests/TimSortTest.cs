using System.Diagnostics.CodeAnalysis;
using ExtraRandom.PRNG;
using ExtraRandom.Specialised;
using FluentAssertions;

namespace ExtraSort.UnitTests;

[SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1600:Elements should be documented",
    Justification = "These are tests"
)]
#pragma warning disable S2699 // There is nothing to really test here, just need to see if it generates stuff.
public class TimSortTest
{
    private const int EntriesToAdd = 5_000;

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

        unsortedList
            .Should()
            .BeEquivalentTo(
                new List<int>
                {
                    0,
                    1,
                    9,
                    11,
                    12,
                    13,
                    15,
                    17,
                    19,
                    19,
                    22,
                    22,
                    22,
                    23,
                    23,
                    24,
                    25,
                    27,
                    28
                }
            );
    }
}
#pragma warning restore S2699
