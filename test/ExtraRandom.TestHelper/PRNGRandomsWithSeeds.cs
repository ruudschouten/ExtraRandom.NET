using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace ExtraRandom.TestHelper;

/// <summary>
/// Test data containing all PRNGs with multiple different seeds
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
public class PRNGRandomsWithSeeds : TheoryData<IRandom, long>
{
    private readonly long[] _seeds = { 500L, 250, 125, 70, 5 };

    public PRNGRandomsWithSeeds()
    {
        foreach (var data in new PRNGRandoms())
        {
            var prng = data[0] as IRandom ?? throw new InvalidOperationException();

            foreach (var seed in _seeds)
            {
                Add(prng, seed);
            }
        }
    }
}
