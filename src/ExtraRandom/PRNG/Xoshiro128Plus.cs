namespace ExtraRandom.PRNG;

/// <summary>
/// <para>Xoroshiro128+ PRNG implementation.</para>
/// <para>
/// The best and fastest 32-bit generator for 32-bit floating-point numbers.
/// We suggest to use its upper bits for floating-point generation, as it is slightly faster than <see cref="Xoshiro128StarStar">Xoshiro128**</see>.
/// </para>
/// <para>
/// The state must be seeded so that it is not everywhere zero.
/// </para>
/// </summary>
/// <remarks>
/// <para>Source: https://prng.di.unimi.it/xoshiro128plus.c</para>
/// </remarks>
public sealed class Xoshiro128Plus : Random32
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Xoshiro128Plus"/> class.
    /// </summary>
    /// <param name="baseSeed">Base seed to use for the random number generation.</param>
    public Xoshiro128Plus(ulong baseSeed = 0)
        : this(baseSeed, baseSeed + 1, baseSeed + 2, baseSeed + 3) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Xoshiro128Plus"/> class.
    /// </summary>
    /// <param name="seed1">First seed.</param>
    /// <param name="seed2">Second seed.</param>
    /// <param name="seed3">Third seed.</param>
    /// <param name="seed4">Fourth seed.</param>
    public Xoshiro128Plus(ulong seed1, ulong seed2, ulong seed3, ulong seed4)
    {
        State = new ulong[4];
        SetSeed(seed1, seed2, seed3, seed4);
    }

    /// <inheritdoc />
    protected override uint Next()
    {
        var result = (uint)State[0] + (uint)State[3];
        var t = State[1] << 9;

        State[2] ^= State[0];
        State[3] ^= State[1];
        State[1] ^= State[2];
        State[0] ^= State[3];

        State[2] ^= t;
        State[3] = RotL((uint)State[3], 11);

        return result;
    }

    private static uint RotL(uint x, int k) => (x << k) | (x >> (32 - k));
}
