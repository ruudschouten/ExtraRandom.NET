namespace ExtraShuffle.TestHelper;

public static class Playlist
{
    public static List<Song> GetTestPlaylist()
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
            new Song(
                "I Can Feel a Hot One",
                "Mean Everything to Nothing",
                "Alternative",
                artists[2]
            ),
            new Song(
                "It All Makes Sense at the End",
                "Mean Everything to Nothing",
                "Alternative",
                artists[2]
            ),
            new Song("m'lover", "Sonderlust", "Alternative", artists[3])
        ];
    }
}
