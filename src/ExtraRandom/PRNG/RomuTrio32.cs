using System.Numerics;

namespace ExtraRandom.PRNG;

/// <summary>
/// With 96 bits of state, this generator is not suitable for the largest jobs.
/// But it can supply the needs of most applications.
/// Because its register pressure is lower than <see cref="RomuQuad32"/>, there will be fewer spills, causing an application to run faster.
/// </summary>
/// <remarks>
/// <para>
/// Paper: https://arxiv.org/pdf/2002.11331
/// </para>
/// </remarks>
public sealed class RomuTrio32 : Random32
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RomuTrio32"/> class.
    /// </summary>
    public RomuTrio32(ulong seed1, ulong seed2, ulong seed3)
        : this([seed1, seed2, seed3]) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RomuTrio32"/> class.
    /// </summary>
    /// <param name="seed"> RNG seed numbers.</param>
    private RomuTrio32(ulong[] seed)
    {
        State = new ulong[3];
        SetSeed(seed);
    }

    /// <inheritdoc />
    protected override uint Next()
    {
        var xp = State[0];
        var yp = State[1];
        var zp = State[2];

        State[0] = 3323815723u * zp;
        State[1] = BitOperations.RotateLeft(yp - xp, 6);
        State[2] = BitOperations.RotateLeft(zp - yp, 22);

        return (uint)State[0];
    }
}
