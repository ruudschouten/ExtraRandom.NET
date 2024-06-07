using System.Runtime.InteropServices;
using ExtraUtil.Math;

namespace ExtraRandom.Specialised.Biased;

/// <summary>
/// Bias which tries to get values close to the average value.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public record AverageBias : IBias
{
    private double _average;
    private double _closestAverage;

    /// <inheritdoc />
    public double SetBiasValues(double min, double max)
    {
        _closestAverage = max;
        _average = (max - min) / 2;
        return 0.0;
    }

    /// <inheritdoc />
    public double Roll(in IRandom random, double min, double max)
    {
        return random.NextDouble(min, max);
    }

    /// <inheritdoc />
    public double GetClosest(double roll, double closest)
    {
        _closestAverage = _average.GetClosest(roll, _closestAverage);
        return _closestAverage;
    }
}
