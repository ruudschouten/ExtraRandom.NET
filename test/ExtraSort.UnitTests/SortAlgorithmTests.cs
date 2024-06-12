using ExtraRandom.PRNG;
using ExtraSort.TestHelper;
using FluentAssertions;

namespace ExtraSort.UnitTests;

public class SortAlgorithmTests
{
    [Theory]
    [ClassData(typeof(SortAlgorithms))]
    public void Sort_Worked(ISortAlgorithm algorithm)
    {
        var unsortedList = TestValues.UnsortedNumbers;

        unsortedList.Sort(algorithm);

        unsortedList.Should().ContainInOrder(TestValues.SortedNumbers);
    }

    [Theory]
    [ClassData(typeof(SortAlgorithms))]
    public void Sort_Worked_LargeSet(ISortAlgorithm algorithm)
    {
        var unsortedList = TestValues.LargeUnsortedNumbers;

        unsortedList.Sort(algorithm);

        unsortedList.Should().ContainInOrder(TestValues.LargeSortedNumbers);
    }

    [Theory]
    [ClassData(typeof(SortAlgorithms))]
    public void Sort_RandomGeneratedList(ISortAlgorithm algorithm)
    {
        const int size = 50_000;
        var random = new RomuDuoJr(3213123218871ul);
        var list = new List<int>(size);
        for (var i = 0; i < size; i++)
            list.Add(random.NextInt());
        
        list.Sort(algorithm);
        list.Should().BeInAscendingOrder();
    }
}
