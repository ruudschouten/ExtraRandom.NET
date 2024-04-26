using System.Diagnostics.CodeAnalysis;
using ExtraRandom.TestHelper;
using Xunit.Abstractions;

// ReSharper disable HeuristicUnreachableCode
// ReSharper disable RedundantAssignment
#pragma warning disable CS0162 // Unreachable code detected -> Ignored because of the const bool which can be set to true for testing.

namespace ExtraRandom.PerformanceTests;

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
[SuppressMessage(
    "Blocker Code Smell",
    "S2699:Tests should include assertions",
    Justification = "These are performance tests."
)]
public class PRNGTest : ResultOutputHelper
{
    private const int Loops = 200;

    /// <summary>
    /// Determines if the test should keep track of how many times a single number has been generated.
    /// </summary>
    /// <remarks>
    /// <para>If this is set to <see langword="true"/>, it will impact performance quite a bit.</para>
    /// </remarks>
    private const bool _recordAmountOfGeneratedNumbers = true;

    public PRNGTest(ITestOutputHelper output)
        : base(output, _recordAmountOfGeneratedNumbers) { }

    [Theory]
    [ClassData(typeof(PRNGRandoms))]
    public void Bool(IRandom rand)
    {
        var generateHits = new SortedDictionary<bool, int>();

        for (var i = 0; i < Loops; i++)
        {
            var @bool = rand.NextBoolean();

            RecordHit(@bool, ref generateHits);
        }

        PrintHits(ref generateHits);
    }

    [Theory]
    [ClassData(typeof(PRNGRandoms))]
    public void ByteRange(IRandom rand)
    {
        var generateHits = new SortedDictionary<byte, int>();

        const byte min = byte.MinValue;
        const byte max = byte.MaxValue;

        for (var i = 0; i < Loops; i++)
        {
            var @byte = rand.NextByte(min, max);
            Assert.InRange(@byte, min, max);

            RecordHit(@byte, ref generateHits);
        }

        PrintHits(ref generateHits);
    }

    [Theory]
    [ClassData(typeof(PRNGRandoms))]
    public void IntRange(IRandom rand)
    {
        var generateHits = new SortedDictionary<int, int>();

        const int min = 0;
        const int max = 21475;

        for (var i = 0; i < Loops; i++)
        {
            var @int = rand.NextInt(min, max);
            Assert.InRange(@int, min, max);

            RecordHit(@int, ref generateHits);
        }

        PrintHits(ref generateHits);
    }

    [Theory]
    [ClassData(typeof(PRNGRandoms))]
    public void UIntRange(IRandom rand)
    {
        var generateHits = new SortedDictionary<uint, int>();

        const uint min = uint.MinValue;
        const uint max = uint.MaxValue;

        for (var i = 0; i < Loops; i++)
        {
            var @uint = rand.NextUInt(min, max);
            Assert.InRange(@uint, min, max);

            RecordHit(@uint, ref generateHits);
        }

        PrintHits(ref generateHits);
    }

    [Theory]
    [ClassData(typeof(PRNGRandoms))]
    public void LongRange(IRandom rand)
    {
        var generateHits = new SortedDictionary<long, int>();

        const long min = 0;
        const long max = long.MaxValue;

        for (var i = 0; i < Loops; i++)
        {
            var @long = rand.NextLong(min, max);
            Assert.InRange(@long, min, max);

            RecordHit(@long, ref generateHits);
        }

        PrintHits(ref generateHits);
    }

    [Theory]
    [ClassData(typeof(PRNGRandoms))]
    public void ULongRange(IRandom rand)
    {
        var generateHits = new SortedDictionary<ulong, int>();

        const ulong min = 0;
        const ulong max = ulong.MaxValue;

        for (var i = 0; i < Loops; i++)
        {
            var @ulong = rand.NextULong(min, max);
            Assert.InRange(@ulong, min, max);

            RecordHit(@ulong, ref generateHits);
        }

        PrintHits(ref generateHits);
    }

    [Theory]
    [ClassData(typeof(PRNGRandoms))]
    public void DoubleRange(IRandom rand)
    {
        var generateHits = new SortedDictionary<double, int>();

        const double min = -1.0;
        const double max = 3.5;

        for (var i = 0; i < Loops; i++)
        {
            var @double = rand.NextDouble(min, max);
            Assert.InRange(@double, min, max);

            RecordHit(@double, ref generateHits);
        }

        PrintHits(ref generateHits);
    }
}
