namespace ExtraRandom.PRNG;

using System.Numerics;

/// <summary>
/// <para>Xoroshiro256+ PRNG implementation.
/// The best and fastest Xoroshiro generator for floating-point numbers.
/// </para>
/// <para>
/// We suggest to use its upper bits for floating-point generation, as it is slightly faster than <see cref="Xoshiro256PlusPlus">Xoshiro256++</see>/<see cref="Xoshiro256StarStar">Xoshiro256**</see>.
/// It passes all tests we are aware of except for the lowest three bits, which might fail linearity tests (and just those),
/// so if low linear complexity is not considered an issue (as it is usually the case) it can be used to generate 64-bit outputs, too.</para>
/// <para>We suggest to use a sign test to extract a random Boolean value, and right shifts to extract subsets of bits.</para>
/// <para>The state must be seeded so that it is not everywhere zero.</para>
/// </summary>
/// <remarks>
/// <para>Source: https://prng.di.unimi.it/xoshiro256plus.c</para>
/// </remarks>
public sealed class Xoshiro256Plus : Random64
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Xoshiro256Plus"/> class.
    /// </summary>
    /// <param name="baseSeed">Base seed to use for the random number generation.</param>
    public Xoshiro256Plus(ulong baseSeed = 0)
        : this(baseSeed, baseSeed + 1, baseSeed + 2, baseSeed + 3) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Xoshiro256Plus"/> class.
    /// </summary>
    /// <param name="seed1">First seed.</param>
    /// <param name="seed2">Second seed.</param>
    /// <param name="seed3">Third seed.</param>
    /// <param name="seed4">Fourth seed.</param>
    public Xoshiro256Plus(ulong seed1, ulong seed2, ulong seed3, ulong seed4)
    {
        State = new ulong[4];
        SetSeed(seed1, seed2, seed3, seed4);
    }

    /// <inheritdoc />
    protected override ulong Next()
    {
        var t = State[1] << 17;

        State[2] ^= State[0];
        State[3] ^= State[1];
        State[1] ^= State[2];
        State[0] ^= State[3];

        State[2] ^= t;

        State[3] = BitOperations.RotateLeft(State[3], 45);

        return State[0] + State[3];
    }
}
