using System.Numerics;

namespace ExtraRandom.PRNG;

/// <summary>
/// RomuDuoJr is a simplification of <see cref="RomuDuo"/>, removing a rotation and addition.
/// The reduced number of operations and register pressure make it the fastest generator presented in the paper using 64-bit arithmetic.
/// RomuDuoJr is suitable for most applications, and it should be preferred when speed is paramount.
/// However, a large job can exceed its reduced capacity, so one must be careful.
/// </summary>
/// <remarks>
/// <para>
/// Paper: https://arxiv.org/pdf/2002.11331
/// </para>
/// </remarks>
public sealed class RomuDuoJr : Random64
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RomuDuoJr"/> class.
    /// </summary>
    /// <param name="baseSeed">Base seed to use for the random number generation.</param>
    /// <remarks>
    /// <para>
    /// <paramref name="baseSeed"/> is used as the base for the seed, three additional <see cref="ulong"/> variables are made,
    /// which each increments the <paramref name="baseSeed"/> value by one.
    /// </para>
    /// </remarks>
    public RomuDuoJr(ulong baseSeed)
        : this([baseSeed, baseSeed + 1]) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RomuDuoJr"/> class.
    /// </summary>
    public RomuDuoJr(ulong seed1, ulong seed2)
        : this([seed1, seed2]) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RomuDuoJr"/> class.
    /// </summary>
    /// <param name="seed"> RNG seed numbers.</param>
    private RomuDuoJr(ulong[] seed)
    {
        State = new ulong[2];
        SetSeed(seed);
    }

    /// <inheritdoc/>
    protected override ulong Next()
    {
        var xp = State[0];
        State[0] = 15241094284759029579u * State[1];
        State[1] = BitOperations.RotateLeft(State[1] - xp, 27);

        return State[0];
    }
}
