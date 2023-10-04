using System.Buffers.Binary;
using System.Security.Cryptography;

namespace ExtraRandom.PRNG;

/// <summary>
/// Shift, Shuffle and Add PRNG.
/// Based on https://github.com/Shiroechi/Litdex.Random/blob/main/Source/PRNG/Shishua.cs
/// </summary>
/// <remarks>
/// Source: https://github.com/espadrine/shishua.
/// </remarks>
public sealed class Shishua : Random64
{
    internal static readonly ulong[] PHI =
    {
        0x9E3779B97F4A7C15,
        0xF39CC0605CEDC834,
        0x1082276BF3A27251,
        0xF86C6A11D0C18E95,
        0x2767F0B153D27B7F,
        0x0347045B5BF1827F,
        0x01886F0928403002,
        0xC1D64BA40F335E36,
        0xF06AD7AE9717877E,
        0x85839D6EFFBD7DC6,
        0x64D325D1C5371682,
        0xCADD0CCCFDFFBBE1,
        0x626E33B8D04B4331,
        0xBBF73C790D94F79D,
        0x471C4AB3ED3D82A5,
        0xFEC507705E4AE6E5,
    };

    private ulong[] _output = new ulong[16]; // 4 lanes, 2 parts
    private ulong[] _counter = new ulong[4]; // 1 lane

    private byte _outputIndex = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="Shishua"/> class.
    /// </summary>
    /// <param name="seed">Seed to use for the random number generation.</param>
    public Shishua(ulong[] seed)
    {
        State = new ulong[16];
        SetSeed(seed);
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="Shishua"/> class.
    /// </summary>
#pragma warning disable MA0055
    ~Shishua()
#pragma warning enable MA0055
    {
        Array.Clear(State, 0, State.Length);
        Array.Clear(_output, 0, _output.Length);
        Array.Clear(_counter, 0, _counter.Length);
    }

    /// <inheritdoc />
    public override void SetSeed(params ulong[] seed)
    {
        if (seed is not { Length: >= 4 })
        {
            throw new ArgumentException(
                "Seed can't be null and should have at least 4 entries.",
                nameof(seed)
            );
        }

        Array.Copy(PHI, 0, State, 0, PHI.Length);

        for (var i = 0; i < 4; i++)
        {
            State[(i * 2) + 0] ^= seed[i]; // { s0,0,s1,0,s2,0,s3,0 }
            State[(i * 2) + 8] ^= seed[(i + 2) % 4]; // { s2,0,s3,0,s0,0,s1,0 }
        }

        for (var i = 0; i < 13; i++)
        {
            Mix(128);
            for (var j = 0; j < 4; j++)
            {
                State[j + 0] = State[j + 12];
                State[j + 4] = State[j + 8];
                State[j + 8] = State[j + 4];
                State[j + 12] = State[j + 0];
            }
        }
    }

    /// <inheritdoc />
    public override void Reseed()
    {
        using var rng = RandomNumberGenerator.Create();
        Span<byte> span = stackalloc byte[64];
        rng.GetNonZeroBytes(span);
        SetSeed(
            BinaryPrimitives.ReadUInt64LittleEndian(span),
            BinaryPrimitives.ReadUInt64LittleEndian(span.Slice(8)),
            BinaryPrimitives.ReadUInt64LittleEndian(span.Slice(16)),
            BinaryPrimitives.ReadUInt64LittleEndian(span.Slice(24))
        );
    }

    /// <inheritdoc />
    protected override ulong Next()
    {
        if (_outputIndex < _output.Length)
        {
            var result = _output[_outputIndex];
            _outputIndex++;
            return result;
        }

        Mix(128);
        _outputIndex = 1; // Reset to 1 since the result on index 0 is returned in the next line.
        return _output[0];
    }

    /// <summary>
    /// Mix the internal state.
    /// </summary>
    /// <param name="size">Size.</param>
    private void Mix(int size)
    {
        for (var i = 0; i < size; i += 128)
        {
            for (var j = 0; j < 2; j++)
            {
                var stateCounter = j * 8;
                var outputCounter = j * 4;

                var tempBuffer = new ulong[8];

                for (var k = 0; k < 4; k++)
                {
                    State[stateCounter + k + 4] += _counter[k];
                }

                var shuffleOffets = new byte[] { 2, 3, 0, 1, 5, 6, 7, 4, 3, 0, 1, 2, 6, 7, 4, 5 };
                for (var k = 0; k < 8; k++)
                {
                    tempBuffer[k] =
                        State[stateCounter + shuffleOffets[k] >> 32]
                        | State[stateCounter + shuffleOffets[k + 8] << 32];
                }

                for (var k = 0; k < 4; k++)
                {
                    var low = State[stateCounter + k + 0] >> 1;
                    var high = State[stateCounter + k + 4] >> 3;

                    State[stateCounter + k + 0] = low + tempBuffer[k + 0];
                    State[stateCounter + k + 4] = high + tempBuffer[k + 4];

                    _output[outputCounter + k] = low ^ tempBuffer[k + 4];
                }
            }

            // Merge together
            for (var j = 0; j < 4; j++)
            {
                _output[j + 8] = State[j + 0] ^ State[j + 12];
                _output[j + 12] = State[j + 8] ^ State[j + 4];

                _counter[j] += (ulong)(7 - (j * 2)); // 7, 5, 3, 1
            }
        }
    }
}
