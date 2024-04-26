using System.Diagnostics.CodeAnalysis;

namespace ExtraRandom;

/// <summary>
/// Base class for random number generator in which the internal state produces a 32-bit output.
/// </summary>
[SuppressMessage(
    "StyleCop.CSharp.OrderingRules",
    "SA1202:Elements should be ordered by access",
    Justification = "The order makes more sense to me like this."
)]
public abstract class Random32 : Random
{
    /// <summary>
    /// Size is 8 bytes.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    protected const byte Size = 4;

    /// <summary>
    /// Internal state of the RNG.
    /// </summary>
#pragma warning disable SA1401, SA1306
    protected ulong[] State = Array.Empty<ulong>();
#pragma warning restore SA1401, SA1306

    /// <summary>
    /// Generate next random number.
    /// </summary>
    /// <returns>
    /// A 64-bit unsigned integer.
    /// </returns>
    protected abstract uint Next();

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
    /// <exception cref="ArgumentException">Seed amount must be at least the same length as the <see cref="State"/> length.</exception>
    public override void SetSeed(params ulong[] seed)
    {
        if (seed == null || seed.Length == 0)
        {
            throw new ArgumentNullException(nameof(seed), "Seed can't be null or empty.");
        }

        if (seed.Length != State.Length)
        {
            throw new ArgumentException($"Seed must be at least {State.Length} long", nameof(seed));
        }

        // Change the length to whichever field is larger.
        var length = seed.Length > State.Length ? State.Length : seed.Length;
        Array.Copy(seed, 0, State, 0, length);
    }

    /// <inheritdoc />
    public override bool NextBoolean() => NextUInt() >> 31 == 0;

    /// <inheritdoc />
    public override byte NextByte() => (byte)(Next() >> 24);

    /// <inheritdoc />
    public override int NextInt() => (int)(Next() >> 1);

    /// <inheritdoc />
    public override uint NextUInt() => Next();

    /// <inheritdoc />
    public override long NextLong() => Next() >> 1;

    /// <inheritdoc />
    public override ulong NextULong()
    {
        ulong high = NextUInt();
        ulong low = NextUInt();
        return high << 32 | low;
    }
}
