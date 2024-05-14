using System.Diagnostics.CodeAnalysis;
using ExtraRandom.PRNG;
using ExtraRandom.Specialised;
using Xunit;
using static ExtraRandom.TestHelper.SeedHelper;

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
        Add(new BiasedRandom(new RomuDuoJr(Seeds[0], Seeds[1]), Bias.Lower, 3));
        Add(new BiasedRandom(new RomuDuoJr(Seeds[0], Seeds[1]), Bias.Average, 3));
        Add(new BiasedRandom(new RomuDuoJr(Seeds[0], Seeds[1]), Bias.Higher, 3));
        Add(new BiasedRandom(new RomuDuoJr(Seeds[0], Seeds[1]), Bias.GoldenRatio, 3));
        Add(new RomuMono(Seeds[0]));
        Add(new RomuDuoJr(Seeds[0], Seeds[1]));
        Add(new RomuDuo(Seeds[0], Seeds[1]));
        Add(new RomuTrio(Seeds[0], Seeds[1], Seeds[2]));
        Add(new RomuTrio32(Seeds[0], Seeds[1], Seeds[2]));
        Add(new RomuQuad(Seeds[0], Seeds[1], Seeds[2], Seeds[3]));
        Add(new RomuQuad32(Seeds[0], Seeds[1], Seeds[2], Seeds[3]));
        Add(new Seiran(Seeds[0], Seeds[1]));
        Add(new Xoroshiro128Plus(Seeds[0], Seeds[1]));
        Add(new Xoroshiro128PlusPlus(Seeds[0], Seeds[1]));
        Add(new Xoroshiro128StarStar(Seeds[0], Seeds[1]));
    }
}
