using ExtraRandom.PRNG;
using ExtraShuffle.TestHelper;
using FluentAssertions;

namespace ExtraShuffle.UnitTests;

using static Playlist;

public class FibonacciHashingShuffleTest
{
    [Fact]
    public void BalancedShuffle_ShouldShuffle_WhenCalled()
    {
        var songs = GetTestPlaylist();
        songs.FibonacciHashingShuffle(new RomuTrio(500), song => new { song.Artist.Name });
        songs.Should().NotContainInOrder(GetTestPlaylist());
    }

    [Fact]
    public void AllShuffles()
    {
        var balanced = GetTestPlaylist();
        var faro = GetTestPlaylist();
        var fibonacci = GetTestPlaylist();
        var fischerYates = GetTestPlaylist();
        var random = new RomuTrio(500);

        balanced.BalancedShuffle(random, song => new { song.Genre });
        faro.FaroShuffle();
        fibonacci.FibonacciHashingShuffle(random, song => new { song.Genre });
        fischerYates.FischerYatesShuffle(random);

        var s = "";
    }
}
