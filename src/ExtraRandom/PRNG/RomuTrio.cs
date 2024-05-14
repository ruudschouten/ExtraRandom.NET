using System.Numerics;

namespace ExtraRandom.PRNG;

/// <summary>
/// RomuTrio has 192 (3×64) bits of state.
/// RomuTrio easily passes all statistical tests.
/// <see cref="RomuQuad"/> is fast, but RomuTrio is faster due to its lower register pressure and sparser ILP table.
/// For these reasons, this generator is recommended most highly.
/// </summary>
/// <remarks>
/// <para>
/// Paper: https://arxiv.org/pdf/2002.11331
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
    public RomuTrio(ulong seed1, ulong seed2, ulong seed3)
        : this([seed1, seed2, seed3]) { }

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
        var xp = State[0];
        var yp = State[1];
        var zp = State[2];

        State[0] = 15241094284759029579u * zp;
        State[1] = BitOperations.RotateLeft(yp - xp, 12);
        State[2] = BitOperations.RotateLeft(zp - yp, 44);

        return State[0];
    }
}
