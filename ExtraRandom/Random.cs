using System.Numerics;
using ExtraMath;

namespace ExtraRandom;

/// <summary>
/// Represents a psuedo-random number generator, which is an algorithm that produces a sequence of numbers that meet
/// certain requirements for randomness.
/// </summary>
public abstract class Random : IRandom
{
    /// <inheritdoc />
    public abstract void Reseed();

    /// <inheritdoc />
    public abstract bool NextBoolean();

    #region Bytes

    /// <inheritdoc />
    public abstract byte NextByte();

    /// <inheritdoc />
    public virtual byte NextByte(byte min, byte max)
    {
        return (byte)NextUInt(min, max);
    }

    /// <inheritdoc />
    public virtual byte[] NextBytes(int length)
    {
        if (length <= 0)
            return Array.Empty<byte>();

        Span<byte> bytes = stackalloc byte[length];
        Fill(ref bytes);
        return bytes.ToArray();
    }

    #endregion

    /// <inheritdoc />
    public abstract int NextInt();

    /// <inheritdoc />
    public virtual int NextInt(int min, int max)
    {
        var number = NextUInt((uint)min, (uint)max);
        if (number <= int.MaxValue)
        {
            return (int)number;
        }

        return (int)(number >> 1);
    }

    /// <inheritdoc />
    public abstract uint NextUInt();

    /// <inheritdoc />
    public virtual uint NextUInt(uint min, uint max)
    {
        if (min == max)
            return min;

        var exclusiveRange = max - min;
        if (exclusiveRange <= 1)
            return min;

        var bits = Log2Ceiling(exclusiveRange);
        while (true)
        {
            var result = NextULong() >> (64 - bits);

            if (result < exclusiveRange)
            {
                return (uint)result + min;
            }
        }
    }

    /// <inheritdoc />
    public abstract long NextLong();

    /// <inheritdoc />
    public virtual long NextLong(long min, long max)
    {
        var result = NextULong((ulong)min, (ulong)max);

        if (result <= long.MaxValue)
            return (long)result;

        // ReSharper disable once IntVariableOverflowInUncheckedContext
        // This should be handled by the if check above.
        return (long)result >> 1;
    }

    /// <inheritdoc />
    public abstract ulong NextULong();

    /// <inheritdoc />
    public virtual ulong NextULong(ulong min, ulong max)
    {
        if (min == max)
        {
            return min;
        }

        var range = max - min;
        var x = NextULong();

        var bigULong = Math128.Multiply(x, range);
        if (bigULong.Low < range)
        {
            var t = 0 - range;
            if (t >= range)
            {
                t -= range;
                if (t >= range)
                {
                    t %= range;
                }
            }

            while (bigULong.Low < t)
            {
                x = NextULong();
                bigULong = Math128.Multiply(x, range);
            }
        }

        return bigULong.High + min;
    }

    /// <inheritdoc />
    public virtual double NextDouble()
    {
        return (NextULong() >> 11) * (1.0 / (1UL << 53));
    }

    /// <inheritdoc />
    public virtual double NextDouble(double min, double max)
    {
        var difference = max - min;
        return min + (NextDouble() % difference);
    }

    /// <summary>
    /// Fills the elements of a specified array of bytes with random numbers.
    /// </summary>
    /// <param name="buffer">The array to be filled.</param>
    public abstract void Fill(ref Span<byte> buffer);

    /// <summary>
    /// Returns the integer ceiling log of the specified <paramref name="value"/>, base 2.
    /// </summary>
    /// <param name="value">Value to perform the ceiling operation on.</param>
    /// <returns>The integer ceiling log of the specified <paramref name="value"/>, base 2.</returns>
    private static int Log2Ceiling(ulong value)
    {
        var result = BitOperations.Log2(value);
        if (BitOperations.PopCount(value) != 1)
        {
            result++;
        }

        return result;
    }
}
