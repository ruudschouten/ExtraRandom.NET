using System.Numerics;
using ExtraMath;
using ExtraRandom.Util;

namespace ExtraRandom;

/// <summary>
/// Represents a psuedo-random number generator, which is an algorithm that produces a sequence of numbers that meet
/// certain requirements for randomness.
/// </summary>
public abstract class Random
{
    private const byte Zero = 0;

    /// <summary>
    /// Minimum integer number.
    /// </summary>
    /// <remarks>Is set to <c>0</c> if <see cref="UseZeroAsMinimumValue"/> is <c>true</c>, otherwise is <see cref="int.MinValue"/>.</remarks>
    private int _minIntValue = int.MinValue;

    /// <summary>
    /// Minimum long number.
    /// </summary>
    /// <remarks>Is set to <c>0</c> if <see cref="UseZeroAsMinimumValue"/> is <c>true</c>, otherwise is <see cref="long.MinValue"/>.</remarks>
    private long _minLongValue = long.MinValue;

    private bool _useZeroAsMinimumValue = true;

    /// <summary>
    /// Gets or sets a value indicating whether the default minimum value is 0, or the minimum value of that specific type.
    /// </summary>
    public bool UseZeroAsMinimumValue
    {
        get => _useZeroAsMinimumValue;
        set
        {
            _useZeroAsMinimumValue = value;
            if (_useZeroAsMinimumValue)
            {
                _minIntValue = Zero;
                _minLongValue = Zero;
            }
            else
            {
                _minIntValue = int.MinValue;
                _minLongValue = long.MinValue;
            }
        }
    }

    /// <summary>
    /// Reseed the RNG.
    /// </summary>
    public abstract void Reseed();

    /// <summary>
    /// Fills the elements of a specified array of bytes with random numbers.
    /// </summary>
    /// <param name="buffer">The array to be filled.</param>
    // TODO: Check if this can be a ref.
    public abstract void Fill(ref byte[] buffer);

    /// <summary>
    /// Fills the elements of a specified array of bytes with random numbers.
    /// </summary>
    /// <param name="buffer">The array to be filled.</param>
    // TODO: Check if this can be a ref.
    public abstract void Fill(ref Span<byte> buffer);

    /// <summary>
    /// Generate next random number.
    /// </summary>
    /// <returns>A 64-bit unsigned integer.</returns>
    public abstract ulong Next();

    /// <summary>
    /// Generate <see cref="T:System.Boolean" /> value.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> or <see langword="false" />.
    /// </returns>
    public abstract bool NextBoolean();

    #region Bytes

    /// <summary>
    /// Generate a non-negative random integer.
    /// </summary>
    /// <returns>A 8-bit unsigned integer.</returns>
    /// <remarks>
    /// Generated value is between <see cref="byte.MinValue">0</see> and <see cref="byte.MaxValue">255</see> of a <see cref="byte"/>.
    /// </remarks>
    public virtual byte NextByte()
    {
        return NextByte(byte.MinValue, byte.MaxValue);
    }

    /// <summary>
    /// Generate a <see cref="byte"/> with a maximum value of <paramref name="max"/>.
    /// </summary>
    /// <param name="max">Exclusive upper bound.</param>
    /// <returns>A 8-bit unsigned integer.</returns>
    /// <remarks>
    /// Generated value is between <see cref="byte.MinValue">0</see> and the provided <paramref name="max"/> value.
    /// </remarks>
    public virtual byte NextByte(byte max)
    {
        return NextByte(byte.MinValue, max);
    }

    /// <summary>
    /// Generate a <see cref="byte"/> within the specified <paramref name="min"/> and <paramref name="max"/> range.
    /// </summary>
    /// <param name="min">Inclusive lower bound.</param>
    /// <param name="max">Exclusive upper bound.</param>
    /// <returns>A 8-bit integer between the <paramref name="min"/> and <paramref name="max"/> values.</returns>
    public virtual byte NextByte(byte min, byte max)
    {
        return (byte)NextUInt(min, max);
    }

    /// <summary>
    /// Generate an array of random bytes.
    /// </summary>
    /// <param name="length">Amount of bytes to generate.</param>
    /// <returns>An array of random bytes.</returns>
    public virtual byte[] NextBytes(int length)
    {
        var bytes = new byte[length];
        for (var i = 0; i < length; i++)
        {
            bytes[i] = NextByte();
        }

        return bytes;
    }

    #endregion

    /// <summary>
    /// Generate a <see cref="int"/>.
    /// </summary>
    /// <returns>A 32-bit signed integer.</returns>
    /// <remarks>
    /// Generated value is between <see cref="_minIntValue"/> and <see cref="int.MaxValue"/> value.
    /// </remarks>
    public virtual int NextInt()
    {
        return NextInt(_minIntValue, int.MaxValue);
    }

    /// <summary>
    /// Generate a <see cref="int"/>.
    /// </summary>
    /// <param name="max">Exclusive upper bound.</param>
    /// <returns>A 32-bit signed integer.</returns>
    /// <remarks>
    /// Generated value is between <see cref="_minIntValue"/> and the provided <paramref name="max"/> value.
    /// </remarks>
    public virtual int NextInt(int max)
    {
        return NextInt(_minIntValue, max);
    }

