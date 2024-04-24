using System.Collections.Immutable;
using ExtraMath;
using ExtraRandom;
using ExtraSort;

namespace ExtraShuffle;

/// <summary>
/// Fibonacci Hashing Shuffle, as explained by Martin Fiedler.
/// It is an alternative to the Balanced Shuffle.
/// <para>
/// source: https://pncnmnp.github.io/blogs/fibonacci-hashing.html
/// </para>
/// </summary>
public static class FibonacciHashingShuffleExtension
{
    public static void FibonacciHashingShuffle<TSource, TKey>(
        this IList<TSource> items,
        IRandom random,
        Func<TSource, TKey> groupingFunction
    )
        where TKey : notnull
    {
        foreach (
            var (oldIndex, entry) in items.FibonacciHashingShuffleIterator(random, groupingFunction)
        )
        {
            items[oldIndex] = entry;
        }
    }

    public static IEnumerable<(int index, TSource replacement)> FibonacciHashingShuffleIterator<
        TSource,
        TKey
    >(this IList<TSource> items, IRandom random, Func<TSource, TKey> groupingFunction)
        where TKey : notnull
    {
        var itemsByCategory = new Dictionary<TKey, Queue<TSource>>();

        // Shuffle the items all at once, to save on enumerations and shuffles later on.
        items.FischerYatesShuffle(random);
        foreach (var item in items)
        {
            var category = groupingFunction(item);
            if (!itemsByCategory.TryGetValue(category, out var value))
            {
                value = new Queue<TSource>();
                itemsByCategory[category] = value;
            }

            value.Enqueue(item);
        }

        var categories = itemsByCategory.Keys.ToList();

        var k = 1;
        var n = categories.Count;

        while (n > 0)
        {
            var i = (int)Math.Floor((k * NumericConstants.GoldenRatioConjugate) % 1 * n);
            var category = categories[i];
            var entry = itemsByCategory[category].Dequeue();

            yield return (k - 1, entry);
            k++;

            if (itemsByCategory[category].Count != 0)
                continue;

            n--;
            categories.RemoveBySwap(i);
        }
    }
}
