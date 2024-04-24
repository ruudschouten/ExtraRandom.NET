using ExtraSort;

namespace ExtraShuffle;

public static class FaroShuffleExtension
{
    /// <summary>
    /// <para>Performs a Faro Shuffle on the <paramref name="list"/>.</para>
    /// <para>
    /// The Faro Shuffle is a perfect shuffle, meaning the deck will be perfectly interleaved by the shuffle.
    /// This is achieved by splitting the deck into two equal halves and then interleaving them together.
    /// </para>
    /// <para>This is a <c>O(n)</c> operation.</para>
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

    /// <summary>
    /// <para>
    /// Iterate over a range in <paramref name="list"/> using the provided <paramref name="min"/> and <paramref name="max"/>.
    /// </para>
    /// <para>This is a <c>O(n)</c> operation.</para>
    /// </summary>
    /// <remarks>
    /// <para>This does <b>NOT</b> alter <paramref name="list"/>, you need to do this yourself.</para>
    /// <para>If you do not want to do this, consider using <see cref="FaroShuffle{T}"/></para>
    /// </remarks>
    /// <param name="list">The list to shuffle.</param>
    /// <param name="min">The minimum index in the list to shuffle from. Default is 0.</param>
    /// <param name="max">The maximum index in the list to shuffle to. Default is the last index of the list.</param>
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
