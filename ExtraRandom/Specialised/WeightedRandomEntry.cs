using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace ExtraRandom.Specialised;

/// <summary>
/// Symbolises an entry used in a Weighted Random generator.
/// </summary>
/// <typeparam name="T">Type for the <see cref="Value"/> property.</typeparam>
[StructLayout(LayoutKind.Sequential)]
public readonly struct WeightedRandomEntry<T>
    : IEquatable<WeightedRandomEntry<T>>,
        IComparable<WeightedRandomEntry<T>>,
        IComparable
    where T : notnull
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WeightedRandomEntry{T}"/> struct.
    /// </summary>
    /// <param name="value">Value for this entry.</param>
    /// <param name="weight">Likeliness that this entry gets chosen.</param>
    [SetsRequiredMembers]
    public WeightedRandomEntry(T value, int weight)
    {
        Value = value;
        Weight = weight;
    }

    /// <summary>
    /// Gets the value for this entry.
    /// </summary>
    public required T Value { get; init; }

    /// <summary>
    /// Gets the likeliness that this entry gets chosen.
    /// </summary>
    public required int Weight { get; init; }

    #region Equals and Comparing

    /// <summary>
    /// Check if <paramref name="left"/> is equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">Object to check for matches.</param>
    /// <param name="right">Object to check matches with.</param>
    /// <returns><c>true</c> if matched, <c>false</c> otherwise.</returns>
    public static bool operator ==(WeightedRandomEntry<T> left, WeightedRandomEntry<T> right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Check if <paramref name="left"/> is not equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">Object to check for matches.</param>
    /// <param name="right">Object to check matches with.</param>
    /// <returns><c>true</c> if not matched, <c>false</c> otherwise.</returns>
    public static bool operator !=(WeightedRandomEntry<T> left, WeightedRandomEntry<T> right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    /// Check if <paramref name="left"/>'s <see cref="Weight"/> is smaller than <param name="right"></param>'s <see cref="Weight"/>.
    /// </summary>
    /// <param name="left">Object to check if it is smaller.</param>
    /// <param name="right">Object to check with.</param>
    /// <returns><c>true</c> is smaller, <c>false</c>otherwise.</returns>
    public static bool operator <(WeightedRandomEntry<T> left, WeightedRandomEntry<T> right)
    {
        return left.CompareTo(right) < 0;
    }

    /// <summary>
    /// Check if <paramref name="left"/>'s <see cref="Weight"/> is larger than <param name="right"></param>'s <see cref="Weight"/>.
    /// </summary>
    /// <param name="left">Object to check if it is larger.</param>
    /// <param name="right">Object to check with.</param>
    /// <returns><c>true</c> is larger, <c>false</c>otherwise.</returns>
    public static bool operator >(WeightedRandomEntry<T> left, WeightedRandomEntry<T> right)
    {
        return left.CompareTo(right) > 0;
    }

    /// <summary>
    /// Check if <paramref name="left"/>'s <see cref="Weight"/> is smaller or equal to <param name="right"></param>'s <see cref="Weight"/>.
    /// </summary>
    /// <param name="left">Object to check if it is smaller or equal.</param>
    /// <param name="right">Object to check with.</param>
    /// <returns><c>true</c> is smaller, <c>false</c>otherwise.</returns>
    public static bool operator <=(WeightedRandomEntry<T> left, WeightedRandomEntry<T> right)
    {
        return left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Check if <paramref name="left"/>'s <see cref="Weight"/> is larger or equal to <param name="right"></param>'s <see cref="Weight"/>.
    /// </summary>
    /// <param name="left">Object to check if it is larger or equal.</param>
    /// <param name="right">Object to check with.</param>
    /// <returns><c>true</c> is smaller, <c>false</c>otherwise.</returns>
    public static bool operator >=(WeightedRandomEntry<T> left, WeightedRandomEntry<T> right)
    {
        return left.CompareTo(right) >= 0;
    }

    /// <inheritdoc />
    public bool Equals(WeightedRandomEntry<T> other)
    {
        return Weight == other.Weight && EqualityComparer<T>.Default.Equals(Value, other.Value);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is WeightedRandomEntry<T> other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Weight, Value);
    }

    /// <inheritdoc />
    public int CompareTo(WeightedRandomEntry<T> other)
    {
        return Weight.CompareTo(other.Weight);
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return 1;
        return obj is WeightedRandomEntry<T> other
            ? CompareTo(other)
            : throw new ArgumentException(
                $"Object must be of type {nameof(WeightedRandomEntry<T>)}",
                nameof(obj)
            );
    }

    #endregion
}
