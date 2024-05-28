namespace ExtraRandom.PRNG;

/// <summary>
/// <para>Xoroshiro128** PRNG implementation.</para>
/// <para>
/// 32 bit all-purpose, rock-solid generators.
/// It excellent speed, a state size (128 bits) that is large enough for mild parallelism.
/// </para>
/// <para>For generating just floating-point numbers,<see cref="Xoroshiro128Plus">Xoroshiro128+</see> is even faster</para>
/// </summary>
/// <remarks>
/// <para>Source: https://prng.di.unimi.it/xoshiro128starstar.c</para>
/// </remarks>
public sealed class Xoshiro128StarStar : Random32
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Xoroshiro128StarStar"/> class.
    /// </summary>
    /// <param name="baseSeed">Base seed to use for the random number generation.</param>
    public Xoshiro128StarStar(ulong baseSeed = 0)
        : this(baseSeed, baseSeed + 1, baseSeed + 2, baseSeed + 3) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Xoroshiro128StarStar"/> class.
    /// </summary>
    /// <param name="seed1">First seed.</param>
    /// <param name="seed2">Second seed.</param>
    /// <param name="seed3">Third seed.</param>
    /// <param name="seed4">Fourth seed.</param>
    public Xoshiro128StarStar(ulong seed1, ulong seed2, ulong seed3, ulong seed4)
    {
        State = new ulong[4];
        SetSeed(seed1, seed2, seed3, seed4);
    }

    /// <inheritdoc />
    protected override uint Next()
    {
        var result = RotL((uint)(State[1] * 5), 7) * 9;
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
