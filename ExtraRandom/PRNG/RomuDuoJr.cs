using System.Buffers.Binary;
using System.Numerics;
using System.Security.Cryptography;

namespace ExtraRandom.PRNG;

/// <summary>
/// Romu random variations, the fastest generator using 64-bit arithmetic, but not suited for huge jobs.
/// Based on: https://github.com/Shiroechi/Litdex.Random/blob/main/Source/PRNG/RomuDuoJr.cs
/// </summary>
/// <remarks>
/// Source: https://arxiv.org/pdf/2002.11331.pdf
/// </remarks>
public sealed class RomuDuoJr : Random64
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RomuDuoJr"/> class.
    /// </summary>
    /// <param name="baseSeed">Base seed to use for the random number generation.</param>
    /// <remarks>
    /// <paramref name="baseSeed"/> is used as the base for the seed, three additional <see cref="ulong"/> variables are made,
    /// which each increments the <paramref name="baseSeed"/> value by one.
    /// </remarks>
    public RomuDuoJr(ulong baseSeed)
        : this(new[] { baseSeed, baseSeed + 1 }) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RomuDuoJr"/> class.
    /// </summary>
    /// <param name="seed"> RNG seed numbers.</param>
    public RomuDuoJr(ulong[] seed)
    {
        State = new ulong[2];
        SetSeed(seed);
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="RomuDuoJr"/> class.
    /// </summary>
#pragma warning disable MA0055
    ~RomuDuoJr()
#pragma warning restore MA0055
    {
        Array.Clear(State, 0, State.Length);
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    protected override ulong Next()
    {
        var x = State[0];
        var y = State[1];
        State[0] = 15241094284759029579u * y;
        State[1] = BitOperations.RotateLeft(y - x, 27);
        return x;
    }
}
