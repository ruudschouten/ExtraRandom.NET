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
    public readonly byte NextByte(byte min, byte max)
    {
        return (byte)Roll(min, max);
    }

    /// <inheritdoc cref="IRandom.NextInt()"/>
    public readonly int NextInt(int min, int max)
    {
        return (int)Roll(min, max);
    }

    /// <inheritdoc cref="IRandom.NextLong()"/>
    public readonly long NextLong(long min, long max)
    {
        return Roll(min, max);
    }

    private readonly long Roll(long min, long max)
    {
        var average = (max - min) / 2;

        var closestMin = max;
        var closestMax = max;
        var closestAvg = max;

        var closestForBias = 0L;

        for (var i = 0; i < _rolls; i++)
        {
            var roll = _random.NextLong(min, max);

            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (_bias)
            {
                case Bias.Lower:
                    var minDiff = roll - min;
                    if (minDiff >= closestMin)
                        continue;

                    closestMin = minDiff;
                    break;

                case Bias.Average:
                    var difference = closestAvg;
                    if (roll == average)
                        return roll;

                    if (roll > average)
                        difference = roll - average;
                    if (roll < average)
                        difference = average - roll;

                    if (difference >= closestAvg)
                        continue;

                    closestAvg = difference;
                    break;

                case Bias.Higher:
                    var maxDiff = max - roll;
                    if (maxDiff >= closestMax)
                        continue;

                    closestMax = maxDiff;
                    break;
            }

            closestForBias = roll;
        }

        return closestForBias;
    }
}
