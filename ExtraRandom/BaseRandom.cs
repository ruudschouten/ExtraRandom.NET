namespace ExtraRandom;

using ExtraRandom.PRNG;

/// <summary>
/// Base Random Generation class to create other Random Generations of.
/// </summary>
public abstract class BaseRandom
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRandom"/> class with a random seed.
    /// </summary>
    protected BaseRandom() { }

    /// <summary>
    /// Reseed the psuedo-random number generation.
    /// </summary>
    /// <param name="seed">new seed to use for generation.</param>
    public abstract void Reseed(int seed);

    /// <summary>
    /// Generate a random <see cref="int"/> value where the maximum value is specified.
    /// </summary>
    /// <param name="max">
    /// The maximum value the generated number can be.
    /// <para><b>Default</b>: int.<see cref="int.MaxValue"/>.</para>
    /// </param>
    /// <returns>The generated number.</returns>
    public int NextInt(int max = int.MaxValue)
    {
        return NextInt(0, max);
    }

    /// <summary>
    /// Generate a random <see cref="int"/> value where the minimum and maximum value are specified.
    /// </summary>
    /// <param name="min">The minimum value the generated number can be.</param>
    /// <param name="max">The maximum value the generated number can be.</param>
    /// <returns>The generated number.</returns>
    public abstract int NextInt(int min, int max);

    /// <summary>
    /// Generate a random <see cref="float"/> value where the maximum value is specified.
    /// </summary>
    /// <param name="max">
    /// The maximum value the generated number can be.
    /// <para><b>Default</b>: 1f.</para>
    /// </param>
    /// <returns>The generated number.</returns>
    public float NextFloat(float max = 1f)
    {
        return NextFloat(0, max);
    }

    /// <summary>
    /// Generate a random <see cref="float"/> value where the minimum and maximum value are specified.
    /// </summary>
    /// <param name="min">The minimum value the generated number can be.</param>
    /// <param name="max">The maximum value the generated number can be.</param>
    /// <returns>The generated number.</returns>
    public abstract float NextFloat(float min, float max);

    /// <summary>
    /// Generate a random <see cref="long"/> value where the maximum value is specified.
    /// </summary>
    /// <param name="max">
    /// The maximum value the generated number can be.
    /// <para><b>Default</b>: long.<see cref="long.MaxValue"/>.</para>
    /// </param>
    /// <returns>The generated number.</returns>
    public long NextLong(long max = long.MaxValue)
    {
        return NextLong(0, max);
    }

    /// <summary>
    /// Generate a random <see cref="long"/> value where the minimum and maximum value are specified.
    /// </summary>
    /// <param name="min">The minimum value the generated number can be.</param>
    /// <param name="max">The maximum value the generated number can be.</param>
    /// <returns>The generated number.</returns>
    public abstract long NextLong(long min, long max);

    /// <summary>
    /// Generate a random <see cref="double"/> value where the maximum value is specified.
    /// </summary>
    /// <param name="max">
    /// The maximum value the generated number can be.
    /// <para><b>Default</b>: 1.0.</para>
    /// </param>
    /// <returns>The generated number.</returns>
    public double NextDouble(double max = 1.0)
    {
        return NextDouble(0, max);
    }

    /// <summary>
    /// Generate a random <see cref="double"/> value where the minimum and maximum value are specified.
    /// </summary>
    /// <param name="min">The minimum value the generated number can be.</param>
    /// <param name="max">The maximum value the generated number can be.</param>
    /// <returns>The generated number.</returns>
    public abstract double NextDouble(double min, double max);
}
