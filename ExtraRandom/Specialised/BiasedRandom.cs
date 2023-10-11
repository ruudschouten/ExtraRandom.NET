using System.Numerics;
using System.Runtime.InteropServices;

namespace ExtraRandom.Specialised;

/// <summary>
/// Specialised random generator in which you can specify a bias.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct BiasedRandom
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

    /// <inheritdoc cref="IRandom.NextByte()"/>
    public byte NextByte(byte min, byte max)
    {
        return (byte)Roll(min, max);
    }

    /// <inheritdoc cref="IRandom.NextInt()"/>
    public int NextInt(int min, int max)
    {
        return (int)Roll(min, max);
    }

    /// <inheritdoc cref="IRandom.NextLong()"/>
    public long NextLong(long min, long max)
    {
        return Roll(min, max);
    }

    /// <summary>
    /// Generate a <see cref="double"/>.
    /// </summary>
    /// <param name="min">Inclusive lower bound.</param>
    /// <param name="max">Exclusive upper bound.</param>
    /// <param name="tolerance">Tolerance value used to check if a value is equal to another value.</param>
    /// <returns>A double-precision floating point number between the <paramref name="min"/> and <paramref name="max"/> values.</returns>
    public double NextDouble(double min, double max, double tolerance = 0.05d)
    {
        return Roll(min, max, tolerance);
    }

    private long Roll(long min, long max)
    {
        var average = (max - min) / 2;
        var closestAvg = max;
        long? closestForBias = null;

        for (var i = 0; i < _rolls; i++)
        {
            var roll = _random.NextLong(min, max);

            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (_bias)
            {
                case Bias.Lower:
                    if (roll >= closestForBias)
                        continue;
                    break;

                case Bias.Average:
                    if (roll == average)
                        return roll;

                    var difference = Math.Abs(average - roll);
                    if (difference >= closestAvg)
                        continue;

                    closestAvg = difference;
                    break;

                case Bias.Higher:
                    if (roll <= closestForBias)
                        continue;
                    break;
            }

            closestForBias = roll;
        }

        return (long)closestForBias!;
    }

    private double Roll(double min, double max, double tolerance)
    {
        var average = (max - min) / 2;
        var closestAvg = max;
        double? closestForBias = null;

        for (var i = 0; i < _rolls; i++)
        {
            var roll = _random.NextDouble(min, max);

            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (_bias)
            {
                case Bias.Lower:
                    if (roll >= closestForBias)
                        continue;
                    break;

                case Bias.Average:
                    if (Math.Abs(roll - average) < tolerance)
                        return roll;

                    var difference = Math.Abs(average - roll);
                    if (difference >= closestAvg)
                        continue;

                    closestAvg = difference;
                    break;

                case Bias.Higher:
                    if (roll <= closestForBias)
                        continue;
                    break;
            }

            closestForBias = roll;
        }

        return (double)closestForBias!;
    }
}
