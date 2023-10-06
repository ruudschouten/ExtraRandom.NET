namespace ExtraRandom.Specialised;

public struct NewWeightedRandom<T>
    where T : notnull
{
    private readonly IRandom _random;

    /// <summary>
    /// Initializes a new instance of the <see cref="NewWeightedRandom{T}"/> struct.
    /// </summary>
    /// <param name="random"><see cref="IRandom"/> instance to use for RNG.</param>
    public NewWeightedRandom(IRandom random)
    {
        _random = random;
    }

    /// <summary>
    /// Gets the entries which can be rolled with this Weighted Random generator.
    /// </summary>
    public LinkedList<WeightedRandomEntry<T>> Entries { get; init; } = new(); // TODO: Check if a LinkedList is the best fit for this.

    /* TODO:
     Instead of using percentages, sort the Entries by weight
     and roll a number between 0 and the sum of all weights.
     Final number can then be used to retrieve the entry.
     */

    /// <summary>
    /// Add a new entry to the list with the provided <paramref name="value"/> and <paramref name="weight"/>.
    /// </summary>
    /// <param name="value">Value of the entry to add.</param>
    /// <param name="weight">Weight of the entry to add.</param>
    public void Add(T value, int weight)
    {
        Add(new WeightedRandomEntry<T>(value, weight));
    }

    /// <summary>
    /// Add a new <see cref="WeightedRandomEntry{T}"/> to the list, automatically sorting it by
    /// <see cref="WeightedRandomEntry{T}.Weight">weight.</see>
    /// </summary>
    /// <param name="entry">Entry to add.</param>
    public void Add(WeightedRandomEntry<T> entry)
    {
        if (Entries.Count == 0)
        {
            Entries.AddFirst(entry);
            return;
        }

        // TODO: Look into Quick Sort or a Binary Tree to speed this up
        // Also check if this is actually slow or not.

        // New entry should be added *before* the current node when the weight is larger than the new entry.
        for (var node = Entries.First; node != null; node = node.Next)
        {
            if (node.Value.Weight <= entry.Weight)
                continue;

            Entries.AddBefore(node, new LinkedListNode<WeightedRandomEntry<T>>(entry));
            return;
        }

        // If the loop didn't return prematurely, the entry has the smallest weight so far.
        // Add it to the end.
        Entries.AddLast(entry);
    }
}
