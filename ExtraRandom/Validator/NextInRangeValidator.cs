using System.Numerics;

namespace ExtraRandom.Validator;

/// <summary>
/// Contains validation methods to check ranges.
/// </summary>
public static class NextInRangeValidator
{
    /// <summary>
    /// Validates that the range is within the range of <paramref name="min"/> and <paramref name="max"/>.
    /// </summary>
    /// <param name="min">Minimum value.</param>
    /// <param name="max">Maximum value.</param>
    public static void ValidateRange<T>(T min, T max)
        where T : INumber<T>
    {
        if (min > max)
        {
            throw new ArgumentOutOfRangeException(
                nameof(min),
                "Min value must be smaller than or equal to max value."
            );
        }
    }
}
