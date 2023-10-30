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
public class CollectionExtensionTest
{
    [Fact]
    public void Slices()
    {
        var unsortedList = new List<WeightedRandomEntry<int>>();

        var random = new RomuDuoJr(500);

        for (var i = 0; i < 10; i++)
        {
            unsortedList.Add(new WeightedRandomEntry<int>(i, random.NextByte()));
        }

        // SKIP: 5, 6, 7, 8, 9
        // TAKE: 0, 1, 2, 3, 4
        var startItems = unsortedList.Slice(0, 4);
        startItems
            .Should()
            .ContainInOrder(
                unsortedList[0],
                unsortedList[1],
                unsortedList[2],
                unsortedList[3],
                unsortedList[4]
            );

        // SKIP: 0, 5, 6, 7, 8, 9
        // TAKE: 1, 2, 3, 4
        var fourItems = unsortedList.Slice(1, 4);
        fourItems
            .Should()
            .HaveCount(4)
            .And.ContainInOrder(unsortedList[1], unsortedList[2], unsortedList[3], unsortedList[4]);

        // SKIP: 0, 1, 2, 3, 4, 8, 9
        // TAKE: 5, 6, 7
        var threeItems = unsortedList.Slice(5, 7);
        threeItems
            .Should()
            .HaveCount(3)
            .And.ContainInOrder(unsortedList[5], unsortedList[6], unsortedList[7]);

        // SKIP: 0, 1, 2, 3, 4, 5, 6
        // TAKE: 7, 8, 9
        var endItems = unsortedList.Slice(7, unsortedList.Count);
        endItems.Should().ContainInOrder(unsortedList[7], unsortedList[8], unsortedList[9]);
    }

    [Theory]
    [InlineData(new[] { 0, 1, 4, 19, 21 }, 19, 3)]
    [InlineData(new[] { 1, 2, 3, 3, 8, 4, 21, 39 }, 39, 8)]
    [InlineData(new[] { 13, 14, 15, 16 }, 3, 0)]
    [InlineData(new[] { 13, 14, 15, 16 }, 20, 4)]
    [InlineData(new[] { 1, 2, 6, 7, 10 }, 5, 2)]
    public void BinarySearch(IList<int> list, int item, int index)
    {
        var pos = list.BinarySearch(item);
        pos.Should().Be(index);
    }
}
