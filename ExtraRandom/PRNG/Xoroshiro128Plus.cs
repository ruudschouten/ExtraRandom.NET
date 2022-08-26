namespace ExtraRandom.PRNG;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

/// <summary>
/// Implementation of http://prng.di.unimi.it/xoroshiro128plus.c
/// One of the fastest prng's for 32bit/64bit floating points.
/// <para>
/// Code is heavily inspired by
/// <a href="https://github.com/martinothamar/Xoroshiro128Plus/blob/master/src/Xoroshiro128Plus/Xoroshiro128Plus.cs">this Xoroshiro128Plus repo</a>.
/// </para>
/// <para>
/// But it didn't have a NuGet package, so I just copied it over and made some small adjustments.
/// </para>
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(
    "Security",
    "SCS0005:Weak random number generator.",
    Justification = "The weak rng is used so generate stronger rng.")]
public struct Xoroshiro128Plus
{
    private const ulong DoubleMask = (1L << 53) - 1;
    private const double Norm53 = 1.0d / (1L << 53);
    private const ulong FloatMask = (1L << 24) - 1;
    private const float Norm24 = 1.0f / (1L << 24);

    private const int A = 24;
    private const int B = 16;
    private const int C = 37;

    private ulong state0;
    private ulong state1;

    /// <summary>
    /// Initializes a new instance of the <see cref="Xoroshiro128Plus"/> struct.
    /// </summary>
    /// <param name="random">The default <see cref="Random"/> to use.</param>
    public Xoroshiro128Plus(Random? random = null)
        : this()
    {
        if (random is { })
        {
            Reseed(random);
        }
        else
        {
            Unsafe.SkipInit(out ulong rng1);
            Unsafe.SkipInit(out uint rng2);
            Unsafe.SkipInit(out ulong rng3);
            Unsafe.SkipInit(out uint rng4);

            state0 = rng1 << 32 | rng2;
            state1 = rng3 << 32 | rng4;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Xoroshiro128Plus"/> struct with the given <paramref name="seed"/>.
    /// </summary>
    /// <param name="seed">The seed to use for random number generation.</param>
    public Xoroshiro128Plus(int seed)
        : this(new Random(seed))
    {
    }

    /// <summary>
    /// Reseed the psuedo-random number generation, with the provided <paramref name="seed"/>.
    /// </summary>
    /// <param name="seed">new seed to use for generation.</param>
    public void Reseed(int seed)
    {
        Reseed(new Random(seed));
    }

    /// <summary>
    /// Generate a random <see cref="double"/> value between 0.0 and 1.0.
    /// </summary>
    /// <returns>The generated number.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public double NextDouble()
    {
        return (NextInternal() & DoubleMask) * Norm53;
    }

    /// <summary>
    /// Generate a random <see cref="float"/> value between 0f and 1f.
    /// </summary>
    /// <returns>The generated number.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public float NextFloat()
    {
        return (NextInternal() & FloatMask) * Norm24;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static ulong RotateLeft(ulong x, int k)
    {
        return (x << k) | (x >> (64 - k));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private ulong NextInternal()
    {
        var s0 = state0;
        var s1 = state1 ^ s0;

        state0 = RotateLeft(s0, A) ^ s1 ^ s1 << B;
        state1 = RotateLeft(s1, C);

        return state0 + state1;
    }

    /// <summary>
    /// Reseed the psuedo-random number generation.
    /// </summary>
    /// <param name="random">The generator to calculate the states for.</param>
    private void Reseed(Random random)
    {
        const int min = int.MinValue;
        const int max = int.MaxValue;
        state0 = (ulong)random.Next(min, max) << 32 | (uint)random.Next(min, max);
        state1 = (ulong)random.Next(min, max) << 32 | (uint)random.Next(min, max);
    }
}