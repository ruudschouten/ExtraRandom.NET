namespace ExtraRandom.Util;

/// <summary>
/// Utility class used to add extension methods to some number data types.
/// </summary>
public static class NumberUtil
{
    /// <summary>
    /// Checks if the <paramref name="value"/> is in between the
    /// <paramref name="low"/> and <paramref name="high"/> values.
    /// </summary>
    /// <param name="value">
    /// The value to check for against <paramref name="low"/> and <paramref name="high"/>
    /// </param>
    /// <param name="low">The lower value to check against <paramref name="value"/>.</param>
    /// <param name="high">The higher value to check against <paramref name="value"/>.</param>
    /// <returns>
    /// True if <paramref name="value"/> is in between <paramref name="low"/> and <paramref name="high"/>.
    /// False otherwise.
    /// </returns>
    public static bool IsBetween(this float value, float low, float high)
    {
        return value >= low && value <= high;
    }

    /// <summary>
    /// Checks if the <paramref name="value"/> is in between the
    /// <paramref name="low"/> and <paramref name="high"/> values.
    /// </summary>
    /// <param name="value">
    /// The value to check for against <paramref name="low"/> and <paramref name="high"/>
    /// </param>
    /// <param name="low">The lower value to check against <paramref name="value"/>.</param>
    /// <param name="high">The higher value to check against <paramref name="value"/>.</param>
    /// <returns>
    /// True if <paramref name="value"/> is in between <paramref name="low"/> and <paramref name="high"/>.
    /// False otherwise.
    /// </returns>
    public static bool IsBetween(this int value, int low, int high)
    {
        return value >= low && value <= high;
    }
}