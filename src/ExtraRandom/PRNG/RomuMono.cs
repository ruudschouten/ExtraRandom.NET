using System.Numerics;

namespace ExtraRandom.PRNG;

/// <summary>
///  RomuMono32 is tiny, consisting of only those two arithmetic operations plus one more to extract the returned result.
/// </summary>
/// <remarks>
/// <para>
/// Paper: https://arxiv.org/pdf/2002.11331
/// </para>
/// </remarks>
public sealed class RomuMono : Random32
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RomuMono"/> class.
    /// </summary>
    /// <param name="seed"> RNG seed numbers.</param>
    public RomuMono(ulong seed)
    {
        State = [(seed & 0x1fffffffu) + 1156979152u];
    }

    /// <inheritdoc />
    protected override uint Next()
    {
        State[0] = BitOperations.RotateLeft(State[0] * 3611795771u, 12);
        return (ushort)(State[0] >> 16);
    }
}
