using System.Diagnostics.CodeAnalysis;
using ExtraRandom.PRNG;
using ExtraRandom.Specialised;
using Xunit;

namespace ExtraRandom.TestHelper;

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
public class PRNGRandoms : TheoryData<IRandom>
{
    public PRNGRandoms()
    {
        Add(new BiasedRandom(new RomuDuoJr(500), Bias.Lower, 3));
        Add(new BiasedRandom(new RomuDuoJr(500), Bias.Average, 3));
        Add(new BiasedRandom(new RomuDuoJr(500), Bias.Higher, 3));
        Add(new BiasedRandom(new RomuDuoJr(500), Bias.GoldenRatio, 3));
        Add(new MiddleSquareWeylSequence(500));
        Add(new RomuDuo(500));
        Add(new RomuDuoJr(500));
        Add(new RomuTrio(500));
        Add(new Seiran(500));
        Add(new Xoroshiro128Plus(500));
        Add(new Xoroshiro128PlusPlus(500));
        Add(new Xoroshiro128StarStar(500));
    }
}
