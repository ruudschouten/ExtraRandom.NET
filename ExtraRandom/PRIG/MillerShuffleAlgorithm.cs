using System.Diagnostics.CodeAnalysis;
using ExtraRandom.PRNG;

namespace ExtraRandom.PRIG;

/// <summary>
/// <para>
/// The Miller Shuffle Algorithm is a pseudo-random index generator that produces a shuffled index.
/// There are some <see cref="MillerShuffleVariant">variants</see> to this algorithm, each suited for different sizes of items.
/// </para>
/// <para>Source: https://github.com/RondeSC/Miller_Shuffle_Algo</para>
/// </summary>
public static class MillerShuffleAlgorithm
{
    /// <summary>
    /// <para>Random number generator used by the Miller Shuffle B variant.</para>
    /// <para>Default is <see cref="RomuTrio"/> with a seed of 500.</para>
    /// </summary>
    /// <remarks><para>This is <i>only</i> used with <see cref="MillerShuffleB"/>.</para></remarks>
    public static IRandom Random { get; set; } = new RomuTrio(500);

    /// <summary>
    /// <para>Produces a shuffled Index given a base Index, a random seed and the length of the list being indexed.</para>
    /// <para>based on the <paramref name="variant"/> you can get different performance and randomness.</para>
    /// </summary>
    public static double MillerShuffle(
        int index,
        int seed,
        int itemCount,
        MillerShuffleVariant variant = MillerShuffleVariant.MS_XLite
    )
    {
        return variant switch
        {
            MillerShuffleVariant.MS_Lite => MillerShuffleLite((short)index, seed, (short)itemCount),
            MillerShuffleVariant.MS_XLite
                => MillerShuffleXLite((short)index, seed, (short)itemCount),
            MillerShuffleVariant.MSA_d => MillerShuffleD((uint)index, (uint)seed, (uint)itemCount),
            MillerShuffleVariant.MSA_e => MillerShuffleE((uint)index, (uint)seed, (uint)itemCount),
            MillerShuffleVariant.MSA_b => MillerShuffleB((uint)index, (uint)seed, (uint)itemCount),
            _ => throw new ArgumentOutOfRangeException(nameof(variant), variant, message: null)
        };
    }

    /// <summary>
    /// Produces a shuffled Index given a base Index, a random seed and the length of the list being indexed
    /// for each <paramref name="index"/>: 0 to <paramref name="itemCount"/>-1, unique indexes are returned in a pseudo "random" order.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The item count must be smaller than 9949, otherwise the modulo operations with primes will not be able to return a correct random index.
    /// </para>
    /// </remarks>
    public static double MillerShuffleXLite(short index, int seed, short itemCount)
    {
        const short prime1 = 3343;
        const short prime2 = 9949;

        seed += (131 * (index / itemCount));
        var si = ((index + seed) % itemCount);
        var r1 = (seed % prime2);
        var r2 = (seed % prime1);
        var rx = ((seed / itemCount % itemCount) + 1);

        if (si % 3 == 0)
            si = (((si / 3 * prime2) + r1) % ((itemCount + 2) / 3) * 3);
        if (si % 2 == 0)
            si = ((((si / 2) * prime1) + r2) % ((itemCount + 1) / 2) * 2);
        if (si < rx)
            si = (((si * prime2) + r1) % rx);
        else
            si = (((((si - rx) * prime1) + r2) % (itemCount - rx)) + rx);

        return si;
    }

    /// <summary>
    /// Produces a shuffled Index given a base Index, a random seed and the length of the list being indexed
    /// for each <paramref name="index"/>: 0 to <paramref name="itemCount"/>-1, unique indexes are returned in a pseudo "random" order.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The item count must be smaller than 9973, otherwise the modulo operations with primes will not be able to return a correct random index.
    /// </para>
    /// </remarks>
    public static double MillerShuffleLite(short index, int seed, short itemCount)
    {
        const int prime1 = 3343;
        const int prime2 = 9949;
        const int prime3 = 9973;

        seed += 131 * (index / itemCount);
        var si = (index + seed) % itemCount;
        var r1 = seed % prime3;
        var r2 = seed % prime1;
        var r3 = seed % prime2;
        var rx = (seed / itemCount % itemCount) + 1;

        if (si % 3 == 0)
            si = ((((si / 3) * prime1) + r1) % ((itemCount + 2) / 3)) * 3;
        if (si % 2 == 0)
            si = ((((si / 2) * prime2) + r2) % ((itemCount + 1) / 2)) * 2;
        if (si < rx)
            si = (((si * prime3) + r2) % rx);
        else
            si = (((((si - rx) * prime2) + r1) % (itemCount - rx)) + rx);

        return ((si * prime3) + r3) % itemCount;
    }

