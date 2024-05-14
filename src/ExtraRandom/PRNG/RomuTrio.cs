using System.Buffers.Binary;
using System.Numerics;
using System.Security.Cryptography;

namespace ExtraRandom.PRNG;

/// <summary>
/// Romu random variations, great for general purpose work, including huge jobs.
/// Est. capacity = 2^75 bytes. Register pressure = 6. State size = 192 bits.
/// Based on: https://github.com/Shiroechi/Litdex.Random/blob/main/Source/PRNG/RomuTrio.cs.
/// </summary>
/// <remarks>
/// <para>
/// Source: https://www.romu-random.org/
/// </para>
/// </remarks>
public sealed class RomuTrio : Random64
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RomuTrio"/> class.
    /// </summary>
    /// <param name="baseSeed">Base seed to use for the random number generation.</param>
    /// <remarks>
    /// <para>
    /// <paramref name="baseSeed"/> is used as the base for the seed, three additional <see cref="ulong"/> variables are made,
    /// which each increments the <paramref name="baseSeed"/> value by one.
    /// </para>
    /// </remarks>
    public RomuTrio(ulong baseSeed)
        : this([baseSeed, baseSeed + 1, baseSeed + 2]) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RomuTrio"/> class.
    /// </summary>
    /// <param name="seed"> RNG seed numbers.</param>
    private RomuTrio(ulong[] seed)
    {
        State = new ulong[3];
        SetSeed(seed);
    }

    /// <inheritdoc />
    protected override ulong Next()
    {
        var x = State[0];
        var y = State[1];
        var z = State[2];

        State[0] = 15241094284759029579u * z;
        State[1] = BitOperations.RotateLeft(y - x, 12);
        State[2] = BitOperations.RotateRight(z - y, 44);

        return x;
    }
}
