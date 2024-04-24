using ExtraRandom.PRNG;
using ExtraShuffle.TestHelper;
using FluentAssertions;
using static ExtraShuffle.TestHelper.Playlist;

namespace ExtraShuffle.UnitTests;

public class BalancedShuffleTest
{
    [Fact]
    public void BalancedShuffle_ShouldShuffle_WhenCalled()
    {
        var songs = GetTestPlaylist();
        songs.BalancedShuffle(new RomuTrio(500), song => new { song.Artist.Name });
        songs.Should().NotContainInOrder(GetTestPlaylist());
    }

    [Fact]
    public void BalancedShuffle_ShouldReturnSameList_WhenCalledWithOneSong()
    {
        var songs = new List<Song> { GetTestPlaylist()[0] };

        songs.BalancedShuffle(new RomuTrio(500), song => new { song.Artist.Name });

        songs.Should().HaveCount(1);
        songs.Single().Title.Should().Be("Rolling in the Deep");
    }
}
