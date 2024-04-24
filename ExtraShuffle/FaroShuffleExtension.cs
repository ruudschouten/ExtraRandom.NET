using ExtraSort;

namespace ExtraShuffle;

public static class FaroShuffleExtension
{
    /// <summary>
    /// Performs a Faro Shuffle on the <paramref name="list"/>.
    /// <para>
    /// The Faro Shuffle is a perfect shuffle, meaning the deck will be perfectly interleaved by the shuffle.
    /// This is achieved by splitting the deck into two equal halves and then interleaving them together.
    /// </para>
    /// </summary>
    /// <param name="list">The list to shuffle.</param>
    /// <param name="min">The minimum index in the list to shuffle from. Default is 0.</param>
    /// <param name="max">The maximum index in the list to shuffle to. Default is the last index of the list.</param>
    public static void FaroShuffle<T>(this IList<T> list, int min = 0, int max = int.MinValue)
    {
        foreach (var (old, replacement) in list.FaroShuffleIterator(min, max))
        {
            list[old] = replacement;
        }
    }
    public static IEnumerable<(int old, T replacement)> FaroShuffleIterator<T>(
        this IList<T> list,
        int min = 0,
        int max = int.MinValue
    )
    {
        if (max == int.MinValue)
            max = list.Count;

        var mid = (min + max) / 2;
        var firstHalf = list.Slice(min, mid - 1).ToArray();
        var secondHalf = list.Slice(mid, max).ToArray();
        var totalSize = firstHalf.Length + secondHalf.Length;

        for (var i = 0; i < totalSize; i++)
        {
            var currentHalf = i % 2 == 0 ? firstHalf : secondHalf;
            if (i / 2 < currentHalf.Length)
            {
                yield return (i, currentHalf[i / 2]);
            }
        }
    }
}
