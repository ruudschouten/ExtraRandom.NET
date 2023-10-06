using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using ExtraRandom.PRNG;
using Xunit.Abstractions;

// ReSharper disable HeuristicUnreachableCode
// ReSharper disable RedundantAssignment
#pragma warning disable CS0162 // Unreachable code detected -> Ignored because of the const bool which can be set to true for testing.

namespace ExtraRandom.UnitTests.PRNG;

[SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1600:Elements should be documented",
    Justification = "These are tests"
)]
[SuppressMessage(
    "ReSharper",
    "InconsistentNaming",
    Justification = "PRNG is an abbreviation, so the naming is fine."
)]
public class PRNGTest
{
    private const int Loops = 500;

    /// <summary>
    /// Determines if the test should keep track of how many times a single number has been generated.
    /// </summary>
    /// <remarks>If this is set to <c>true</c>, it will impact performance quite a bit.</remarks>
    private const bool _recordAmountOfGeneratedNumbers = true;

    private readonly ITestOutputHelper _output;

    public PRNGTest(ITestOutputHelper output)
    {
        _output = output;
    }

    /// <summary>
    /// Method used by PRNGTest to determine what PRNG to use.
    /// New PRNGs should be added here for testing.
    /// </summary>
    /// <returns>A collection of PRNGs which are used by xUnit.</returns>
    public static IEnumerable<object[]> PRNGs()
    {
        yield return new object[] { new Shishua(500) };
        yield return new object[] { new Xoroshiro128Plus(500) };
    }

    [Theory]
    [MemberData(nameof(PRNGs))]
    public void IntRange(IRandom rand)
    {
        var generateHits = new SortedDictionary<int, int>();

        const int minInt = 0;
        const int maxInt = 21475;

        for (var i = 0; i < Loops; i++)
        {
            var @int = rand.NextInt(minInt, maxInt);
            Assert.InRange(@int, minInt, maxInt);

            RecordHit(@int, ref generateHits);
        }

        PrintHits(ref generateHits);
    }

    [Theory]
    [MemberData(nameof(PRNGs))]
    public void LongRange(IRandom rand)
    {
        var generateHits = new SortedDictionary<long, int>();

        const long minLong = 0;
        const long maxLong = long.MaxValue;

        for (var i = 0; i < Loops; i++)
        {
            var @long = rand.NextLong(minLong, maxLong);
            Assert.InRange(@long, minLong, maxLong);

            RecordHit(@long, ref generateHits);
        }

        PrintHits(ref generateHits);
    }

    [Theory]
    [MemberData(nameof(PRNGs))]
    public void DoubleRange(IRandom rand)
    {
        var generateHits = new SortedDictionary<double, int>();

        const double minDouble = -1.0;
        const double maxDouble = 3.5;

        for (var i = 0; i < Loops; i++)
        {
            var @double = rand.NextDouble(minDouble, maxDouble);
            Assert.InRange(@double, minDouble, maxDouble);

            RecordHit(@double, ref generateHits);
        }

        PrintHits(ref generateHits);
    }

    private static void RecordHit<T>(T number, ref SortedDictionary<T, int> generateHits)
        where T : INumber<T>
    {
        if (!_recordAmountOfGeneratedNumbers)
            return;

        var exists = generateHits.ContainsKey(number);
        if (!exists)
        {
            generateHits.Add(number, 1);
            return;
        }

        generateHits[number]++;
    }

    private void PrintHits<T>(ref SortedDictionary<T, int> generateHits)
        where T : INumber<T>
    {
        if (!_recordAmountOfGeneratedNumbers)
            return;

        foreach (var hits in generateHits)
        {
            _output.WriteLine($"{hits.Key}: {hits.Value}");
        }
    }
}
