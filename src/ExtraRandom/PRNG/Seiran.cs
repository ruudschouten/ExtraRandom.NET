using System.Numerics;

namespace ExtraRandom.PRNG;

/// <summary>
/// Linear-feedback shift register-based pseudorandom number generators.
/// Based on: https://github.com/Shiroechi/Litdex.Random/blob/main/Source/PRNG/Seiran.cs.
/// </summary>
/// <remarks>
/// <para>
/// Source: https://github.com/andanteyk/prng-seiran
/// </para>
/// </remarks>
public sealed class Seiran : Random64
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Seiran"/> class.
    /// </summary>
    /// <param name="baseSeed">Base seed to use for the random number generation.</param>
    /// <remarks>
    /// <para>
    /// <paramref name="baseSeed"/> is used as the base for the seed, three additional <see cref="ulong"/> variables are made,
    /// which each increments the <paramref name="baseSeed"/> value by one.
    /// </para>
    /// </remarks>
    public Seiran(ulong baseSeed)
        : this([baseSeed, baseSeed + 1]) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Seiran"/> class.
    /// </summary>
    public Seiran(ulong seed1, ulong seed2)
        : this([seed1, seed2]) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Seiran"/> class.
    /// </summary>
    /// <param name="seed">Seed to use for the random number generation.</param>
    private Seiran(ulong[] seed)
    {
        State = new ulong[2];
        SetSeed(seed);
    }

    /// <inheritdoc />
    protected override ulong Next()
    {
        var s0 = State[0];
        var s1 = State[1];

        State[0] = s0 ^ BitOperations.RotateLeft(s1, 29);
        State[1] = s0 ^ s1 << 9;

        return BitOperations.RotateLeft((State[0] + State[1]) * 9, 29) + State[0];
    }
}
