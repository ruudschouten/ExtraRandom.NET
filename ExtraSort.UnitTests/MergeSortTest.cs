using System.Diagnostics.CodeAnalysis;
using FluentAssertions;

namespace ExtraSort.UnitTests;

[SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1600:Elements should be documented",
    Justification = "These are tests"
)]
#pragma warning disable S2699 // There is nothing to really test here, just need to see if it generates stuff.
public class MergeSortTest
{
    [Fact]
    public void MergeSort_Worked()
    {
        var unsortedList = TestValues.UnsortedNumbers;

        var sorted = unsortedList.MergeSort();

        sorted.Should().ContainInOrder(TestValues.SortedNumbers);
    }
}
#pragma warning restore S2699
