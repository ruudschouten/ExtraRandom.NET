using ExtraRandom.PRIG;
using ExtraRandom.PRNG;
using Xunit;

namespace ExtraShuffle.TestHelper;

public class ShuffleFunctions : TheoryData<ShuffleFunction>
{
#pragma warning disable MA0051
    public ShuffleFunctions()
#pragma warning restore MA0051
    {
        Add(
            new ShuffleFunction(
                "Balanced Shuffle",
                list =>
                {
                    list.BalancedShuffle(new RomuTrio(500), song => song.Genre);
                    return list;
                }
            )
        );
        Add(
            new ShuffleFunction(
                "Faro Shuffle",
                list =>
                {
                    list.FaroShuffle();
                    return list;
                }
            )
        );
        Add(
            new ShuffleFunction(
                "Fibonacci Hashing Shuffle",
                list =>
                {
                    list.FibonacciHashingShuffle(new RomuTrio(500), song => song.Genre);
                    return list;
                }
            )
        );
        Add(
            new ShuffleFunction(
                "Fischer Yates Shuffle",
                list =>
                {
                    list.FischerYatesShuffle(new RomuTrio(500));
                    return list;
                }
            )
        );
        Add(
            new ShuffleFunction(
                "Miller Shuffle B",
                list =>
                {
                    list.MillerShuffle(500, MillerShuffleVariant.MSA_b);
                    return list;
                }
            )
        );
        Add(
            new ShuffleFunction(
                "Miller Shuffle D",
                list =>
                {
                    list.MillerShuffle(500, MillerShuffleVariant.MSA_d);
                    return list;
                }
            )
        );
        Add(
            new ShuffleFunction(
                "Miller Shuffle E",
                list =>
                {
                    list.MillerShuffle(500, MillerShuffleVariant.MSA_e);
                    return list;
                }
            )
        );
        Add(
            new ShuffleFunction(
                "Miller Shuffle Lite",
                list =>
                {
                    list.MillerShuffle(500, MillerShuffleVariant.MS_Lite);
                    return list;
                }
            )
        );
        Add(
            new ShuffleFunction(
                "Miller Shuffle XLite",
                list =>
                {
                    list.MillerShuffle();
                    return list;
                }
            )
        );
    }
}
