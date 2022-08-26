namespace ExtraRandom;

using ExtraRandom.PRNG;

/// <summary>
/// A wrapper around the <see cref="Xoroshiro128Plus"/> Psuedo-Random Number Generator."/>
/// </summary>
public class XoroshiroRandom : BaseRandom
{
    /// <summary>
    /// A Psuedo-Random Number Generator based on the <a href="https://prng.di.unimi.it/">Xoshiro / Xoroshiro generators</a>.
    /// </summary>
    private Xoroshiro128Plus _xoroshiro;

    /// <summary>
    /// Initializes a new instance of the <see cref="XoroshiroRandom"/> class with a random seed.
    /// </summary>
    public XoroshiroRandom()
    {
        _xoroshiro = new Xoroshiro128Plus(new Random());
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XoroshiroRandom"/> class with the given <paramref name="seed"/>
    /// </summary>
    /// <param name="seed">The seed that should be used to generate random numbers.</param>
    public XoroshiroRandom(int seed)
    {
        _xoroshiro = new Xoroshiro128Plus(seed);
    }

    /// <inheritdoc/>
    public override void Reseed(int seed)
    {
        _xoroshiro.Reseed(seed);
    }

    /// <inheritdoc/>
    public override int NextInt(int min, int max)
    {
        return Convert.ToInt32(NextFloat(min, max));
    }

    /// <inheritdoc/>
    public override float NextFloat(float min, float max)
    {
        return (_xoroshiro.NextFloat() * (max - min)) + min;
    }

    /// <inheritdoc/>
    public override long NextLong(long min, long max)
    {
        return Convert.ToInt64(NextDouble(min, max));
    }

    /// <inheritdoc/>
    public override double NextDouble(double min, double max)
    {
        return (_xoroshiro.NextDouble() * (max - min)) + min;
    }
}