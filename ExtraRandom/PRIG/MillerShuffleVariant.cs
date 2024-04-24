using System.Diagnostics.CodeAnalysis;

namespace ExtraRandom.PRIG;

/// <summary>
/// Variants of the Miller Shuffle Algorithm.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum MillerShuffleVariant
{
    /// <summary>
    /// <para>Variant, works just like <see cref="MS_Lite"/>, but uses one less step when rotating to get a index.
    /// This makes it faster, but more collision-prone.
    /// </para>
    /// <para>However, this Variant is more than enough for most use cases.</para>
    /// </summary>
    MS_XLite = 0,

    /// <summary>
    /// Variant suited for when you have upwards to a million unique index possibilities.
    /// </summary>
    MS_Lite,

    /// <summary>
    /// Variant which is not repeatable due to the use of a random generator.
    /// Better at randomixing pattern occurrences, providing very good sequence distribution over time.
    /// Preferred for Shuffles used for dealing to Serious! competing players.
    /// </summary>
    /// <remarks>Use is not generally advised due to it:
    /// <list type="number">
    /// <item><description>not being repeatable 1:1</description></item>
    /// <item><description>won't work for concurrent or nested shuffles</description></item>
    /// </list>
    /// </remarks>
    MSA_b,

    /// <summary>
    /// Variant suited for when you have billions of unique index possibilities.
    /// </summary>
    MSA_d,

    /// <summary>
    /// Variant suited for when you have billions of unique index possibilities.
    /// Produces nearly the same randomness as <see cref="MSA_d"/>, with changes and added code to increase the potential number of shuffle permutations generated.
    /// </summary>
    MSA_e
}
