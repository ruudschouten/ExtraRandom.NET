using ExtraRandom;
using ExtraRandom.PRNG;
using FluentAssertions;

namespace ExtraShuffle.UnitTests;

public class FischerYatesShuffleTest
{
    private readonly List<int> _list = [1, 2, 3, 4, 5];
    private readonly IRandom _random = new RomuTrio(500);

    [Fact]
    public void TestSort()
    {
        _list.FischerYatesShuffle(_random);
        _list.Should().NotContainInOrder(1, 2, 3, 4, 5);
    }

    [Fact]
    public void TestIterableSort()
    {
        foreach (var (i, j) in _list.FischerYatesShuffleIterable(_random))
        {
            (_list[i], _list[j]) = (_list[j], _list[i]);
        }

        _list.Should().NotContainInOrder(1, 2, 3, 4, 5);
    }

    [Fact]
    public async Task TestSortAsync()
    {
        await foreach (var (i, j) in _list.FischerYatesShuffleAsync(_random))
        {
            (_list[i], _list[j]) = (_list[j], _list[i]);
        }

        _list.Should().NotContainInOrder(1, 2, 3, 4, 5);
    }
}
