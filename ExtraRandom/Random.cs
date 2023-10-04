using System.Numerics;
using ExtraMath;

namespace ExtraRandom;

/// <summary>
/// Represents a psuedo-random number generator, which is an algorithm that produces a sequence of numbers that meet
/// certain requirements for randomness.
/// </summary>
public abstract class Random : IRandom
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

    /// <inheritdoc />
    public abstract void Reseed();

    /// <inheritdoc />
    public abstract bool NextBoolean();

    #region Bytes

    /// <inheritdoc />
    public virtual byte NextByte()
    {
        return NextByte(byte.MinValue, byte.MaxValue);
    }

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
    public virtual int NextInt()
    {
        return NextInt(_minIntValue, int.MaxValue);
    }

    /// <inheritdoc />
    public virtual int NextInt(int min, int max)
    {
        return (int)(NextUInt((uint)min, (uint)max) >> 1);
    }

    /// <inheritdoc />
    public virtual uint NextUInt()
    {
        return NextUInt(uint.MinValue, uint.MaxValue);
    }

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
    public virtual long NextLong()
    {
        return NextLong(_minLongValue, long.MaxValue);
    }

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
    public virtual ulong NextULong()
    {
        return NextULong(ulong.MinValue, ulong.MaxValue);
    }

    /// <inheritdoc />
    public virtual ulong NextULong(ulong min, ulong max)
    {
        var range = max - min;
        var x = NextULong();

        var bigULong = Math128.Multiply(x, range);

        if (bigULong.Low >= range)
            return bigULong.High;

        // ReSharper disable once IntVariableOverflowInUncheckedContext
        // This should be handled by the if check above.
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
