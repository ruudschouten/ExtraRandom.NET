namespace ExtraRandom;

/// <summary>
/// Interface that all Random classes should inherit from.
/// </summary>
public interface IRandom
{
    /// <summary>
    /// Reseed the RNG.
    /// </summary>
    void Reseed();

    /// <summary>
    /// Manually set the internal RNG state with a single value.
    /// Will call <see cref="SetSeed(ulong[])"/>.
    /// </summary>
    void SetSeed(ulong seed);

    /// <summary>
    /// Manually set the internal RNG state.
    /// </summary>
    /// <param name="seed">Seed to use to generate random numbers.</param>
    void SetSeed(params ulong[] seed);

    /// <summary>
    /// Generate <see cref="T:System.Boolean" /> value.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> or <see langword="false" />.
    /// </returns>
    bool NextBoolean();

    /// <summary>
    /// Generate a non-negative random integer.
    /// </summary>
    /// <returns>A 8-bit unsigned integer.</returns>
    /// <remarks>
    /// <para>Generated value is between 0 and the <see cref="byte.MaxValue"/> of a <see cref="byte"/>.</para>
    /// </remarks>
    byte NextByte();

    /// <summary>
    /// Generate a <see cref="byte"/> within the specified <paramref name="min"/> and <paramref name="max"/> range.
    /// </summary>
    /// <param name="min">Inclusive lower bound.</param>
    /// <param name="max">Exclusive upper bound.</param>
    /// <returns>A 8-bit integer between the <paramref name="min"/> and <paramref name="max"/> values.</returns>
    byte NextByte(byte min, byte max);

    /// <summary>
    /// Generate a <see cref="int"/>.
    /// </summary>
    /// <returns>A 32-bit signed integer.</returns>
    /// <remarks>
    /// <para>Generated value is between 0 and <see cref="int.MaxValue"/> value.</para>
    /// </remarks>
    int NextInt();

    /// <summary>
    /// Generate a <see cref="int"/>.
    /// </summary>
    /// <param name="min">Inclusive lower bound.</param>
    /// <param name="max">Exclusive upper bound.</param>
    /// <returns>A 32-bit signed integer between the <paramref name="min"/> and <paramref name="max"/> values.</returns>
    /// <remarks>
    /// <para>Generated value is between 0 and the provided <paramref name="max"/> value.</para>
    /// </remarks>
    int NextInt(int min, int max);

    /// <summary>
    /// Generate a <see cref="uint"/>.
    /// </summary>
    /// <returns>A 32-bit unsigned integer.</returns>
    uint NextUInt();

    /// <summary>
    /// Generate a <see cref="uint"/>.
    /// </summary>
    /// <param name="min">Inclusive lower bound.</param>
    /// <param name="max">Exclusive upper bound.</param>
    /// <returns>A 32-bit unsigned integer between the <paramref name="min"/> and <paramref name="max"/> values.</returns>
    uint NextUInt(uint min, uint max);

    /// <summary>
    /// Generate a <see cref="long"/>.
    /// </summary>
    /// <returns>A 64-bit signed integer.</returns>
    /// <remarks>
    /// <para>Generated value is between 0 and <see cref="long.MaxValue"/>.</para>
    /// </remarks>
    long NextLong();

    /// <summary>
    /// Generate a <see cref="uint"/>.
    /// </summary>
    /// <param name="min">Inclusive lower bound.</param>
    /// <param name="max">Exclusive upper bound.</param>
    /// <returns>A 64-bit unsigned integer between the <paramref name="min"/> and <paramref name="max"/> values.</returns>
    long NextLong(long min, long max);

    /// <summary>
    /// Generate a <see cref="ulong"/>.
    /// </summary>
    /// <returns>A 64-bit unsigned integer.</returns>
    ulong NextULong();

    /// <summary>
    /// Generate a <see cref="ulong"/>.
    /// </summary>
    /// <param name="min">Inclusive lower bound.</param>
    /// <param name="max">Exclusive upper bound.</param>
    /// <returns>A 64-bit unsigned integer between the <paramref name="min"/> and <paramref name="max"/> values.</returns>
    ulong NextULong(ulong min, ulong max);

    /// <summary>
    /// Generate a <see cref="double"/>.
    /// </summary>
    /// <returns>A double-precision floating point number.</returns>
    /// <remarks><para>Value is between 0.0 and 1.0.</para></remarks>
    double NextDouble();

    /// <summary>
    /// Generate a <see cref="double"/>.
    /// </summary>
    /// <param name="min">Inclusive lower bound.</param>
    /// <param name="max">Exclusive upper bound.</param>
    /// <returns>A double-precision floating point number between the <paramref name="min"/> and <paramref name="max"/> values.</returns>
    double NextDouble(double min, double max);
}
