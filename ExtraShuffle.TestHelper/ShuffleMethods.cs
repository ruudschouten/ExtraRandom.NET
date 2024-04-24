using ExtraRandom.PRNG;
using Xunit;

namespace ExtraShuffle.TestHelper;

public record Shuffle(string Name, Func<IList<Song>, IList<Song>> Method)
{
    public override string ToString()
    {
        return Name;
    }
}

public class ShuffleMethods : TheoryData<Shuffle>
{
    public ShuffleMethods()
    {
        Add(new Shuffle("Balanced Shuffle", list =>
        {
            list.BalancedShuffle(new RomuTrio(500), song => song.Genre);
            return list;
        }));
        Add(new Shuffle("Faro Shuffle", list =>
        {
            list.FaroShuffle();
            return list;
        }));
        Add(new Shuffle("Fibonacci Hashing Shuffle", list =>
        {
            list.FibonacciHashingShuffle(new RomuTrio(500), song => song.Genre);
            return list;
        }));
        Add(new Shuffle("Fischer Yates Shuffle", list =>
        {
            list.FischerYatesShuffle(new RomuTrio(500));
            return list;
        }));
    }
}