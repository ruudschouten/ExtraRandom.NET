using FluentAssertions;

namespace ExtraShuffle.UnitTests;

public class FaroShuffleTest
{
    private readonly List<int> _list = [1, 2, 3, 4, 5, 6, 7, 8];

    [Fact]
    public void FaroShuffle_ShouldShuffle_WhenUsingAnEvenList()
    {
        _list.FaroShuffle();
        _list.Should().ContainInOrder(1, 5, 2, 6, 3, 7, 4, 8);
    }

    [Fact]
    public void FaroShuffle_ShouldShuffle_WhenUsingAnUnEvenList()
    {
        _list.Add(9);
        _list.FaroShuffle();
        _list.Should().ContainInOrder(1, 5, 2, 6, 3, 7, 4, 8, 9);
    }
}