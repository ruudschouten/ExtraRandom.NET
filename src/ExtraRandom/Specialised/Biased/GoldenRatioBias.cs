using ExtraUtil.Math;

namespace ExtraRandom.Specialised.Biased;

/// <summary>
/// <para>
/// Bias which tries to get values close to the golden ratio.
/// </para>
/// <para>
/// The golden ration is determined from the <c>min</c> and <c>max</c> values passed in <see cref="SetBiasValues"/>.
/// </para>
/// </summary>
public record struct GoldenRatioBias : IBias
{
    private double _goldenRatio;

    /// <inheritdoc />
    public double SetBiasValues(double min, double max)
    {
        _goldenRatio = min + ((max - min) / NumericConstants.GoldenRatio);
        return 0.0;
    }

    /// <inheritdoc />
    public readonly double Roll(in IRandom random, double min, double max)
    {
        var baseRoll = random.NextDouble();
        var roll = baseRoll + NumericConstants.GoldenRatioConjugate;
        roll %= 1;
        return (roll * (max - min)) + min;
    }

    /// <inheritdoc />
    public readonly double GetClosest(double roll, double closest)
    {
        return _goldenRatio.GetClosest(roll, closest);
    }
}
