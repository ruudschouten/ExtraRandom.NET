using System.Numerics;

namespace ExtraRandom.PRNG;

/// <summary>
/// <para>Xoroshiro128++ PRNG implementation.</para>
/// <para>
/// All-purpose, rock-solid, small-state generators.
/// It is extremely (sub-ns) fast and passes all tests we are aware of, but its state space is large enough only for mild parallelism.
/// </para>
/// <para>For generating just floating-point numbers,<see cref="Xoroshiro128Plus">Xoroshiro128+</see> is even faster</para>
/// <para>
/// The state must be seeded so that it is not everywhere zero.
/// </para>
/// </summary>
/// <remarks>
/// <para>Source: https://prng.di.unimi.it/xoroshiro128plusplus.c</para>
/// </remarks>
public sealed class Xoroshiro128PlusPlus : Random64
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Xoroshiro128PlusPlus"/> class.
    /// </summary>
    /// <param name="baseSeed">Base seed to use for the random number generation.</param>
    public Xoroshiro128PlusPlus(ulong baseSeed = 0)
        : this(baseSeed, baseSeed + 1) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Xoroshiro128PlusPlus"/> class.
    /// </summary>
    /// <param name="seed1">First seed.</param>
    /// <param name="seed2">Second seed.</param>
    public Xoroshiro128PlusPlus(ulong seed1, ulong seed2)
    {
        State = new ulong[2];
        SetSeed(seed1, seed2);
    }

    /// <inheritdoc />
    protected override ulong Next()
    {
        var s0 = State[0];
        var s1 = State[1];
        var result = BitOperations.RotateLeft(s0 + s1, 17) + s0;

        s1 ^= s0;
        State[0] = BitOperations.RotateLeft(s0, 49) ^ s1 ^ (s1 << 21);
        State[1] = BitOperations.RotateLeft(s1, 28);

        return result;
    }
}
