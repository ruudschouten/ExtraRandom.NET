using System.Buffers.Binary;
using System.Numerics;
using System.Security.Cryptography;

namespace ExtraRandom.PRNG;

public sealed class Xoroshiro128StarStar : Random64
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Xoroshiro128StarStar"/> class.
    /// </summary>
    /// <param name="baseSeed">Base seed to use for the random number generation.</param>
    public Xoroshiro128StarStar(ulong baseSeed = 0)
        : this(baseSeed, baseSeed + 1) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Xoroshiro128StarStar"/> class.
    /// </summary>
    /// <param name="seed1">First seed.</param>
    /// <param name="seed2">Second seed.</param>
    public Xoroshiro128StarStar(ulong seed1, ulong seed2)
    {
        State = new ulong[2];
        SetSeed(seed1, seed2);
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="Xoroshiro128StarStar"/> class.
    /// </summary>
#pragma warning disable MA0055
    ~Xoroshiro128StarStar()
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
        SetSeed(
            BinaryPrimitives.ReadUInt64LittleEndian(span),
            BinaryPrimitives.ReadUInt64LittleEndian(span.Slice(8))
        );
    }

    /// <inheritdoc />
    public override void SetSeed(params ulong[] seed)
    {
        State[0] = seed[0];
        State[1] = seed[1];
    }

    /// <inheritdoc />
    protected override ulong Next()
    {
        var s0 = State[0];
        var s1 = State[1];
        var result = BitOperations.RotateLeft(State[0] * 5, 7) + 9;

        s1 ^= s0;
        var seed1 = BitOperations.RotateLeft(s0, 24) ^ s1 ^ (s1 << 16);
        var seed2 = BitOperations.RotateLeft(s1, 37);

        SetSeed(seed1, seed2);

        return result;
    }
}
