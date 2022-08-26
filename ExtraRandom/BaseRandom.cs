using ExtraRandom.PRNG;

namespace ExtraRandom;

/// <summary>
/// Base Random Generation class to create other Random Generations of.
/// </summary>
public abstract class BaseRandom
{
    // TODO: Write own Random Generation, based off of xoshiro**, and take some "inspiration" from:
    // https://github.com/Shiroechi/Litdex.Random/tree/main/Source

    /// <summary>
    /// The Random Generation Generator by Unity.Mathematics.
    /// </summary>
    protected readonly Shishua ShisuaRng;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRandom"/> class with a random seed.
    /// </summary>
    protected BaseRandom()
    {
        ShisuaRng = new Shishua();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRandom"/> class with the given <paramref name="seed"/>.
    /// </summary>
    /// <param name="seed">The seed that should be used to generate random numbers.</param>
    protected BaseRandom(int seed)
    {
        ShisuaRng = new Shishua(new[]
        {
            (ulong)seed,
            (ulong)seed + 1,
            (ulong)seed + 2,
            (ulong)seed + 3
        });
    }

    /// <summary>
    /// Generate a random <see cref="int"/> value.
    /// </summary>
    /// <returns>The generated number.</returns>
    public int NextInt()
    {
        return NextInt(0, int.MaxValue);
    }

    /// <summary>
    /// Generate a random <see cref="int"/> value where the maximum value is specified.
    /// </summary>
    /// <param name="max">The maximum value the generated number can be.</param>
    /// <returns>The generated number.</returns>
    public int NextInt(int max)
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
    /// Generate a random <see cref="float"/> value.
    /// </summary>
    /// <returns>The generated number.</returns>
    public float NextFloat()
    {
        return NextFloat(0, float.MaxValue);
    }

    /// <summary>
    /// Generate a random <see cref="float"/> value where the maximum value is specified.
    /// </summary>
    /// <param name="max">The maximum value the generated number can be.</param>
    /// <returns>The generated number.</returns>
    public float NextFloat(float max)
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
    /// Generate a random <see cref="long"/> value.
    /// </summary>
    /// <returns>The generated number.</returns>
    public long NextLong()
    {
        return NextLong(0, long.MaxValue);
    }

    /// <summary>
    /// Generate a random <see cref="long"/> value where the maximum value is specified.
    /// </summary>
    /// <param name="max">The maximum value the generated number can be.</param>
    /// <returns>The generated number.</returns>
    public long NextLong(long max)
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
    /// Generate a random <see cref="double"/> value.
    /// </summary>
    /// <returns>The generated number.</returns>
    public double NextDouble()
    {
        return NextDouble(0, double.MaxValue);
    }

    /// <summary>
    /// Generate a random <see cref="double"/> value where the maximum value is specified.
    /// </summary>
    /// <param name="max">The maximum value the generated number can be.</param>
    /// <returns>The generated number.</returns>
    public double NextDouble(double max)
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