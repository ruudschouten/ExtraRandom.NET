using System.Numerics;

namespace ExtraRandom.PRNG;

/// <summary>
/// <para>Xoroshiro128+ PRNG implementation.</para>
/// <para>
/// The best and fastest Xoroshiro small-state generator for floating-point numbers,
/// but its state space is large enough only for mild parallelism.
/// We suggest to use its upper bits for floating-point generation,
/// as it is slightly faster than <see cref="Xoroshiro128PlusPlus">Xoroshiro128++</see>/<see cref="Xoroshiro128StarStar">Xoroshiro128**</see>.
/// </para>
/// <para>
/// It passes all tests we are aware of except for the four lower bits,
/// which might fail linearity tests (and just those),
/// so if low linear complexity is not considered an issue (as it is usually the case) it can be used to generate 64-bit outputs, too.
/// moreover, this generator has a very mild Hamming-weight dependency.
/// If you are concerned, use <see cref="Xoroshiro128PlusPlus">Xoroshiro128++</see>, <see cref="Xoroshiro128StarStar">Xoroshiro128**</see>
/// or <see cref="Xoshiro256Plus">Xoroshiro256+</see>.
/// </para>
/// </summary>
/// <remarks>
/// <para>Source: https://prng.di.unimi.it/xoroshiro128plus.c</para>
/// </remarks>
public sealed class Xoroshiro128Plus : Random64
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Xoroshiro128Plus"/> class.
    /// </summary>
    /// <param name="baseSeed">Base seed to use for the random number generation.</param>
    public Xoroshiro128Plus(ulong baseSeed = 0)
        : this(baseSeed, baseSeed + 1) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Xoroshiro128Plus"/> class.
    /// </summary>
    /// <param name="seed1">First seed.</param>
    /// <param name="seed2">Second seed.</param>
    public Xoroshiro128Plus(ulong seed1, ulong seed2)
    {
        State = new ulong[2];
        SetSeed(seed1, seed2);
    }

    /// <inheritdoc />
    protected override ulong Next()
    {
        var s0 = State[0];
        var s1 = State[1];
        var result = s0 + s1;

        s1 ^= s0;
        State[0] = BitOperations.RotateLeft(s0, 24) ^ s1 ^ (s1 << 16);
        State[1] = BitOperations.RotateLeft(s1, 37);

        return result;
    }
}
