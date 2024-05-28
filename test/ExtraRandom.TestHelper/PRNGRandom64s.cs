using System.Diagnostics.CodeAnalysis;
using ExtraRandom.PRNG;
using Xunit;
using static ExtraRandom.TestHelper.SeedHelper;

namespace ExtraRandom.TestHelper;

/// <summary>
/// Test data containing all 64 bit PRNGs.
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
public class PRNGRandom64s : TheoryData<Random64>
{
    public PRNGRandom64s()
    {
        Add(new RomuDuoJr(Seeds[0], Seeds[1]));
        Add(new RomuDuo(Seeds[0], Seeds[1]));
        Add(new RomuTrio(Seeds[0], Seeds[1], Seeds[2]));
        Add(new RomuQuad(Seeds[0], Seeds[1], Seeds[2], Seeds[3]));
        Add(new Seiran(Seeds[0], Seeds[1]));
        Add(new SplitMix64(Seeds[0]));
        Add(new Xoroshiro128Plus(Seeds[0], Seeds[1]));
        Add(new Xoroshiro128PlusPlus(Seeds[0], Seeds[1]));
        Add(new Xoroshiro128StarStar(Seeds[0], Seeds[1]));
        Add(new Xoshiro256Plus(Seeds[0], Seeds[1], Seeds[2], Seeds[3]));
        Add(new Xoshiro256PlusPlus(Seeds[0], Seeds[1], Seeds[2], Seeds[3]));
        Add(new Xoshiro256StarStar(Seeds[0], Seeds[1], Seeds[2], Seeds[3]));
    }
}
