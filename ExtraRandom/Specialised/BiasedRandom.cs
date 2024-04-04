using System.Numerics;
using System.Runtime.InteropServices;
using ExtraRandom.Validator;

namespace ExtraRandom.Specialised;

/// <summary>
/// Specialised random generator in which you can specify a bias.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct BiasedRandom : IRandom
{
    /// <summary>
    /// PRNG instance to use for the random rolls.
    /// </summary>
    private readonly IRandom _random;

    /// <summary>
    /// What type of results to favour.
    /// </summary>
    private readonly Bias _bias;

    /// <summary>
    /// Amount of rolls to perform before picking a result closest to the <see name="_bias"/>.
    /// </summary>
    private readonly int _rolls;

    /// <summary>
    /// Initializes a new instance of the <see cref="BiasedRandom"/> struct.
    /// </summary>
    /// <param name="random">PRNG instance to use for the random rolls.</param>
    /// <param name="bias">What type of results to favour.</param>
    /// <param name="rolls">Amount of rolls to perform before picking a result closest to the <paramref name="bias"/>.</param>
    public BiasedRandom(IRandom random, Bias bias, int rolls)
    {
        _random = random;
        _bias = bias;
        _rolls = rolls;
    }

    /// <inheritdoc />
    public void Reseed()
    {
        _random.Reseed();
    }

    /// <inheritdoc cref="IRandom.SetSeed(ulong)" />
    public void SetSeed(ulong seed)
    {
        _random.SetSeed(seed);
    }

    /// <inheritdoc cref="IRandom.SetSeed(ulong[])" />
    public void SetSeed(params ulong[] seed)
    {
        _random.SetSeed(seed);
    }

    /// <inheritdoc />
    public bool NextBoolean()
    {
        return _random.NextBoolean();
    }

    /// <inheritdoc />
    public byte NextByte()
    {
        return NextByte(byte.MinValue, byte.MaxValue);
    }

    /// <inheritdoc/>
    public byte NextByte(byte min, byte max)
    {
        return (byte)Roll(min, max);
    }

    /// <inheritdoc />
    public int NextInt()
    {
        return NextInt(int.MinValue, int.MaxValue);
    }

    /// <inheritdoc />
    public int NextInt(int min, int max)
    {
        return (int)Roll(min, max);
    }

    /// <inheritdoc />
    public uint NextUInt()
    {
        return NextUInt(uint.MinValue, uint.MaxValue);
    }

    /// <inheritdoc />
    public uint NextUInt(uint min, uint max)
    {
        return (uint)Roll(min, max);
    }

    /// <inheritdoc />
    public long NextLong()
    {
        return NextLong(long.MinValue, long.MaxValue);
    }

    /// <inheritdoc cref="IRandom.NextLong()"/>
    public long NextLong(long min, long max)
    {
        return (long)Roll(min, max);
    }

    /// <inheritdoc />
    public ulong NextULong()
    {
        return NextULong(ulong.MinValue, ulong.MaxValue);
    }

    /// <inheritdoc />
    public ulong NextULong(ulong min, ulong max)
    {
        return Roll(min, max);
    }

    /// <inheritdoc />
    public double NextDouble()
    {
        return NextDouble(double.MinValue, double.MaxValue);
    }

    /// <inheritdoc />
    public double NextDouble(double min, double max)
    {
        return Roll(min, max);
    }

    private ulong Roll(ulong min, ulong max)
    {
        if (min == max)
            return min;

        NextInRangeValidator.ValidateRange(min, max);

        var average = (max - min) / 2;
        var closestAvg = max;

        var closestForBias = _bias switch
        {
            Bias.Lower => ulong.MaxValue,
            Bias.Higher => ulong.MinValue,
            _ => 0UL
        };

        for (var i = 0; i < _rolls; i++)
        {
            var roll = _random.NextULong(min, max);
            closestForBias = GetClosestForBias(roll, closestForBias, average, ref closestAvg);
        }

        return closestForBias;
    }

    private double Roll(double min, double max)
    {
        NextInRangeValidator.ValidateRange(min, max);

        var average = (max - min) / 2;
        var closestAvg = max;

        var closestForBias = _bias switch
        {
            Bias.Lower => double.MaxValue,
            Bias.Higher => double.MinValue,
            _ => 0
        };

        for (var i = 0; i < _rolls; i++)
        {
            var roll = _random.NextDouble(min, max);
            closestForBias = GetClosestForBias(roll, closestForBias, average, ref closestAvg);
        }

        return closestForBias;
    }

    private T GetClosestForBias<T>(T roll, T closestForBias, T average, ref T closestAvg)
        where T : INumber<T>
    {
        switch (_bias)
        {
            case Bias.Lower when roll < closestForBias:
                return roll;
            case Bias.Average:
            {
                var difference = average - roll;
                if (difference < closestAvg)
                    closestAvg = difference;
                return roll;
            }

            case Bias.Higher when roll > closestForBias:
                return roll;
            default:
                return closestForBias;
        }
    }
}
