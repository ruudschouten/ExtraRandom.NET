namespace ExtraShuffle.TestHelper;

public record Song(string Title, string Album, string Genre, Artist Artist)
{
    public Song()
        : this(string.Empty, string.Empty, string.Empty, new Artist(string.Empty)) { }
}
