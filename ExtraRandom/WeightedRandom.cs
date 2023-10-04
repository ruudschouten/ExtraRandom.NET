using System.Runtime.InteropServices;

namespace ExtraRandom;

using ExtraRandom.Util;

/// <summary>
/// A random generator which uses weights to determine the likeliness of an entry being picked.
/// </summary>
/// <typeparam name="T">determines the type of the value, of the entries.</typeparam>
public class WeightedRandom<T>
    where T : notnull
{
    private readonly XoroshiroRandom _random;

    private bool _hasCalculatedWeight;
    private bool _hasCalculatedPercentages;
    private bool _hasSortedEntries;

    private int _collectiveWeight;

    /// <summary>
    /// Initializes a new instance of the <see cref="WeightedRandom{T}"/> class with a random seed and no entries.
    /// </summary>
    /// <param name="seed">seed that should be used for random number generation.</param>
    public WeightedRandom(int? seed = null)
    {
        _random = seed == null ? new XoroshiroRandom() : new XoroshiroRandom((int)seed);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WeightedRandom{T}"/> class with preset entries.
    /// </summary>
    /// <param name="entries">entries that can be rolled for.</param>
    /// <param name="seed">seed that should be used for random number generation.</param>
    public WeightedRandom(IList<Entry<T>> entries, int? seed = null)
        : this(seed)
    {
        Entries = entries;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WeightedRandom{T}"/> class with preset entries.
    /// </summary>
    /// <param name="entries">entries in a key-value collection, that can be rolled for.</param>
    /// <param name="seed">seed that should be used for random number generation.</param>
    public WeightedRandom(IDictionary<T, int> entries, int? seed = null)
        : this(seed)
    {
        SetEntries(entries);
    }

    /// <summary>
    /// Gets or sets the entries for the weighted random.
    /// </summary>
    public IList<Entry<T>> Entries { get; set; } = new List<Entry<T>>();

    /// <summary>
    /// Gets <see cref="_collectiveWeight"/>.
    /// If it hasn't been calculated yet, or <see cref="_hasCalculatedWeight"/> is set to true,
    /// it will calculate the weight.
    /// </summary>
    public int CollectiveWeight
    {
        get
        {
            if (_collectiveWeight != 0 && !_hasCalculatedWeight)
            {
                return _collectiveWeight;
            }

            _collectiveWeight = CalculateCollectiveWeight();
            _hasCalculatedWeight = true;

            return _collectiveWeight;
        }
    }

    /// <summary>
    /// Calculate the collective weight of all the entries.
    /// </summary>
    /// <returns>The sum of all entries' weight.</returns>
    private int CalculateCollectiveWeight()
    {
        return Entries.Sum(entry => entry.Weight);
    }

    /// <summary>
    /// Calculate the percentages of each entry.
    /// </summary>
    private void CalculatePercentage()
    {
        if (_hasCalculatedPercentages)
        {
            return;
        }

        foreach (var entry in Entries)
        {
            entry.Percentage = (float)entry.Weight / CollectiveWeight * 100;
        }

        _hasCalculatedPercentages = true;
    }

    /// <summary>
    /// Sort the entries based on their percentages.
    /// </summary>
    private void Sort()
    {
        if (_hasSortedEntries)
        {
            return;
        }

        Entries = Entries.OrderByDescending(entry => entry.Percentage).ToList();
        _hasSortedEntries = true;
    }

    /// <summary>
    /// Add a new <see cref="Entry{TValue}"/> with the provided <paramref name="value"/> and <paramref name="weight"/>.
    /// </summary>
    /// <param name="value">value for the entry.</param>
    /// <param name="weight">weight for the entry.</param>
    public void Add(T value, int weight)
    {
        Add(new Entry<T>(weight, value));
    }

    /// <summary>
    /// Add the provided <paramref name="entry"/> into the <see cref="Entry{TValue}"/> collection.
    /// </summary>
    /// <param name="entry"><see cref="Entry{TValue}"/> to be added.</param>
    public void Add(Entry<T> entry)
    {
        _collectiveWeight += entry.Weight;
        Entries.Add(entry);

        _hasCalculatedWeight = false;
        _hasCalculatedPercentages = false;
        _hasSortedEntries = false;
    }

    /// <summary>
    /// Overwrite <see cref="Entries"/> with the provided <paramref name="dictionary"/>.
    /// </summary>
    /// <param name="dictionary">
    /// key-value collection which will be parsed into a list of <see cref="Entry{TValue}">Entries</see>.
    /// </param>
    public void SetEntries(IDictionary<T, int> dictionary)
    {
        Entries = new List<Entry<T>>();
        foreach (var pair in dictionary)
        {
            Entries.Add(new Entry<T>(pair.Value, pair.Key));
        }

        _hasCalculatedWeight = false;
        _hasCalculatedPercentages = false;
        _hasSortedEntries = false;
    }

    /// <summary>
    /// Get the next randomly picked <see cref="Entry{TValue}"/>.
    /// </summary>
    /// <returns>a random <see cref="Entry{TValue}"/>.</returns>
    public Entry<T>? Next()
    {
        // Calculate percentages
        CalculatePercentage();

        // Sort the entries based on their percentages.
        Sort();

        var roll = _random.NextFloat(100f);

        var previousPercentage = 0f;

        // Loop over collection and check if the roll was between the previous roll and this roll.
        foreach (var entry in Entries)
        {
            var currentPercentage = entry.Percentage + previousPercentage;
            if (roll.IsBetween(previousPercentage, currentPercentage))
            {
                return entry;
            }

            previousPercentage += entry.Percentage;
        }

        return null;
    }

    /// <summary>
    /// An entry for <see cref="WeightedRandom{T}"/> generation.
    /// </summary>
    /// <typeparam name="TValue">type for <see cref="Value"/>.</typeparam>
    // TODO: Check if this can made into a Struct.
    public class Entry<TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entry{T}"/> class.
        /// </summary>
        /// <param name="weight">likeliness that this entry should be chosen.</param>
        /// <param name="value">value for the entry.</param>
        public Entry(int weight, TValue value)
        {
            Weight = weight;
            Value = value;
        }

        /// <summary>
        /// Gets or sets the likeliness that this entry gets chosen.
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Gets or sets the value for this entry.
        /// </summary>
        public TValue Value { get; set; }

        /// <summary>
        /// Gets or sets the percentage based on the <see cref="Weight"/> and the <see cref="WeightedRandom{T}.CollectiveWeight"/>.
        /// </summary>
        public float Percentage { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Value} - [{Weight}] with {Percentage}% chance";
        }
    }
}
