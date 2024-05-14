using System.Numerics;

namespace ExtraRandom.PRNG;

/// <summary>
/// RomuDuo will be faster than <see cref="RomuTrio"/> when its lower register pressure causes fewer memory-spills.
/// Its state size of 128 bits is large enough to support many streams with essentially no chance of overlap.
/// The estimated capacity of RomuDuo is 258 values (261 bytes), which is significantly lower than that of <see cref="RomuTrio"/> or <see cref="RomuQuad"/>.
/// But this lower capacity is still larger than any job, and it would take 9 years to consume it at the rate of one per nanosecond.
/// </summary>
/// <remarks>
/// <para>
/// Paper: https://arxiv.org/pdf/2002.11331
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
    public RomuDuo(ulong seed1, ulong seed2)
        : this([seed1, seed2]) { }

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
        return State[0];
    }
}
