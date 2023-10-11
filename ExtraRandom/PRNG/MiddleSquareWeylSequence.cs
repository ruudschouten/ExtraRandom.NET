using System.Security.Cryptography;

namespace ExtraRandom.PRNG;

/// <summary>
/// Improved version from Middle Square Method, invented by John Von Neumann.
/// </summary>
/// <remarks>
/// Source: https://arxiv.org/abs/1704.00358
/// </remarks>
public sealed class MiddleSquareWeylSequence : Random32
{
    // Random output
    private ulong _output = 0;

    // Weyl sequence
    private ulong _sequence = 0;

    // Odd constant
    private ulong _increment = 0xB5AD4ECEDA1CE2A9;

    /// <summary>
    /// Initializes a new instance of the <see cref="MiddleSquareWeylSequence"/> class.
    /// </summary>
    /// <param name="seed"> RNG seed numbers.</param>
    public MiddleSquareWeylSequence(ulong seed)
    {
        State = new ulong[2];
        SetSeed(seed);
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="MiddleSquareWeylSequence"/> class.
    /// </summary>
#pragma warning disable MA0055
    ~MiddleSquareWeylSequence()
#pragma warning restore MA0055
    {
        _output = 0;
        _sequence = 0;
        _increment = 0;
    }

    /// <inheritdoc />
    public override void Reseed()
    {
        using var rng = RandomNumberGenerator.Create();
        Span<byte> span = new byte[16];
        rng.GetNonZeroBytes(span);
        _sequence = System.Buffers.Binary.BinaryPrimitives.ReadUInt64LittleEndian(span);
        _output = System.Buffers.Binary.BinaryPrimitives.ReadUInt64LittleEndian(span.Slice(8));
    }

    /// <inheritdoc />
    public override void SetSeed(params ulong[] seed)
    {
        _output = seed[0];
        _sequence = seed[0];
    }

    /// <inheritdoc />
    protected override uint Next()
    {
        _output *= _output;
        _output += _sequence + _increment;
        _output = (_output >> 32) | (_output << 32);
        return (uint)_output;
    }
}
