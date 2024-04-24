using ExtraMath;
using ExtraRandom;
using ExtraSort;

namespace ExtraShuffle;

/// <summary>
/// <para>
/// Fibonacci Hashing Shuffle, as explained by Martin Fiedler.
/// It is an alternative to the Balanced Shuffle.
/// </para>
/// <para>
/// source: https://pncnmnp.github.io/blogs/fibonacci-hashing.html
/// </para>
/// </summary>
public static class FibonacciHashingShuffleExtension
{
    /// <summary>
    /// <para>
    /// Shuffle the <paramref name="list"/> by first grouping elements using <paramref name="groupingFunction"/>,
    /// then applying a fibonacci hashing to shuffle the list.
    /// </para>
    /// <para>This is an <c>O(n + m)</c> operation, where <c>m</c> is the amount of groups created with the <paramref name="groupingFunction"/>.</para>
    /// </summary>
    /// <param name="list">List to shuffle</param>
    /// <param name="random">Random number generator</param>
    /// <param name="groupingFunction">Grouping function</param>
    public static void FibonacciHashingShuffle<TSource, TKey>(
        this IList<TSource> list,
        IRandom random,
        Func<TSource, TKey> groupingFunction
    )
        where TKey : notnull
    {
        foreach (
            var (oldIndex, entry) in list.FibonacciHashingShuffleIterator(random, groupingFunction)
        )
        {
            list[oldIndex] = entry;
        }
    }

    /// <summary>
    /// <para>
    /// Iterate over <paramref name="list"/> by first grouping elements using <paramref name="groupingFunction"/>,
    /// then applying a fibonacci hashing to shuffle the list.
    /// </para>
    /// <para>This is an <c>O(n + m)</c> operation, where <c>m</c> is the amount of groups created with the <paramref name="groupingFunction"/>.</para>
    /// </summary>
    /// <remarks>
    /// <para>This does <b>NOT</b> alter <paramref name="list"/>, you need to do this yourself.</para>
    /// <para>If you do not want to do this, consider using <see cref="FibonacciHashingShuffle{TSource,TKey}"/></para>
    /// </remarks>
    /// <param name="list">List to shuffle</param>
    /// <param name="random">Random number generator</param>
    /// <param name="groupingFunction">Grouping function</param>
    public static IEnumerable<(int index, TSource replacement)> FibonacciHashingShuffleIterator<
        TSource,
        TKey
    >(this IList<TSource> list, IRandom random, Func<TSource, TKey> groupingFunction)
        where TKey : notnull
    {
        var groupedItems = new Dictionary<TKey, Queue<TSource>>();
        var groups = new List<TKey>();

        // Shuffle the items all at once, to save on enumerations and shuffles later on.
        list.MillerShuffle(random.NextInt(1, short.MaxValue));
        foreach (var item in list)
        {
            var group = groupingFunction(item);
            if (!groupedItems.TryGetValue(group, out var value))
            {
                value = new Queue<TSource>();
                groupedItems[group] = value;
                groups.Add(group);
            }

            value.Enqueue(item);
        }

        var k = 1;
        var n = groups.Count;

        while (n > 0)
        {
            var i = (int)Math.Floor((k * NumericConstants.GoldenRatioConjugate) % 1 * n);
            var group = groups[i];
            var entry = groupedItems[group].Dequeue();

            list[k - 1] = entry;

            yield return (k - 1, entry);
            k++;

            if (groupedItems[group].Count != 0)
                continue;

            n--;
            groups.RemoveBySwap(i);
        }
    }
}
