using ExtraShuffle.TestHelper;
using FluentAssertions;

namespace ExtraShuffle.UnitTests;

using static Playlist;

public class ShuffleTester
{
    [Theory]
    [ClassData(typeof(ShuffleFunctions))]
    public void Shuffling_ShouldNotBeTheSameOrderAsTheUnshuffledList_WhenCalled(
        ShuffleFunction shuffle
    )
    {
        var unsorted = GetTestPlaylist();
        var playlist = GetTestPlaylist();
        shuffle.Method.Invoke(playlist);

        playlist.Should().NotContainInOrder(unsorted);
        playlist.Distinct().Count().Should().Be(unsorted.Distinct().Count());
    }
}
