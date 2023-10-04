using System.Buffers.Binary;
using System.Numerics;
using System.Security.Cryptography;

namespace ExtraRandom.PRNG;

/// <summary>
/// Xoroshiro128+ PRNG implementation.
/// <para>
/// Based on https://github.com/Shiroechi/Litdex.Random/blob/main/Source/PRNG/Xoroshiro128Plus.cs
/// </para>
/// </summary>
/// <remarks>
/// Source: https://prng.di.unimi.it/xoroshiro128plus.c
/// </remarks>
public class Xoroshiro128Plus : Random64
{
    private static readonly ulong[] JUMP = { 0xDF900294D8F554A5, 0x170865DF4B3201FC };

    public Xoroshiro128Plus(ulong seed1 = 0, ulong seed2 = 0)
    {
        State = new ulong[2];
        State[0] = seed1;
        State[1] = seed2;
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="Xoroshiro128Plus"/> class.
    /// </summary>
#pragma warning disable MA0055
    ~Xoroshiro128Plus()
#pragma warning restore MA0055
    {
        Array.Clear(State, 0, State.Length);
    }

    /// <inheritdoc />
    public override void Reseed()
    {
        using var rng = RandomNumberGenerator.Create();
        Span<byte> span = stackalloc byte[16];
        rng.GetNonZeroBytes(span);

        State[0] = BinaryPrimitives.ReadUInt64LittleEndian(span);
        State[1] = BinaryPrimitives.ReadUInt64LittleEndian(span.Slice(8));
    }

    /// <summary>
    /// 2^64 calls to NextLong(), it can be used to generate 2^64
    /// non-overlapping subsequences for parallel computations.
    /// </summary>
    public virtual void NextJump()
    {
        var seed1 = 0UL;
        var seed2 = 0UL;

        for (var i = 0; i < 2; i++)
        {
            for (var b = 0; b < 64; b++)
            {
                if ((JUMP[i] & (1UL << b)) != 0)
                {
                    seed1 ^= JUMP[0];
                    seed2 ^= JUMP[1];
                }

                NextInt64();
            }
        }

        State[0] = seed1;
        State[1] = seed2;
    }

    /// <inheritdoc />
    protected override ulong Next()
    {
        var s0 = State[0];
        var s1 = State[1];
        var result = State[0] + State[1];

        s1 ^= s0;
        State[0] = BitOperations.RotateLeft(s0, 24) ^ s1 ^ (s1 << 16);
        State[1] = BitOperations.RotateLeft(s1, 37);

        return result;
    }
}
