using System.Buffers.Binary;
using System.Numerics;
using System.Security.Cryptography;

namespace ExtraRandom.PRNG;

/// <summary>
/// Romu random variations, great for general purpose work, including huge jobs.
/// Est. capacity = 2^75 bytes. Register pressure = 6. State size = 192 bits.
/// Based on: https://github.com/Shiroechi/Litdex.Random/blob/main/Source/PRNG/RomuTrio.cs
/// </summary>
/// <remarks>
/// Source: https://www.romu-random.org/
/// </remarks>
public sealed class RomuTrio : Random64
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RomuTrio"/> class.
    /// </summary>
    /// <param name="baseSeed">Base seed to use for the random number generation.</param>
    /// <remarks>
    /// <paramref name="baseSeed"/> is used as the base for the seed, three additional <see cref="ulong"/> variables are made,
    /// which each increments the <paramref name="baseSeed"/> value by one.
    /// </remarks>
    public RomuTrio(ulong baseSeed)
        : this(new[] { baseSeed, baseSeed + 1, baseSeed + 2 }) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RomuTrio"/> class.
    /// </summary>
    /// <param name="seed"> RNG seed numbers.</param>
    public RomuTrio(ulong[] seed)
    {
        State = new ulong[3];
        SetSeed(seed);
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="RomuTrio"/> class.
    /// </summary>
#pragma warning disable MA0055
    ~RomuTrio()
#pragma warning restore MA0055
    {
        Array.Clear(State, 0, State.Length);
    }

    /// <inheritdoc />
    public override void Reseed()
    {
        using var rng = RandomNumberGenerator.Create();
        Span<byte> span = stackalloc byte[24];
        rng.GetNonZeroBytes(span);

        SetSeed(
            BinaryPrimitives.ReadUInt64LittleEndian(span),
            BinaryPrimitives.ReadUInt64LittleEndian(span.Slice(8)),
            BinaryPrimitives.ReadUInt64LittleEndian(span.Slice(16))
        );
    }

    /// <inheritdoc />
    public override void SetSeed(params ulong[] seed)
    {
        State[0] = seed[0];
        State[1] = seed[1];
        State[2] = seed[2];
    }

    /// <inheritdoc />
    protected override ulong Next()
    {
        var x = State[0];
        var y = State[1];
        var z = State[2];

        State[0] = 15241094284759029579u * z;
        State[1] = BitOperations.RotateLeft(y - x, 12);
        State[2] = BitOperations.RotateRight(z - y, 44);

        return x;
    }
}
