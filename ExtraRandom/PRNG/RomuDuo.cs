using System.Buffers.Binary;
using System.Numerics;
using System.Security.Cryptography;

namespace ExtraRandom.PRNG;

/// <summary>
/// Romu random variations, might be faster than <see cref="RomuTrio"/> due to using fewer registers, but might struggle with massive jobs.
/// Est. capacity = 2^61 bytes. Register pressure = 5. State size = 128 bits.
/// Based on: https://github.com/Shiroechi/Litdex.Random/blob/main/Source/PRNG/RomuDuo.cs
/// </summary>
/// <remarks>
/// Source: https://www.romu-random.org/
/// </remarks>
public sealed class RomuDuo : Random64
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RomuDuo"/> class.
    /// </summary>
    /// <param name="baseSeed">Base seed to use for the random number generation.</param>
    /// <remarks>
    /// <paramref name="baseSeed"/> is used as the base for the seed, three additional <see cref="ulong"/> variables are made,
    /// which each increments the <paramref name="baseSeed"/> value by one.
    /// </remarks>
    public RomuDuo(ulong baseSeed)
        : this(new[] { baseSeed, baseSeed + 1 }) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RomuDuo"/> class.
    /// </summary>
    /// <param name="seed"> RNG seed numbers.</param>
    public RomuDuo(ulong[] seed)
    {
        State = new ulong[2];
        SetSeed(seed);
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="RomuDuo"/> class.
    /// </summary>
#pragma warning disable MA0055
    ~RomuDuo()
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
        var xp = State[0];
        State[0] = 15241094284759029579u * State[1];
        State[1] =
            BitOperations.RotateLeft(State[1], 27) + BitOperations.RotateLeft(State[1], 15) - xp;
        return xp;
    }
}
