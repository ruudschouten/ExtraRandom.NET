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
    protected BaseRandom()
    {
    }

    /// <summary>
    /// </summary>
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