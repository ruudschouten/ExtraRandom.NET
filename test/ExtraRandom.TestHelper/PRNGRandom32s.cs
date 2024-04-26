using System.Diagnostics.CodeAnalysis;
using ExtraRandom.PRNG;
using Xunit;

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
        Add(new MiddleSquareWeylSequence(500));
    }
}
