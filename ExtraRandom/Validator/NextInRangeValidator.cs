using System.Numerics;

namespace ExtraRandom.Validator;

public static class NextInRangeValidator
{
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
