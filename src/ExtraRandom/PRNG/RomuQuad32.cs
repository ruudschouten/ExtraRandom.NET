using System.Numerics;

namespace ExtraRandom.PRNG;

/// <summary>
/// This 128-bit generator is suitable for large jobs, is very fast (with no output latency), and has excellent statistical quality.
/// It is the only Romu generator using 32-bit arithmetic this paper recommends for general purpose use.
/// However, as mentioned above, <see cref="RomuTrio32"/> is faster due to its lower register pressure, so we recommend employing it when feasible.
/// </summary>
/// <remarks>
/// <para>
/// Paper: https://arxiv.org/pdf/2002.11331
/// </para>
/// </remarks>
public sealed class RomuQuad32 : Random32
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RomuQuad32"/> class.
    /// </summary>
    public RomuQuad32(ulong seed1, ulong seed2, ulong seed3, ulong seed4)
        : this([seed1, seed2, seed3, seed4]) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RomuQuad32"/> class.
    /// </summary>
    /// <param name="seed"> RNG seed numbers.</param>
    private RomuQuad32(ulong[] seed)
    {
        State = new ulong[4];
        SetSeed(seed);
    }

    /// <inheritdoc />
    protected override uint Next()
    {
        var wp = State[0];
        var xp = State[1];
        var yp = State[2];
        var zp = State[3];

        State[0] = 3323815723u * zp;
        State[1] = zp + BitOperations.RotateLeft(wp, 26);
        State[2] = yp - xp;
        State[3] = BitOperations.RotateLeft(yp + wp, 9);

        return (uint)State[1];
    }
}
