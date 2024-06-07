using System.Diagnostics.CodeAnalysis;
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
    [Fact]
    public void TimSort_Worked()
    {
        var unsortedList = TestValues.UnsortedNumbers;

        unsortedList.Sort(new TimSort());

        unsortedList.Should().ContainInOrder(TestValues.SortedNumbers);
    }
}
#pragma warning restore S2699
