namespace ExtraRandom.PRNG;

/// <summary>
/// <para>Xoroshiro64* PRNG implementation.
/// The best and fastest 32-bit Xoroshiro small-state generator for 32-bit floating-point numbers.
/// It is suggested to use its upper bits for floating-point generation, as it is slightly faster than <see cref="Xoroshiro64StarStar">Xoroshiro64**</see>.
/// It passes all tests we are aware of except for linearity tests,
/// as the lowest six bits have low linear complexity,
/// so if low linear complexity is not considered an issue (as it is usually the case) it can be used to generate 32-bit outputs, too.</para>
/// <para> We suggest to use a sign test to extract a random Boolean value, and right shifts to extract subsets of bits.</para>
/// <para>The state must be seeded so that it is not everywhere zero.</para>
/// </summary>
/// <remarks>
/// <para>Source: https://prng.di.unimi.it/xoroshiro64star.c</para>
/// </remarks>
public sealed class Xoroshiro64Star : Random32
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Xoroshiro64Star"/> class.
    /// </summary>
    /// <param name="baseSeed">Base seed to use for the random number generation.</param>
    public Xoroshiro64Star(ulong baseSeed = 0)
        : this(baseSeed, baseSeed + 1) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Xoroshiro64Star"/> class.
    /// </summary>
    /// <param name="seed1">First seed.</param>
    /// <param name="seed2">Second seed.</param>
    public Xoroshiro64Star(ulong seed1, ulong seed2)
    {
        State = new ulong[2];
        SetSeed(seed1, seed2);
    }

    /// <inheritdoc />
    protected override uint Next()
    {
        var s0 = (uint)State[0];
        var s1 = (uint)State[1];
        var result = s0 * 0x9E3779BB;

        s1 ^= s0;
        State[0] = RotL(s0, 26) ^ s1 ^ (s1 << 9);
        State[1] = RotL(s1, 13);
        return result;
    }

    private static uint RotL(uint x, int k) => (x << k) | (x >> (32 - k));
}
