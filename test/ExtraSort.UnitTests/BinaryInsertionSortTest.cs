using System.Diagnostics.CodeAnalysis;
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
    [Fact]
    public void BinaryInsertionSort_Worked()
    {
        var unsortedList = TestValues.UnsortedNumbers;

        unsortedList.BinaryInsertionSort();

        unsortedList.Should().ContainInOrder(TestValues.SortedNumbers);
    }
}

#pragma warning restore S2699
