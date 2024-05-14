using System.Runtime.InteropServices;
using ExtraRandom.Validator;
using ExtraUtil.Math;

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
    public BiasedRandom(IRandom random, Bias bias, int rolls = 1)
    {
        _random = random;
        _bias = bias;
        if (rolls < 1)
            throw new ArgumentOutOfRangeException(nameof(rolls), "Rolls must be greater than 0.");
        _rolls = rolls;
    }

    #region IRandom interface

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
        return (ulong)Roll(min, max);
    }

    /// <inheritdoc />
    public double NextDouble()
    {
        return NextDouble(0, 1.0);
    }

    /// <inheritdoc />
    public double NextDouble(double min, double max)
    {
        return Roll(min, max);
    }

    #endregion

    private double Roll(double min, double max)
    {
        NextInRangeValidator.ValidateRange(min, max);

        var closestForBias = SetBiasValues(
            min,
            max,
            out var average,
            out var closestAvg,
            out var goldenRatio
        );

        for (var i = 0; i < _rolls; i++)
        {
            double roll;
            switch (_bias)
            {
                case Bias.Lower:
                    roll = _random.NextDouble(min, max);
                    closestForBias = GetClosestForLowerBias(roll, closestForBias);
                    break;
                case Bias.Higher:
                    roll = _random.NextDouble(min, max);
                    closestForBias = GetClosestForHigherBias(roll, closestForBias);
                    break;
                case Bias.Average:
                    roll = _random.NextDouble(min, max);
                    closestForBias = GetClosestForAverageBias(roll, average, ref closestAvg);
                    break;
                case Bias.GoldenRatio:
                    closestForBias = GetClosestForGoldenRatioBias(
                        _random.NextDouble(),
                        min,
                        max,
                        closestForBias,
                        goldenRatio
                    );
                    break;
                default:
                    throw new InvalidOperationException("Invalid bias type.");
            }
        }

        return closestForBias;
    }

    private double SetBiasValues(
        double min,
        double max,
        out double average,
        out double closestAvg,
        out double goldenRatio
    )
    {
        var closestForBias = 0.0;
        average = double.MinValue;
        closestAvg = double.MinValue;
        goldenRatio = double.MinValue;
        switch (_bias)
        {
            case Bias.Lower:
                closestForBias = double.MaxValue;
                break;
            case Bias.Higher:
                closestForBias = double.MinValue;
                break;
            case Bias.GoldenRatio:
                goldenRatio = min + ((max - min) / NumericConstants.GoldenRatio);
                break;
            case Bias.Average:
                average = (max - min) / 2;
                closestAvg = max;
                break;
            default:
                throw new InvalidOperationException("Invalid bias type.");
        }

        return closestForBias;
    }

    private static double GetClosestForLowerBias(double roll, double closestForBias)
    {
        return roll < closestForBias ? roll : closestForBias;
    }

    private static double GetClosestForHigherBias(double roll, double closestForBias)
    {
        return roll > closestForBias ? roll : closestForBias;
    }

    private static double GetClosestForAverageBias(
        double roll,
        double average,
        ref double closestAvg
    )
    {
        var difference = average - roll;
        if (difference < closestAvg)
            closestAvg = difference;
        return roll;
    }

    private static double GetClosestForGoldenRatioBias(
        double baseRoll,
        double min,
        double max,
        double currentClosest,
        double closestForBias
    )
    {
        var roll = baseRoll + NumericConstants.GoldenRatioConjugate;
        roll %= 1;
        roll = (roll * (max - min)) + min;

        return closestForBias.GetClosest(roll, currentClosest);
    }
}
