using System.Diagnostics.CodeAnalysis;
using ExtraRandom.UnitTests.Util;
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
    private const int Loops = 10_000;

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

    [Theory]
    [ClassData(typeof(PRNGTestData))]
#pragma warning disable S2699 // There is nothing to really test here, just need to see if it generates stuff.
    public void Bool(IRandom rand)
#pragma warning restore S2699
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
    [ClassData(typeof(PRNGTestData))]
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
    [ClassData(typeof(PRNGTestData))]
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
    [ClassData(typeof(PRNGTestData))]
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
    [ClassData(typeof(PRNGTestData))]
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
    [ClassData(typeof(PRNGTestData))]
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
    [ClassData(typeof(PRNGTestData))]
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

    private static void RecordHit<T>(T number, ref SortedDictionary<T, int> generateHits)
        where T : notnull
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
        where T : notnull
    {
        if (!_recordAmountOfGeneratedNumbers)
            return;

        foreach (var hits in generateHits)
        {
            _output.WriteLine($"{hits.Key}: {hits.Value}");
        }
    }
}
