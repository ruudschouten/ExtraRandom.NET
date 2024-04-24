using ExtraRandom.PRNG;
using ExtraShuffle.TestHelper;
using FluentAssertions;

namespace ExtraShuffle.UnitTests;

using static Playlist;

public class Checker
{
    [Fact]
    public void AllShuffles()
    {
        var unsorted = GetTestPlaylist();
        var balanced = GetTestPlaylist();
        var faro = GetTestPlaylist();
        var fibonacci = GetTestPlaylist();
        var fischerYates = GetTestPlaylist();
        var random = new RomuTrio(500);
        var grouping = new Func<Song, string>(song => song.Genre);

        balanced.BalancedShuffle(random, grouping);
        faro.FaroShuffle();
        fibonacci.FibonacciHashingShuffle(random, grouping);
        fischerYates.FischerYatesShuffle(random);

        balanced.Should().NotContainInOrder(unsorted);
        faro.Should().NotContainInOrder(unsorted);
        fibonacci.Should().NotContainInOrder(unsorted);
        fischerYates.Should().NotContainInOrder(unsorted);
    }
}