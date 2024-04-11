using System.Diagnostics.CodeAnalysis;

namespace ExtraRandom;

/// <summary>
/// Base class for random number generator in which the internal state produces a 64-bit output.
/// </summary>
[SuppressMessage(
    "StyleCop.CSharp.OrderingRules",
    "SA1202:Elements should be ordered by access",
    Justification = "The order makes more sense to me like this."
)]
public abstract class Random64 : Random
{
    /// <summary>
    /// Size is 8 bytes.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    protected const byte Size = 8;

    /// <summary>
    /// Internal state of the RNG.
    /// </summary>
#pragma warning disable SA1401, SA1306
    protected ulong[] State = [];
#pragma warning restore SA1306, SA1401

    /// <summary>
    /// Generate next random number.
    /// </summary>
    /// <returns>
    /// A 64-bit unsigned integer.
    /// </returns>
    protected abstract ulong Next();

    /// <inheritdoc cref="IRandom.SetSeed(ulong)" />
    public override void SetSeed(ulong seed)
    {
        var seedCollection = new ulong[State.Length];
        for (var i = 0; i < State.Length; i++)
        {
            seedCollection[i] = seed + (ulong)i;
        }
        SetSeed(seedCollection);
    }

    /// <inheritdoc cref="IRandom.SetSeed(ulong[])" />
    /// <exception cref="ArgumentNullException">Array of seed is null or empty.</exception>
    /// <exception cref="ArgumentException">Seed amount must be at least the same length as the state length.</exception>
    public override void SetSeed(params ulong[] seed)
    {
        if (seed == null || seed.Length == 0)
        {
            throw new ArgumentNullException(nameof(seed), "Seed can't be null or empty.");
        }

        if (seed.Length != State.Length)
        {
            throw new ArgumentException($"Seed must be at exactly {State.Length} long", nameof(seed));
        }

        Array.Copy(seed, 0, State, 0, State.Length);
    }

    /// <inheritdoc />
    public override bool NextBoolean() => Next() >> 63 == 0;

    /// <inheritdoc />
    public override byte NextByte() => (byte)(Next() >> 56);

    /// <inheritdoc />
    public override int NextInt() => (int)(Next() >> 33);

    /// <inheritdoc />
    public override uint NextUInt() => (uint)(Next() >> 32);

    /// <inheritdoc />
    public override long NextLong() => (long)(Next() >> 1);

    /// <inheritdoc />
    public override ulong NextULong() => Next();
}
