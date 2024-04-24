using System.Runtime.CompilerServices;
using ExtraRandom;

namespace ExtraShuffle;

public static class FischerYatesShuffleExtension
{
    /// <summary>
    /// Randomly shuffle the <paramref name="list"/>.
    /// </summary>
    public static void FischerYatesShuffle<T>(
        this IList<T> list,
        IRandom random,
        int min = 0,
        int max = int.MinValue
    )
    {
        if (max == int.MinValue)
            max = list.Count;

        foreach (var (index, replacer) in list.FischerYatesShuffleIterable(random, min, max))
        {
            (list[index], list[replacer]) = (list[replacer], list[index]);
        }
    }

    /// <summary>
    /// Iterate over <paramref name="list"/>.
    /// Each iteration returns the indices of the two elements that should be swapped.
    /// </summary>
    /// <remarks> This does <b>NOT</b> alter <paramref name="list"/>, you need to do this yourself.
    /// <para>
    /// If you do not want to do this, consider using <see cref="FischerYatesShuffle{T}"/>
    /// </para> </remarks>
    public static IEnumerable<(int index, int replacement)> FischerYatesShuffleIterable<T>(
        this ICollection<T> list,
        IRandom random,
        int min = 0,
        int max = int.MinValue
    )
    {
        if (max == int.MinValue)
            max = list.Count;

        for (var i = max - 1; i >= min; i--)
        {
            var j = random.NextInt(i, max);
            yield return (i, j);
        }
    }

    /// <summary>
    /// Iterate over a range in <paramref name="list"/> using the provided <paramref name="min"/> and <paramref name="max"/>.
    /// Each iteration returns the indices of the two elements that should be swapped.
    /// </summary>
    /// <remarks>This does <b>NOT</b> alter <paramref name="list"/>, you need to do this yourself.
    /// <para>
    /// If you do not want to do this, consider using <see cref="FischerYatesShuffle{T}"/>
    /// </para></remarks>
    public static async IAsyncEnumerable<(int index, int replacement)> FischerYatesShuffleAsync<T>(
        this ICollection<T> list,
        IRandom random,
        int min = 0,
        int max = int.MinValue,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        if (max == int.MinValue)
            max = list.Count;

        for (var i = max - 1; i >= min; i--)
        {
            var j = await Task.Run(() => random.NextInt(i, max), cancellationToken);
            yield return (i, j);
        }
    }
}
