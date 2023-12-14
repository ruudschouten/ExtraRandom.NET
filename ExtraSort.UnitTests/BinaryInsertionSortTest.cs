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
public class BinaryInsertionSortTest
{
    private const int EntriesToAdd = 50_000;

    [Fact]
    public void BinaryInsertionSort()
    {
        var unsortedList = new List<WeightedRandomEntry<string>>();

        var random = new RomuDuoJr(500);

        for (var i = 0; i < EntriesToAdd; i++)
        {
            unsortedList.Add(new WeightedRandomEntry<string>($"{i}", random.NextByte()));
        }

        unsortedList.BinaryInsertionSort();
    }

    [Fact]
    public void BinaryInsertionSort_Worked()
    {
        var unsortedList = TestValues.UnsortedNumbers;

        unsortedList.BinaryInsertionSort();

        unsortedList.Should().ContainInOrder(TestValues.SortedNumbers);
    }
}

#pragma warning restore S2699
