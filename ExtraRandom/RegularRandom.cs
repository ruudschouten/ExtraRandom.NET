namespace ExtraRandom;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// A basic wrapper class around the regular .NET <see cref="System.Random"/>.
/// </summary>
[SuppressMessage(
    "Security",
    "SCS0005:Weak random number generator.",
    Justification = "Weak number generation is acceptable for this class."
)]
public class RegularRandom : BaseRandom
{
    /// <summary>
    /// The Random Generation Generator by .NET.
    /// </summary>
    private System.Random _random;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegularRandom"/> class with a random seed.
    /// </summary>
    public RegularRandom()
    {
        _random = new System.Random();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RegularRandom"/> class with the provided <paramref name="seed"/>.
    /// </summary>
    /// <param name="seed">seed that should be used to generate random numbers.</param>
    public RegularRandom(int seed)
    {
        _random = new System.Random(seed);
    }

    /// <inheritdoc/>
    public override void Reseed(int seed)
    {
        _random = new System.Random(seed);
    }

    /// <inheritdoc/>
    public override int NextInt(int min, int max)
    {
        return _random.Next(min, max);
    }

    /// <inheritdoc/>
    public override float NextFloat(float min, float max)
    {
        return (_random.NextSingle() * (max - min)) + min;
    }

    /// <inheritdoc/>
    public override long NextLong(long min, long max)
    {
        return _random.NextInt64(min, max);
    }

    /// <inheritdoc/>
    public override double NextDouble(double min, double max)
    {
        return (_random.NextDouble() * (max - min)) + min;
    }
}
