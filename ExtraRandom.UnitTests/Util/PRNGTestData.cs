using System.Collections;
using System.Diagnostics.CodeAnalysis;
using ExtraRandom.PRNG;
using ExtraRandom.Specialised;

namespace ExtraRandom.UnitTests.Util;

/// <summary>
/// Test data containing all PRNGs.
/// </summary>
[SuppressMessage(
    "ReSharper",
    "InconsistentNaming",
    Justification = "PRNG is an abbreviation, so the naming is fine."
)]
[SuppressMessage(
    "Minor Code Smell",
    "S101:Types should be named in PascalCase",
    Justification = "PRNG is an abbreviation, so the naming is fine."
)]
public class PRNGTestData : IEnumerable<object[]>
{
    /// <summary>
    /// Method used to determine what PRNG to use.
    /// New PRNGs should be added here for testing.
    /// </summary>
    /// <returns>A collection of PRNGs which are used by xUnit.</returns>
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { new BiasedRandom(new RomuDuoJr(500), Bias.Lower, 3) };
        yield return new object[] { new BiasedRandom(new RomuDuoJr(500), Bias.Average, 3) };
        yield return new object[] { new BiasedRandom(new RomuDuoJr(500), Bias.Higher, 3) };
        yield return new object[] { new MiddleSquareWeylSequence(500) };
        yield return new object[] { new RomuDuo(500) };
        yield return new object[] { new RomuDuoJr(500) };
        yield return new object[] { new RomuTrio(500) };
        yield return new object[] { new Seiran(500) };
        yield return new object[] { new Xoroshiro128Plus(500) };
        yield return new object[] { new Xoroshiro128PlusPlus(500) };
        yield return new object[] { new Xoroshiro128StarStar(500) };
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
