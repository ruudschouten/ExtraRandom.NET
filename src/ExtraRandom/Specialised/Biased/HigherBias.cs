namespace ExtraRandom.Specialised.Biased;

/// <summary>
/// Bias which tries to get higher values.
/// </summary>
public record HigherBias : IBias
{
    /// <inheritdoc />
    public double SetBiasValues(double min, double max)
    {
        return double.MinValue;
    }

    /// <inheritdoc />
    public double Roll(in IRandom random, double min, double max)
    {
        return random.NextDouble(min, max);
    }

    /// <inheritdoc />
    public double GetClosest(double roll, double closest)
    {
        return roll > closest ? roll : closest;
    }
}
