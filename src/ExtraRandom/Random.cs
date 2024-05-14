using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using ExtraRandom.Validator;
using ExtraUtil.Math;

namespace ExtraRandom;

/// <summary>
/// Represents a psuedo-random number generator, which is an algorithm that produces a sequence of numbers that meet
/// certain requirements for randomness.
/// </summary>
public abstract class Random : IRandom
{
    /// <inheritdoc cref="IRandom.SetSeed(ulong)" />
    public abstract void SetSeed(ulong seed);

    /// <inheritdoc cref="IRandom.SetSeed(ulong[])" />
    public abstract void SetSeed(params ulong[] seed);

    /// <inheritdoc />
    public abstract bool NextBoolean();

    #region Bytes

    /// <inheritdoc />
    public abstract byte NextByte();

    /// <inheritdoc />
    public virtual byte NextByte(byte min, byte max)
    {
        if (min == max)
            return min;

        NextInRangeValidator.ValidateRange(min, max);

        return (byte)NextUInt(min, max);
    }

    #endregion

    /// <inheritdoc />
    public abstract int NextInt();

    /// <inheritdoc />
    public virtual int NextInt(int min, int max)
    {
        if (min == max)
            return min;

        NextInRangeValidator.ValidateRange(min, max);

        var exclusiveRange = max - min;
        if (exclusiveRange <= 1)
            return min;

        var bits = Log2Ceiling((ulong)exclusiveRange);
        while (true)
        {
            var result = NextLong() >> (64 - bits);

            if (result < exclusiveRange)
            {
                return (int)result + min;
            }
        }
    }

    /// <inheritdoc />
    public abstract uint NextUInt();

    /// <inheritdoc />
    public virtual uint NextUInt(uint min, uint max)
    {
        if (min == max)
            return min;

        NextInRangeValidator.ValidateRange(min, max);

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
    [SuppressMessage(
        "ReSharper",
        "IntVariableOverflowInUncheckedContext",
        Justification = "Overflow is part of the randomness."
    )]
    public virtual long NextLong(long min, long max)
    {
        var result = NextULong((ulong)min, (ulong)max);

        if (result <= long.MaxValue)
            return (long)result;

        return (long)result >> 1;
    }

    /// <inheritdoc />
    public abstract ulong NextULong();

    /// <inheritdoc />
    public virtual ulong NextULong(ulong min, ulong max)
    {
        if (min == max)
            return min;

        NextInRangeValidator.ValidateRange(min, max);

        var range = max - min;
        var x = NextULong();

        var bigULong = Math128.Multiply(x, range);
        if (bigULong.Low >= range)
            return bigULong.High + min;

        // ReSharper disable once IntVariableOverflowInUncheckedContext
        // Overflow is checked later in the if and while statements.
        var t = 0ul - range;
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
        NextInRangeValidator.ValidateRange(min, max);
        return (NextDouble() * (max - min)) + min;
    }

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
