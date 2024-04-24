using ExtraRandom.PRIG;

namespace ExtraShuffle;

public static class MillerShuffleExtension
{
    public static void MillerShuffle<T>(
        this IList<T> list,
        int seed = 500,
        MillerShuffleVariant variant = MillerShuffleVariant.MS_XLite
    )
    {
        var count = list.Count - 1;
        for (var i = 1; i <= count; i++)
        {
            var replacer = (int)MillerShuffleAlgorithm.MillerShuffle(i, seed, count, variant);
            (list[i], list[replacer]) = (list[replacer], list[i]);
        }
    }

    public static IEnumerable<(int index, int replacer)> MillerShuffleIterator<T>(
        this IList<T> list,
        int seed = 500,
        MillerShuffleVariant variant = MillerShuffleVariant.MS_XLite
    )
    {
        var count = list.Count - 1;
        for (var i = 1; i <= count; i++)
        {
            var replacer = (int)MillerShuffleAlgorithm.MillerShuffle(i, seed, count, variant);
            yield return (i, replacer);
        }
    }
}
