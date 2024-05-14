using System.Buffers.Binary;
using System.Numerics;
using System.Security.Cryptography;

namespace ExtraRandom.PRNG;

/// <summary>
/// <para>
/// Xoroshiro128+ PRNG implementation.
/// </para>
/// <para>
/// Based on https://github.com/Shiroechi/Litdex.Random/blob/main/Source/PRNG/Xoroshiro128Plus.cs
/// </para>
/// </summary>
/// <remarks>
/// <para>
/// Source: https://prng.di.unimi.it/xoroshiro128plus.c
/// </para>
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
    private Xoroshiro128Plus(ulong seed1, ulong seed2)
    {
        State = new ulong[2];
        SetSeed(seed1, seed2);
    }

    /// <inheritdoc />
    protected override ulong Next()
    {
        var s0 = State[0];
        var s1 = State[1];
        var result = State[0] + State[1];

        s1 ^= s0;
        var seed1 = BitOperations.RotateLeft(s0, 24) ^ s1 ^ (s1 << 16);
        var seed2 = BitOperations.RotateLeft(s1, 37);

        SetSeed(seed1, seed2);

        return result;
    }
}
