using System.Numerics;

namespace ExtraRandom.PRNG;

/// <summary>
/// <para>Xoshiro256++</para>
/// <para>
/// An all-purpose, rock-solid generators.
/// It has excellent (sub-ns) speed, a state (256 bits) that is large enough for any parallel application, and it passes all tests we are aware of.
/// For generating just floating-point numbers, <see cref="Xoshiro256Plus">Xoshiro256+</see> is even faster.
/// </para>
/// <para>
/// The state must be seeded so that it is not everywhere zero.
/// </para>
/// </summary>
/// <remarks>
/// <para>Source: https://prng.di.unimi.it/xoshiro256plusplus.c</para>
/// </remarks>
public sealed class Xoshiro256PlusPlus : Random64
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Xoshiro256Plus"/> class.
    /// </summary>
    /// <param name="baseSeed">Base seed to use for the random number generation.</param>
    public Xoshiro256PlusPlus(ulong baseSeed = 0)
        : this(baseSeed, baseSeed + 1, baseSeed + 2, baseSeed + 3) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Xoshiro256Plus"/> class.
    /// </summary>
    /// <param name="seed1">First seed.</param>
    /// <param name="seed2">Second seed.</param>
    /// <param name="seed3">Third seed.</param>
    /// <param name="seed4">Fourth seed.</param>
    public Xoshiro256PlusPlus(ulong seed1, ulong seed2, ulong seed3, ulong seed4)
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

        return BitOperations.RotateLeft(State[0] + State[3], 23) + State[0];
    }
}
