using ExtraRandom;

namespace ExtraShuffle;

/// <summary>
/// <para>
/// Balanced Shuffle, as explained by Martin Fiedler.
/// The basic principle of the algorithm is to split the  collection into multiple logical groups.
/// </para>
/// <para>
/// source: https://keyj.emphy.de/balanced-shuffle/
/// </para>
/// </summary>
public static class BalancedShuffleExtension
{
    /// <summary>
    /// Shuffle the <paramref name="list"/> by first grouping elements using <paramref name="groupingFunction"/>,
    /// then making all groups the same size by adding dummy elements, and finally merging and shuffling the groups.
    /// </summary>
    /// <param name="list">List to shuffle</param>
    /// <param name="random">Random number generator</param>
    /// <param name="groupingFunction">Grouping function</param>
    public static void BalancedShuffle<TSource, TKey>(
        this IList<TSource> list,
        IRandom random,
        Func<TSource, TKey> groupingFunction
    )
        where TKey : notnull
    {
        foreach (var (index, replacement) in list.BalancedShuffleIterable(random, groupingFunction))
        {
            list[index] = replacement;
        }
    }

    /// <summary>
    /// Iterate over <paramref name="list"/> by first grouping elements using <paramref name="groupingFunction"/>,
    /// then making all groups the same size by adding dummy elements, and finally merging and shuffling the groups.
    /// </summary>
    /// <remarks>
    /// <para>This does <b>NOT</b> alter <paramref name="list"/>, you need to do this yourself.</para>
    /// <para>
    /// If you do not want to do this, consider using <see cref="BalancedShuffle{TSource,TKey}"/>
    /// </para></remarks>
    /// <param name="list">List to shuffle</param>
    /// <param name="random">Random number generator</param>
    /// <param name="groupingFunction">Grouping function</param>
    public static IEnumerable<(int index, TSource replacement)> BalancedShuffleIterable<
        TSource,
        TKey
    >(this IList<TSource> list, IRandom random, Func<TSource, TKey> groupingFunction)
        where TKey : notnull
    {
        var groups = GroupItems(list, groupingFunction, out var maxEntryCount);
        var filledGroups = Fill(groups, maxEntryCount, random);
        var index = 0;
        foreach (var column in MergeIntoColumns(filledGroups, maxEntryCount, random))
        {
            foreach (var entry in column)
            {
                yield return (index++, entry);
            }
        }
    }

    /// <summary>
    /// Go through the list and group the items by the <paramref name="groupingFunction"/>, and count the maximum group size.
    /// </summary>
    private static Dictionary<TKey, List<TSource>> GroupItems<TSource, TKey>(
        IList<TSource> list,
        Func<TSource, TKey> groupingFunction,
        out int maxEntryCount
    )
        where TKey : notnull
    {
        maxEntryCount = 0;
        var groups = new Dictionary<TKey, List<TSource>>();
        foreach (var item in list)
        {
            var key = groupingFunction(item);
            if (!groups.TryGetValue(key, out var group))
            {
                group = new List<TSource>();
                groups.Add(key, group);
            }

            group.Add(item);
            if (group.Count > maxEntryCount)
            {
                maxEntryCount = group.Count;
            }
        }

        return groups;
    }

    /// <summary>
    /// FIll the <paramref name="groups"/> with dummies in random positions to make all groups the same size and randomise the order.
    /// </summary>
    /// <param name="groups">Groups to fill</param>
    /// <param name="maxEntryCount">Maximum group size</param>
    /// <param name="random">Random number generator</param>
    private static List<List<TSource?>> Fill<TSource, TKey>(
        Dictionary<TKey, List<TSource>> groups,
        int maxEntryCount,
        IRandom random
    )
        where TKey : notnull
    {
        List<List<TSource?>> filledGroups = new(groups.Count);
        var n = maxEntryCount * groups.Count; // total length of the output list
        var k = groups.Count; // number of segments

        foreach (var (_, group) in groups)
        {
            List<TSource?> filledGroup = new(maxEntryCount);
            filledGroup.AddRange(group);

            while (filledGroup.Count < maxEntryCount)
            {
                // Compute the optimal length of the segment
                var r = (double)n / k;
                // Add noise
                r += r * random.NextDouble(-0.1, 0.1); // +/- 10% deviation

                // Convert r to an integer and clip it against bounds
                var rInt = (int)Math.Round(r);
                rInt = Math.Clamp(rInt, 1, n - k + 1);

                for (var i = 0; i < rInt && filledGroup.Count < maxEntryCount; i++)
                {
                    filledGroup.Add(default!);
                }

                // Update n and k for the next iteration
                n -= rInt;
                k--;
            }

            // Transpose the group by a random offset
            var offset = random.NextInt(0, maxEntryCount);
            filledGroup = filledGroup.Skip(offset).Concat(filledGroup.Take(offset)).ToList();

            filledGroups.Add(filledGroup);
        }

        return filledGroups;
    }

    /// <summary>
    /// Merge the sorted groups into a single shuffled list.
    /// </summary>
    /// <param name="filledGroups">Groups to merge</param>
    /// <param name="maxEntryCount">Maximum group size</param>
    /// <param name="random">Random number generator</param>
    private static IEnumerable<List<TSource>> MergeIntoColumns<TSource>(
        IReadOnlyCollection<List<TSource?>> filledGroups,
        int maxEntryCount,
        IRandom random
    )
    {
        for (var column = 0; column < maxEntryCount; column++)
        {
            var slice = new List<TSource>(filledGroups.Count);
            slice.AddRange(filledGroups.Select(row => row[column]).OfType<TSource>());

            slice.MillerShuffle(random.NextInt(1, short.MaxValue));
            yield return slice;
        }
    }
}
