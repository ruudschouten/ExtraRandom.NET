using System.Diagnostics.CodeAnalysis;
using ExtraUtil;
using FluentAssertions;

namespace ExtraSort.UnitTests;

[SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1600:Elements should be documented",
    Justification = "These are tests"
)]
public class CollectionExtensionTest
{
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
