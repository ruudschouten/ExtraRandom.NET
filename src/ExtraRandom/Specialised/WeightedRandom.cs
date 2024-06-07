using ExtraSort;

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
    private bool _needsSorting = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="WeightedRandom{T}"/> struct.
    /// </summary>
    /// <param name="random"><see cref="IRandom"/> instance to use for RNG.</param>
    public WeightedRandom(IRandom random)
    {
        _random = random;
    }

    /// <summary>
    /// Gets or sets the entries which can be rolled with this Weighted Random generator.
    /// </summary>
    // ENHANCEMENT: Make this a tree instead, and insert new entries at the correct spot, so sorting is not needed.
    private List<WeightedRandomEntry<T>> Entries { get; } = new();

    /// <summary>
    /// <para>The sorting algorithm to use when sorting the entries.</para>
    /// <para>Default is <see cref="MergeSort"/></para>
    /// </summary>
    public ISortAlgorithm SortAlgorithm { get; set; } = new MergeSort();

    /// <summary>
    /// Add a new entry to the list with the provided <paramref name="value"/> and <paramref name="weight"/>.
    /// </summary>
    /// <param name="value">Value of the entry to add.</param>
    /// <param name="weight">Weight of the entry to add.</param>
    public void Add(T value, ulong weight)
    {
        Add(new WeightedRandomEntry<T>(value, weight));
    }

    /// <summary>
    /// Roll the next value.
    /// </summary>
    /// <returns>The random value based on weight.</returns>
    public T Next()
    {
        return NextWithWeight(out _).Value;
    }

    /// <summary>
    /// Perform a sort function on the entries.
    /// </summary>
    /// <param name="sortAlgorithm">algorithm to sort with.</param>
    /// <returns>The list sorted.</returns>
    public IList<WeightedRandomEntry<T>> ManualSort(
        ISortAlgorithm sortAlgorithm
    )
    {
        Entries.Sort(sortAlgorithm);
        _needsSorting = false;
        return Entries;
    }

    /// <summary>
    /// Roll the next value, while also returning the <see cref="WeightedRandomEntry{T}"/> and the random value that was rolled.
    /// </summary>
    /// <param name="roll">Randomly rolled value that was used to determine what <see cref="WeightedRandomEntry{T}"/> to pick.</param>
    /// <returns>The <see cref="WeightedRandomEntry{T}"/> that was picked.</returns>
    public WeightedRandomEntry<T> NextWithWeight(out ulong roll)
    {
        if (_needsSorting)
        {
            Entries.Sort(SortAlgorithm);
            _needsSorting = false;
        }

        roll = _random.NextULong(0, _collectiveWeight);

        var handledRoll = roll;
        foreach (var entry in Entries)
        {
            if (entry.Weight >= handledRoll)
                return entry;
            handledRoll -= entry.Weight;
        }

        return Entries[Entries.Count];
    }

    /// <summary>
    /// Add a new <see cref="WeightedRandomEntry{T}"/> to the list, automatically sorting it by <see cref="WeightedRandomEntry{T}.Weight">weight.</see>
    /// </summary>
    /// <param name="entry">Entry to add.</param>
    /// <remarks><para>Performance for this is not checked, if performance issues arise, look here first.</para></remarks>
    private void Add(WeightedRandomEntry<T> entry)
    {
        // Update collectiveWeight here, so the value is kept up to date and doesn't require a loop through the Entries.
        _collectiveWeight += entry.Weight;
        _needsSorting = true;
        Entries.Add(entry);
    }
}