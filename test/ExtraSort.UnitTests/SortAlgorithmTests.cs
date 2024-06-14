using ExtraRandom.PRNG;
using ExtraSort.TestHelper;
using FluentAssertions;
using Xunit.Abstractions;

namespace ExtraSort.UnitTests;

public class SortAlgorithmTests(ITestOutputHelper output)
{
    [Theory]
    [ClassData(typeof(SortAlgorithms))]
    public void Sort_Worked(ISortAlgorithm algorithm)
    {
        var unsortedList = TestValues.UnsortedNumbers.ToArray();

        output.WriteLine("Unsorted");
        output.WriteLine(string.Join(',', unsortedList));

        unsortedList.Sort(algorithm);

        output.WriteLine("Sorted");
        output.WriteLine(string.Join(',', unsortedList));

        unsortedList.Should().ContainInOrder(TestValues.SortedNumbers);
    }

    [Theory]
    [ClassData(typeof(SortAlgorithms))]
    public void Sort_Worked_LargeSet(ISortAlgorithm algorithm)
    {
        var unsortedList = TestValues.LargeUnsortedNumbers.ToArray();
        output.WriteLine("First 50 unsorted elements");
        output.WriteLine(string.Join(',', unsortedList.Take(50)));

        unsortedList.Sort(algorithm);

        output.WriteLine("First 50 sorted elements");
        output.WriteLine(string.Join(',', unsortedList.Take(50)));

        unsortedList.Should().ContainInOrder(TestValues.LargeSortedNumbers);
    }

    [Theory]
    [ClassData(typeof(SortAlgorithms))]
    public void Sort_RandomGeneratedList(ISortAlgorithm algorithm)
    {
        const int size = 50_000;
        var random = new RomuDuoJr(813123218871ul);
        var list = new List<int>(size);
        for (var i = 0; i < size; i++)
            list.Add(random.NextInt());

        output.WriteLine("First 50 unsorted elements");
        output.WriteLine(string.Join(',', list.Take(50)));

        list.Sort(algorithm);

        output.WriteLine("First 50 sorted elements");
        output.WriteLine(string.Join(',', list.Take(50)));
        list.Should().BeInAscendingOrder();
    }
}
