using ExtraRandom.PRIG;

namespace ExtraShuffle;

public static class MillerShuffleExtension
{
    public static void MillerShuffle<T>(
        this IList<T> list, int seed, MillerShuffleVariant variant
    )
    {
        foreach (var (index, replacer) in list.MillerShuffleIterator(seed, variant))
        {
            (list[index], list[replacer]) = (list[replacer], list[index]);
        }
    }

    public static IEnumerable<(int index, int replacer)> MillerShuffleIterator<T>(this IList<T> list, int seed,
        MillerShuffleVariant variant)
    {
        var count = list.Count - 1;
        for (var i = 0; i <= count; i++)
        {
            var replacer = (int)MillerShuffleAlgorithm.MillerShuffle(i, seed, count, variant);
            yield return (i, replacer);
        }
    }
}