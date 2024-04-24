using ExtraShuffle.TestHelper;
using FluentAssertions;

namespace ExtraShuffle.UnitTests;

using static Playlist;

public class ShuffleTester
{
    [Theory]
    [ClassData(typeof(ShuffleMethods))]
    public void Shuffling_ShouldNotBeTheSameOrderAsTheUnshuffledList_WhenCalled(Shuffle shuffle)
    {
        var unsorted = GetTestPlaylist();
        var playlist = GetTestPlaylist();
        shuffle.Method.Invoke(playlist);

        playlist.Should().NotContainInOrder(unsorted);
    }
}
