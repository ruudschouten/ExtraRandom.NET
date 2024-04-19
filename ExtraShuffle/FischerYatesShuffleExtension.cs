using System.Runtime.CompilerServices;
using ExtraRandom;

namespace ExtraShuffle;

public static class FischerYatesShuffleExtension
{
    /// <summary>
    /// Randomly shuffle the <paramref name="list"/>.
    /// </summary>
    public static void FischerYatesShuffle<T>(this IList<T> list, IRandom random, int min = 0, int max = int.MinValue)
    {
        if (max == int.MinValue) max = list.Count;

        foreach (var (i, j) in list.FischerYatesShuffleIterable(random, min, max))
        {
            (list[i], list[j]) = (list[j], list[i]);
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
    public static IEnumerable<(int, int)> FischerYatesShuffleIterable<T>(
        this IList<T> list,
        IRandom random,
        int min = 0,
        int max = int.MinValue)
    {
        if (max == int.MinValue) max = list.Count;

        for (var i = max - 1; i >= min; i--)
        {
            var j = random.NextInt(i, max);
            yield return new ValueTuple<int, int>(i, j);
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
    public static async IAsyncEnumerable<(int, int)> FischerYatesShuffleAsync<T>(
        this IList<T> list,
        IRandom random,
        int min = 0,
        int max = int.MinValue,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (max == int.MinValue) max = list.Count;

        for (var i = max - 1; i >= min; i--)
        {
            var j = await Task.Run(() => random.NextInt(i, max), cancellationToken);
            yield return new ValueTuple<int, int>(i, j);
        }
    }
}