    /// <summary>
    /// Generate a <see cref="int"/>.
    /// </summary>
    /// <param name="min">Inclusive lower bound.</param>
    /// <param name="max">Exclusive upper bound.</param>
    /// <returns>A 32-bit signed integer between the <paramref name="min"/> and <paramref name="max"/> values.</returns>
    /// <remarks>
    /// Generated value is between <see cref="_minIntValue"/> and the provided <paramref name="max"/> value.
    /// </remarks>
    public virtual int NextInt(int min, int max)
    {
        return (int)(NextUInt((uint)min, (uint)max) >> 1);
    }

    /// <summary>
    /// Generate a <see cref="uint"/>.
    /// </summary>
    /// <returns>A 32-bit unsigned integer.</returns>
    public virtual uint NextUInt()
    {
        return NextUInt(uint.MinValue, uint.MaxValue);
    }

    /// <summary>
    /// Generate a <see cref="uint"/>.
    /// </summary>
    /// <param name="max">Exclusive upper bound.</param>
    /// <returns>A 32-bit unsigned integer.</returns>
    public virtual uint NextUInt(uint max)
    {
        return NextUInt(uint.MinValue, max);
    }

    /// <summary>
    /// Generate a <see cref="uint"/>.
    /// </summary>
    /// <param name="min">Inclusive lower bound.</param>
    /// <param name="max">Exclusive upper bound.</param>
    /// <returns>A 32-bit unsigned integer between the <paramref name="min"/> and <paramref name="max"/> values.</returns>
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

    /// <summary>
    /// Generate a <see cref="long"/>.
    /// </summary>
    /// <returns>A 64-bit signed integer.</returns>
    /// <remarks>
    /// Generated value is between <see cref="_minLongValue"/> and <see cref="long.MaxValue"/>.
    /// </remarks>
    public virtual long NextLong()
    {
        return NextLong(_minLongValue, long.MaxValue);
    }

    /// <summary>
    /// Generate a <see cref="uint"/>.
    /// </summary>
    /// <param name="max">Exclusive upper bound.</param>
    /// <returns>A 32-bit unsigned integer.</returns>
    /// <remarks>
    /// Generated value is between <see cref="_minIntValue"/> and the provided <paramref name="max"/> value.
    /// </remarks>
    public virtual long NextLong(long max)
    {
        return NextLong(_minLongValue, max);
    }

    /// <summary>
    /// Generate a <see cref="uint"/>.
    /// </summary>
    /// <param name="min">Inclusive lower bound.</param>
    /// <param name="max">Exclusive upper bound.</param>
    /// <returns>A 64-bit unsigned integer between the <paramref name="min"/> and <paramref name="max"/> values.</returns>
    public virtual long NextLong(long min, long max)
    {
        var result = NextULong((ulong)min, (ulong)max);

        if (result <= long.MaxValue)
            return (long)result;

        return (long)result >> 1;
    }

    /// <summary>
    /// Generate a <see cref="ulong"/>.
    /// </summary>
    /// <returns>A 64-bit unsigned integer.</returns>
    public virtual ulong NextULong()
    {
        return NextULong(ulong.MinValue, ulong.MaxValue);
    }

    /// <summary>
    /// Generate a <see cref="ulong"/>.
    /// </summary>
    /// <param name="max">Exclusive upper bound.</param>
    /// <returns>A 64-bit unsigned integer.</returns>
    public abstract ulong NextULong(ulong max);

    /// <summary>
    /// Generate a <see cref="ulong"/>.
    /// </summary>
    /// <param name="min">Inclusive lower bound.</param>
    /// <param name="max">Exclusive upper bound.</param>
    /// <returns>A 64-bit unsigned integer between the <paramref name="min"/> and <paramref name="max"/> values.</returns>
    public virtual ulong NextULong(ulong min, ulong max)
    {
        var range = max - min;
        var x = NextULong();

        var bigULong = Math128.Multiply(x, range);

        if (bigULong.Low >= range)
            return bigULong.High;

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

        return bigULong.High;
    }

    /// <summary>
    /// Generate a <see cref="double"/>.
    /// </summary>
    /// <returns>A double-precision floating point number.</returns>
    /// <remarks>Value is between 0.0 and 1.0.</remarks>
    public virtual double NextDouble()
    {
        return (NextULong() >> 11) * (1.0 / (1UL << 53));
    }

    /// <summary>
    /// Generate a <see cref="double"/>.
    /// </summary>
    /// <param name="min">Inclusive lower bound.</param>
    /// <param name="max">Exclusive upper bound.</param>
    /// <returns>A double-precision floating point number between the <paramref name="min"/> and <paramref name="max"/> values.</returns>
    public virtual double NextDouble(double min, double max)
    {
        var difference = max - min;
        return min + (NextDouble() % difference);
    }

    /// <summary>
    /// Returns the integer ceiling log of the specified <paramref name="value"/>, base 2.
    /// </summary>
    /// <param name="value">Value to perform the ceiling operation on.</param>
    /// <returns>The integer ceiling log of the specified <paramref name="value"/>, base 2.</returns>
    internal static int Log2Ceiling(ulong value)
    {
        var result = BitOperations.Log2(value);
        if (BitOperations.PopCount(value) != 1)
        {
            result++;
        }

        return result;
    }
}
