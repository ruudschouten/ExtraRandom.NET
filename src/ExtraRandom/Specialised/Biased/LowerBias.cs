namespace ExtraRandom.Specialised.Biased;

/// <summary>
/// Bias which tries to get lower values.
/// </summary>
public readonly record struct LowerBias : IBias
{
    /// <inheritdoc />
    public double SetBiasValues(double min, double max)
    {
        return double.MaxValue;
    }

    /// <inheritdoc />
    public double Roll(in IRandom random, double min, double max)
    {
        return random.NextDouble(min, max);
    }

    /// <inheritdoc />
    public double GetClosest(double roll, double closest)
    {
        return roll < closest ? roll : closest;
    }
}
