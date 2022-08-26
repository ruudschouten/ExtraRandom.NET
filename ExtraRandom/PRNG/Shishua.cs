namespace ExtraRandom.PRNG;

using System.Buffers.Binary;
using System.Security.Cryptography;

/// <summary>
/// Shift, Shuffle, Add PRNG.
/// <para>
/// More details about the original implementation of Shishua can be found on
/// <a href="https://github.com/espadrine/shishua">their GitHub page</a>.
/// </para>
/// </summary>
/// <remarks>
/// Experimental. Use with your own risk.
/// </remarks>
public class Shishua : BasePrng64
{
    private static readonly ulong[] Phi =
    {
        0x9E3779B97F4A7C15, 0xF39CC0605CEDC834, 0x1082276BF3A27251, 0xF86C6A11D0C18E95,
        0x2767F0B153D27B7F, 0x0347045B5BF1827F, 0x01886F0928403002, 0xC1D64BA40F335E36,
        0xF06AD7AE9717877E, 0x85839D6EFFBD7DC6, 0x64D325D1C5371682, 0xCADD0CCCFDFFBBE1,
        0x626E33B8D04B4331, 0xBBF73C790D94F79D, 0x471C4AB3ED3D82A5, 0xFEC507705E4AE6E5,
    };

    // Note: While it is an array, a "lane" refers to 4 consecutive ulong.
    // RNG state.
    private readonly ulong[] _output = new ulong[16]; // 4 lanes, 2 parts
    private readonly ulong[] _counter = new ulong[4]; // 1 lane

    // register the current index of output
    private byte _outputIndex = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="Shishua"/> class with a random seed.
    /// </summary>
    public Shishua()
        : this(new[]
        {
            (ulong)DateTime.Now.Ticks,
            (ulong)(DateTime.Now.Ticks + 1),
            (ulong)(DateTime.Now.Ticks + 2),
            (ulong)(DateTime.Now.Ticks + 3),
        })
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Shishua"/> class with the provided <paramref name="seed"/>.
    /// </summary>
    /// <param name="seed">
    /// RNG seed.
    /// </param>
    public Shishua(ulong[] seed)
    {
        this.State = new ulong[16]; // 4 lanes
        SetSeed(seed);
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="Shishua"/> class.
    /// </summary>
    ~Shishua()
    {
        Array.Clear(this.State, 0, this.State.Length);
        Array.Clear(_output, 0, _output.Length);
        Array.Clear(_counter, 0, _counter.Length);
    }

    /// <inheritdoc/>
    public override void Reseed()
    {
        using var rng = RandomNumberGenerator.Create();

#if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        Span<byte> span = new byte[64];
        rng.GetNonZeroBytes(span);
        SetSeed(
            BinaryPrimitives.ReadUInt64LittleEndian(span),
            BinaryPrimitives.ReadUInt64LittleEndian(span[8..]),
            BinaryPrimitives.ReadUInt64LittleEndian(span[16..]),
            BinaryPrimitives.ReadUInt64LittleEndian(span[24..]));
#else
		var bytes = new byte[64];
		rng.GetNonZeroBytes(bytes);
		this.SetSeed(
			BitConverter.ToUInt64(bytes, 0),
			BitConverter.ToUInt64(bytes, 8),
			BitConverter.ToUInt64(bytes, 16),
			BitConverter.ToUInt64(bytes, 24));
#endif
    }

    /// <inheritdoc/>
    public sealed override void SetSeed(params ulong[] seed)
    {
        if (seed.Length < 4)
        {
            throw new ArgumentException("Need at least 4 seeds.", nameof(seed));
        }

        Array.Copy(Phi, 0, this.State, 0, Phi.Length);

        for (var i = 0; i < 4; i++)
        {
            this.State[(i * 2) + 0] ^= seed[i]; // { s0, 0, s1, 0, s2, 0, s3, 0 }
            this.State[(i * 2) + 8] ^= seed[(i + 2) % 4]; // { s2, 0, s3, 0, s0, 0, s1, 0 }
        }

        for (var i = 0; i < 13; i++)
        {
            this.Mix(128);
            for (var j = 0; j < 4; j++)
            {
                this.State[j + 0] = this.State[j + 12];
                this.State[j + 4] = this.State[j + 8];
                this.State[j + 8] = this.State[j + 4];
                this.State[j + 12] = this.State[j + 0];
            }
        }
    }

    /// <inheritdoc/>
    public override ulong Next()
    {
        if (_outputIndex < _output.Length)
        {
            var result = _output[_outputIndex];
            _outputIndex++;
            return result;
        }

        Mix(128);
        _outputIndex = 1;
        return _output[0];
    }

    /// <summary>
    /// Mix the internal state.
    /// </summary>
    /// <param name="size">the size for the internal state.</param>
    private void Mix(int size)
    {
        for (var i = 0; i < size; i += 128)
        {
            for (var j = 0; j < 2; j++)
            {
                var stateCounter = j * 8;
                var outputCounter = j * 4;

                // Create a temporary buffer
                var buffer = new ulong[8];

                for (var k = 0; k < 4; k++)
                {
                    this.State[stateCounter + k + 4] += _counter[k];
                }

                var shuffleOffsets = new byte[]
                {
                    2, 3, 0, 1, 5, 6, 7, 4, // left
                    3, 0, 1, 2, 6, 7, 4, 5 // right
                };

                for (var k = 0; k < 8; k++)
                {
                    buffer[k] = (this.State[stateCounter + shuffleOffsets[k]] >> 32) |
                                (this.State[stateCounter + shuffleOffsets[k + 8]] << 32);
                }

                for (var k = 0; k < 4; k++)
                {
                    var low = this.State[stateCounter + k + 0] >> 1;
                    var high = this.State[stateCounter + k + 4] >> 3;

                    this.State[stateCounter + k + 0] = low + buffer[k + 0];
                    this.State[stateCounter + k + 4] = high + buffer[k + 4];

                    _output[outputCounter + k] = low ^ buffer[k + 4];
                }
            }

            // Merge together.
            for (var j = 0; j < 4; j++)
            {
                _output[j + 8] = this.State[j + 0] ^ this.State[j + 12];
                _output[j + 12] = this.State[j + 8] ^ this.State[j + 4];

                _counter[j] += (ulong)(7 - (j * 2)); // 7, 5, 3, 1
            }
        }
    }
}