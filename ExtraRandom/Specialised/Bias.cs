namespace ExtraRandom.Specialised;

/// <summary>
/// Type which specifies what type of results are preferred when using <see cref="BiasedRandom"/>.
/// </summary>
public enum Bias
{
    /// <summary>
    /// Prefer smaller numbers.
    /// </summary>
    Lower,

    /// <summary>
    /// Prefer numbers close to the average.
    /// </summary>
    Average,

    /// <summary>
    /// Prefer larger numbers.
    /// </summary>
    Higher
}
