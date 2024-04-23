using ExtraRandom.PRNG;
using FluentAssertions;

namespace ExtraShuffle.UnitTests;

public record Artist(string Name);

public record Song(string Title, string Album, string Genre, Artist Artist);

public class BalancedShuffleTest
{
    [Fact]
    public void BalancedShuffle_ShouldShuffle_WhenCalled()
    {
        var songs = GetTestSongs();
        songs.BalancedShuffle(new RomuTrio(500), song => new { song.Artist.Name });
        songs.Should().NotContainInOrder(GetTestSongs());
    }

    [Fact]
    public void BalancedShuffle_ShouldReturnSameList_WhenCalledWithOneSong()
    {
        var songs = new List<Song> { GetTestSongs()[0] };

        songs.BalancedShuffle(new RomuTrio(500), song => new { song.Artist.Name });

        songs.Should().HaveCount(1);
        songs.Single().Title.Should().Be("Rolling in the Deep");
    }

    private static List<Song> GetTestSongs()
    {
        var artists = new List<Artist>
        {
            new("Adele"),
            new("Metallica"),
            new("Manchester Orchestra"),
            new("Kishi Bashi")
        };

        return
        [
            new Song("Rolling in the Deep", "21", "Pop", artists[0]),
            new Song("Rumour Has It", "21", "Pop", artists[0]),
            new Song("Someone Like You", "21", "Pop", artists[0]),
            new Song("Right as Rain", "19", "Pop", artists[0]),
            new Song("Make You Feel My Love", "19", "Pop", artists[0]),
            new Song("Enter Sandman", "Metallica", "Metal", artists[1]),
            new Song("The Silence", "A Black Mile to the Surface", "Alternative", artists[2]),
            new Song("The Gold", "A Black Mile to the Surface", "Alternative", artists[2]),
            new Song("The Alien", "A Black Mile to the Surface", "Alternative", artists[2]),
            new Song("The Sunshine", "A Black Mile to the Surface", "Alternative", artists[2]),
            new Song("Simple Math", "Simple Math", "Alternative", artists[2]),
            new Song("I Can Feel a Hot One", "Mean Everything to Nothing", "Alternative", artists[2]),
            new Song("It All Makes Sense at the End", "Mean Everything to Nothing", "Alternative", artists[2]),
            new Song("m'lover", "Sonderlust", "Alternative", artists[3])
        ];
    }
}