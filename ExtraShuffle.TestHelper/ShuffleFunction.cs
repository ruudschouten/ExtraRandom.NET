namespace ExtraShuffle.TestHelper;

public record ShuffleFunction(string Name, Func<IList<Song>, IList<Song>> Method)
{
    public override string ToString()
    {
        return Name;
    }
}
