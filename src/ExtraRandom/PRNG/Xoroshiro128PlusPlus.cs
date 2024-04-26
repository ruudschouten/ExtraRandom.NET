using System.Buffers.Binary;
using System.Numerics;
using System.Security.Cryptography;

namespace ExtraRandom.PRNG;

/// <summary>
/// Xoroshiro128++ PRNG implementation.
/// </summary>
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
    private Xoroshiro128PlusPlus(ulong seed1, ulong seed2)
    {
        State = new ulong[2];
        SetSeed(seed1, seed2);
    }

    /// <inheritdoc />
    public override void Reseed()
    {
        using var rng = RandomNumberGenerator.Create();
        Span<byte> span = stackalloc byte[16];
        rng.GetNonZeroBytes(span);
        SetSeed(
            BinaryPrimitives.ReadUInt64LittleEndian(span),
            BinaryPrimitives.ReadUInt64LittleEndian(span[8..])
        );
    }

    /// <inheritdoc />
    protected override ulong Next()
    {
        var s0 = State[0];
        var s1 = State[1];
        var result = BitOperations.RotateLeft(State[0] + State[1], 17) + s0;

        s1 ^= s0;
        var seed1 = BitOperations.RotateLeft(s0, 49) ^ s1 ^ (s1 << 21);
        var seed2 = BitOperations.RotateLeft(s1, 28);

        SetSeed(seed1, seed2);

        return result;
    }
}
