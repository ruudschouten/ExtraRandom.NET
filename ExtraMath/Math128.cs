namespace ExtraMath;

/// <summary>
/// Utility class for Math methods using 128-bit numbers.
/// </summary>
public static class Math128
{
    /// <summary>
    /// Perform multiplication on two <see cref="ulong"/> values.
    /// </summary>
    /// <param name="x">First number to multiply.</param>
    /// <param name="y">Second number to multiply.</param>
    /// <returns>
    /// 128-bit number, split into two <see cref="ulong"/> instances.
    /// </returns>
    public static BigULong Multiply(ulong x, ulong y)
    {
        var range = default(BigULong);
        range.Low = x * y;

        ulong x0 = (uint)x;
        var x1 = x >> 32;

        ulong y0 = (uint)y;
        var y1 = y >> 32;

        var p11 = x1 * y1;
        var p10 = x1 * y0;
        var p01 = x0 * y1;
        var p00 = x0 * y0;

        // 64-bit product + two 32-bit values.
        var middle = p10 + (p00 >> 32) + (uint)p01;

        // 64-bit product + two 32-bit values.
        range.High = p11 + (middle >> 32) + (p01 >> 32);

        return range;
    }
}
