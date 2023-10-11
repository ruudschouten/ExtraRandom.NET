namespace ExtraRandom.Specialised;

/// <summary>
/// A random generator which uses weights to determine the likeliness of an entry being picked.
/// </summary>
/// <typeparam name="T">determines the type of the value, of the entries.</typeparam>
public struct WeightedRandom<T>
    where T : notnull
{
    private readonly IRandom _random;
    private ulong _collectiveWeight = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="WeightedRandom{T}"/> struct.
    /// </summary>
    /// <param name="random"><see cref="IRandom"/> instance to use for RNG.</param>
    public WeightedRandom(IRandom random)
    {
        _random = random;
    }

    /// <summary>
    /// Gets the entries which can be rolled with this Weighted Random generator.
    /// </summary>
    private LinkedList<WeightedRandomEntry<T>> Entries { get; } = new();

    /// <summary>
    /// Add a new entry to the list with the provided <paramref name="value"/> and <paramref name="weight"/>.
    /// </summary>
    /// <param name="value">Value of the entry to add.</param>
    /// <param name="weight">Weight of the entry to add.</param>
    public void Add(T value, ulong weight)
    {
        // Update collectiveWeight here, so the value is kept up to date and doesn't require a loop through the Entries.
        _collectiveWeight += weight;
        Add(new WeightedRandomEntry<T>(value, weight));
    }

    /// <summary>
    /// Roll the next value.
    /// </summary>
    /// <returns>The random value based on weight.</returns>
    public readonly T Next()
    {
        return NextWithWeight(out _).Value;
    }

    /// <summary>
    /// Roll the next value, while also returning the <see cref="WeightedRandomEntry{T}"/> and the random value that was rolled.
    /// </summary>
    /// <param name="roll">Randomly rolled value that was used to determine what <see cref="WeightedRandomEntry{T}"/> to pick.</param>
    /// <returns>The <see cref="WeightedRandomEntry{T}"/> that was picked.</returns>
    public readonly WeightedRandomEntry<T> NextWithWeight(out ulong roll)
    {
        roll = _random.NextULong(0, _collectiveWeight);

        var handledRoll = roll;
        foreach (var entry in Entries)
        {
            if (entry.Weight >= handledRoll)
                return entry;
            handledRoll -= entry.Weight;
        }

        return Entries.Last?.Value ?? default;
    }

    /// <summary>
    /// Add a new <see cref="WeightedRandomEntry{T}"/> to the list, automatically sorting it by
    /// <see cref="WeightedRandomEntry{T}.Weight">weight.</see>
    /// </summary>
    /// <param name="entry">Entry to add.</param>
    /// <remarks>Performance for this is not checked, if performance issues arise, look here first.</remarks>
    private readonly void Add(WeightedRandomEntry<T> entry)
    {
        if (Entries.Count == 0)
        {
            Entries.AddFirst(entry);
            return;
        }

        // New entry should be added *before* the current node when the weight is larger than the new entry.
        for (var node = Entries.First; node != null; node = node.Next)
        {
            if (node.Value <= entry)
                continue;

            Entries.AddBefore(node, new LinkedListNode<WeightedRandomEntry<T>>(entry));
            return;
        }

        // If the loop didn't return prematurely, the entry has the smallest weight so far.
        // Add it to the end.
        Entries.AddLast(entry);
    }
}