    /// <summary>
    /// Produces a shuffled Index given a base Index, a random seed and the length of the list being indexed
    /// </summary>
    public static double MillerShuffleD(uint index, uint seed, uint itemCount)
    {
        const uint prime1 = 24317;
        const uint prime2 = 32141;
        const uint prime3 = 63629;

        seed += 131 * (index / itemCount);
        var si = (index + seed) % itemCount;
        var r1 = (seed % prime1) + 42;
        var r2 = ((seed * 0x89) ^ r1) % prime2;
        var r3 = (r1 + r2 + prime3) % itemCount;
        var r4 = r1 ^ r2 ^ r3;
        var rx = ((seed / itemCount) % itemCount) + 1;
        var rx2 = ((seed / itemCount / itemCount) % itemCount) + 1;

        if (si % 3 == 0)
            si = ((((si / 3) * prime1) + r1) % ((itemCount + 2) / 3)) * 3;
        if (si % 2 == 0)
            si = ((((si / 2) * prime2) + r2) % ((itemCount + 1) / 2)) * 2;
        if (si < itemCount / 2)
            si = (((si * prime3) + r4) % (itemCount / 2));
        if ((si ^ rx) < itemCount)
            si ^= rx;

        si = (((si * prime3) + r3) % itemCount);

        if ((si ^ rx2) < itemCount)
            si ^= rx2;

        return si;
    }

    /// <summary>
    /// Produces a shuffled Index given a base Index, a random seed and the length of the list being indexed
    /// </summary>
    public static double MillerShuffleE(uint index, uint seed, uint itemCount)
    {
        const uint prime1 = 24317;
        const uint prime2 = 32141;
        const uint prime3 = 63629;

        seed += 131 * (index / itemCount);
        var si = (index + seed) % itemCount;

        var r1 = seed % prime3;
        var r2 = seed % prime1;
        var r3 = seed % prime2;
        var r4 = seed % 2749;
        var halfN = (uint)Math.Floor(((double)itemCount / 2) + 1);
        var rx = (uint)((Math.Floor((double)seed / itemCount) % itemCount) + 1);
        var rkey = (uint)((Math.Floor((double)seed / itemCount / itemCount) % itemCount) + 1);

        if (si % 3 == 0)
            si = ((((si / 3) * prime1) + r1) % ((itemCount + 2) / 3)) * 3;
        if (si <= halfN)
        {
            si = (si + r3) % (halfN + 1);
            si = (uint)((int)halfN - si);
        }

        if (si % 2 == 0)
            si = ((((si / 2) * prime2) + r2) % ((itemCount + 1) / 2)) * 2;
        if (si < halfN)
            si = (((si * prime3) + r4) % halfN);

        if ((si ^ rx) < itemCount) si ^= rx;

        si = (((si * prime3) + r3) % itemCount);

        if ((si ^ rkey) < itemCount) si ^= rkey;

        return si;
    }

    private static uint _optionalIndex = 0;

    /// <summary>
    /// Produces a shuffled Index given a base Index, a random seed and the length of the list being indexed, while using a <see cref="Random"/> instance.
    /// </summary>
    public static double MillerShuffleB(uint index, uint seed, uint itemCount)
    {
        if ((index % itemCount) == 0) _optionalIndex = (uint)MillerShuffleD(index + itemCount - 1, seed, itemCount);
        var si = (uint)MillerShuffleD(index, seed, itemCount);

        if ((index % itemCount) == (itemCount - 1))
        {
            si = _optionalIndex;
        }
        else if ((uint)Math.Floor(Random.NextDouble(0, 1) * 3) == 1)
        {
            var xi = si;
            si = (_optionalIndex % itemCount);
            _optionalIndex = xi;
        }

        return si;
    }
}