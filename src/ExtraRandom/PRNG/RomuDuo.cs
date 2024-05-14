using System.Buffers.Binary;
using System.Numerics;
using System.Security.Cryptography;

namespace ExtraRandom.PRNG;

/// <summary>
/// Romu random variations, might be faster than <see cref="RomuTrio"/> due to using fewer registers, but might struggle with massive jobs.
/// Est. capacity = 2^61 bytes. Register pressure = 5. State size = 128 bits.
/// Based on: https://github.com/Shiroechi/Litdex.Random/blob/main/Source/PRNG/RomuDuo.cs
/// </summary>
/// <remarks>
/// <para>
/// Source: https://www.romu-random.org/
/// </para>
/// </remarks>
public sealed class RomuDuo : Random64
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RomuDuo"/> class.
    /// </summary>
    /// <param name="baseSeed">Base seed to use for the random number generation.</param>
    /// <remarks>
    /// <para>
    /// <paramref name="baseSeed"/> is used as the base for the seed, three additional <see cref="ulong"/> variables are made,
    /// which each increments the <paramref name="baseSeed"/> value by one.
    /// </para>
    /// </remarks>
    public RomuDuo(ulong baseSeed)
        : this([baseSeed, baseSeed + 1]) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RomuDuo"/> class.
    /// </summary>
    /// <param name="seed"> RNG seed numbers.</param>
    private RomuDuo(ulong[] seed)
    {
        State = new ulong[2];
        SetSeed(seed);
    }

    /// <inheritdoc/>
    protected override ulong Next()
    {
        var xp = State[0];
        State[0] = 15241094284759029579u * State[1];
        State[1] =
            BitOperations.RotateLeft(State[1], 27) + BitOperations.RotateLeft(State[1], 15) - xp;
        return xp;
    }
}
