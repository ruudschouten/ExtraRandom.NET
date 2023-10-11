using System.Buffers.Binary;
using System.Numerics;
using System.Security.Cryptography;

namespace ExtraRandom.PRNG;

/// <summary>
/// LFSR-based pseudorandom number generators.
/// Based on: https://github.com/Shiroechi/Litdex.Random/blob/main/Source/PRNG/Seiran.cs
/// </summary>
/// <remarks>
/// Source: https://github.com/andanteyk/prng-seiran
/// </remarks>
public sealed class Seiran : Random64
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Seiran"/> class.
    /// </summary>
    /// <param name="baseSeed">Base seed to use for the random number generation.</param>
    /// <remarks>
    /// <paramref name="baseSeed"/> is used as the base for the seed, three additional <see cref="ulong"/> variables are made,
    /// which each increments the <paramref name="baseSeed"/> value by one.
    /// </remarks>
    public Seiran(ulong baseSeed)
        : this(new[] { baseSeed, baseSeed + 1 }) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Seiran"/> class.
    /// </summary>
    /// <param name="seed">Seed to use for the random number generation.</param>
    public Seiran(ulong[] seed)
    {
        State = new ulong[2];
        SetSeed(seed);
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="Seiran"/> class.
    /// </summary>
#pragma warning disable MA0055
    ~Seiran()
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

        var result = BitOperations.RotateLeft((s0 + s1) * 9, 29) + s0;

        State[0] = s0 ^ BitOperations.RotateLeft(s1, 29);
        State[1] = s0 ^ s1 << 9;

        return result;
    }
}
