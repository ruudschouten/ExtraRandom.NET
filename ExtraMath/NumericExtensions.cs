using System.Numerics;

namespace ExtraMath;

/// <summary>
/// Utility class used to add extension methods to some number data types.
/// </summary>
public static class NumericExtensions
{
    /// <summary>
    /// Checks if the <paramref name="value"/> is in between the <paramref name="low"/> and <paramref name="high"/> values.
    /// </summary>
    /// <param name="value">
    /// The value to check for against <paramref name="low"/> and <paramref name="high"/>.
    /// </param>
    /// <param name="low">The lower value to check against <paramref name="value"/>.</param>
    /// <param name="high">The higher value to check against <paramref name="value"/>.</param>
    /// <typeparam name="T">
    /// Any object that implements <see cref="INumber{TSelf}"/>.
    /// </typeparam>
    /// <returns>
    /// True if <paramref name="value"/> is in between <paramref name="low"/> and <paramref name="high"/>.
    /// False otherwise.
    /// </returns>
    public static bool IsBetween<T>(this INumber<T> value, INumber<T> low, INumber<T> high)
        where T : INumber<T>
    {
        var comparedToHigh = value.CompareTo(high);
        var comparedToLow = value.CompareTo(low);

        var smallerThanHigh = comparedToHigh <= 0;
        var higherThanLow = comparedToLow >= 0;

        return smallerThanHigh && higherThanLow;
    }

    /// <summary>
    /// Get the closest value to the <paramref name="value"/> between the <paramref name="first"/> and <paramref name="second"/> values.
    /// </summary>
    public static double GetClosest(this double value, double first, double second)
    {
        var firstDifference = Math.Abs(value - first);
        var secondDifference = Math.Abs(value - second);

        return firstDifference < secondDifference ? first : second;
    }
}
