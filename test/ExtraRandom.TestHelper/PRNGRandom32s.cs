using System.Diagnostics.CodeAnalysis;
using ExtraRandom.PRNG;
using Xunit;
using static ExtraRandom.TestHelper.SeedHelper;

namespace ExtraRandom.TestHelper;

/// <summary>
/// Test data containing all 32 bit PRNGs.
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
public class PRNGRandom32s : TheoryData<Random32>
{
    public PRNGRandom32s()
    {
        Add(new RomuMono(Seeds[0]));
        Add(new RomuTrio32(Seeds[0], Seeds[1], Seeds[2]));
        Add(new RomuQuad32(Seeds[0], Seeds[1], Seeds[2], Seeds[3]));
        Add(new Xoroshiro64Star(Seeds[0], Seeds[1]));
        Add(new Xoroshiro64StarStar(Seeds[0], Seeds[1]));
        Add(new Xoshiro128Plus(Seeds[0], Seeds[1], Seeds[2], Seeds[3]));
        Add(new Xoshiro128PlusPlus(Seeds[0], Seeds[1], Seeds[2], Seeds[3]));
        Add(new Xoshiro128StarStar(Seeds[0], Seeds[1], Seeds[2], Seeds[3]));
    }
}
