namespace ExtraRandom.PRNG;

/// <summary>
/// <para>Xoroshiro64** PRNG implementation.
/// 32-bit all-purpose, rock-solid, small-state generator.
/// It is extremely fast and passes all tests we are aware of, but its state space is not large enough for any parallel application.
/// For generating just single-precision (i.e., 32-bit) floating-point numbers, <see cref="Xoroshiro64Star">Xoroshiro64*</see> is even faster.</para>
/// <para>The state must be seeded so that it is not everywhere zero.</para>
/// </summary>
/// <remarks>
/// <para>Source: https://prng.di.unimi.it/xoroshiro64starstar.c</para>
/// </remarks>
public sealed class Xoroshiro64StarStar : Random32
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Xoroshiro64StarStar"/> class.
    /// </summary>
    /// <param name="baseSeed">Base seed to use for the random number generation.</param>
    public Xoroshiro64StarStar(ulong baseSeed = 0)
        : this(baseSeed, baseSeed + 1) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Xoroshiro64StarStar"/> class.
    /// </summary>
    /// <param name="seed1">First seed.</param>
    /// <param name="seed2">Second seed.</param>
    public Xoroshiro64StarStar(ulong seed1, ulong seed2)
    {
        State = new ulong[2];
        SetSeed(seed1, seed2);
    }

    /// <inheritdoc />
    protected override uint Next()
    {
        var s0 = (uint)State[0];
        var s1 = (uint)State[1];
        var result = RotL(s0 * 0x9E3779BB, 5) * 5;

        State[0] = RotL(s0, 26) ^ s1 ^ (s1 << 9);
        State[1] = RotL(s1, 13);
        return result;
    }

    private static uint RotL(uint x, int k) => (x << k) | (x >> (32 - k));
}
