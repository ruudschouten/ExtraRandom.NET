using System.Numerics;

namespace ExtraRandom.PRNG;

/// <summary>
/// The state of RomuQuad comprises four 64-bit variables (256 bits).
/// Its processing time is 3 cycles, and it has no output latency, allowing an application to execute as soon as possible.
/// This is the best 64-bit Romu generator presented in the paper, but it also has the highest register pressure, which is 8.
/// <see cref="RomuTrio"/> is recommended instead due to its lower register pressure of 6.
/// But RomuQuad can be employed for massive jobs run by users who choose to be extremely cautious about the probability of overlap.
/// </summary>
/// <remarks>
/// <para>
/// Paper: https://arxiv.org/pdf/2002.11331
/// </para>
/// </remarks>
public sealed class RomuQuad : Random64
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RomuQuad"/> class.
    /// </summary>
    /// <param name="baseSeed">Base seed to use for the random number generation.</param>
    /// <remarks>
    /// <para>
    /// <paramref name="baseSeed"/> is used as the base for the seed, three additional <see cref="ulong"/> variables are made,
    /// which each increments the <paramref name="baseSeed"/> value by one.
    /// </para>
    /// </remarks>
    public RomuQuad(ulong baseSeed)
        : this([baseSeed, baseSeed + 1, baseSeed + 2]) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RomuQuad"/> class.
    /// </summary>
    public RomuQuad(ulong seed1, ulong seed2, ulong seed3, ulong seed4)
        : this([seed1, seed2, seed3, seed4]) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RomuQuad"/> class.
    /// </summary>
    /// <param name="seed"> RNG seed numbers.</param>
    private RomuQuad(ulong[] seed)
    {
        State = new ulong[4];
        SetSeed(seed);
    }

    /// <inheritdoc />
    protected override ulong Next()
    {
        var wp = State[0];
        var xp = State[1];
        var yp = State[2];
        var zp = State[3];

        State[0] = 15241094284759029579u * zp;
        State[1] = zp + BitOperations.RotateLeft(wp, 52);
        State[2] = yp - xp;
        State[3] = BitOperations.RotateLeft(yp + wp, 19);

        return State[1];
    }
}
