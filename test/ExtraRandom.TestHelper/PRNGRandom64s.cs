using System.Diagnostics.CodeAnalysis;
using ExtraRandom.PRNG;
using Xunit;

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
        Add(new RomuDuo(500));
        Add(new RomuDuoJr(500));
        Add(new RomuTrio(500));
        Add(new Seiran(500));
        Add(new Xoroshiro128Plus(500));
        Add(new Xoroshiro128PlusPlus(500));
        Add(new Xoroshiro128StarStar(500));
    }
}
