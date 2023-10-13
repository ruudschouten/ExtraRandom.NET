using System.Diagnostics.CodeAnalysis;
using ExtraRandom.PRNG;
using ExtraRandom.Specialised;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace ExtraSort.UnitTests;

[SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1600:Elements should be documented",
    Justification = "These are tests"
)]
public class MergeSortTest
{
    private const int EntriesToAdd = 50_000;

    [Fact]
#pragma warning disable S2699 // There is nothing to really test here, just need to see if it generates stuff.
    public void Test_Merge_List()
#pragma warning restore S2699
    {
        var unsortedList = new List<WeightedRandomEntry<string>>();

        var random = new RomuDuoJr(500);

        for (var i = 0; i < EntriesToAdd; i++)
        {
            unsortedList.Add(new WeightedRandomEntry<string>($"{i}", random.NextByte()));
        }

        var sorted = unsortedList.MergeSort();

        var s = "";
    }
}
