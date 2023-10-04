using ExtraRandom.PRNG;
using Xunit.Abstractions;

namespace ExtraRandom.UnitTests;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1600:Elements should be documented",
    Justification = "These are tests"
)]
public class PrngTest
{
    private const int Loops = 1_000_000;

    [Fact]
    public void XoroshiroIntRange()
    {
        const int minInt = 0;
        const int maxInt = 21475;

        var rand = new Xoroshiro128Plus(500, 21);
        for (var i = 0; i < Loops; i++)
        {
            var @int = rand.NextInt(minInt, maxInt);
            Assert.InRange(@int, minInt, maxInt);
        }
    }

    [Fact]
    public void XoroshiroLongRange()
    {
        const long minLong = 0;
        const long maxLong = long.MaxValue;

        var rand = new Xoroshiro128Plus(500, 21);
        for (var i = 0; i < Loops; i++)
        {
            var @long = rand.NextLong(minLong, maxLong);
            Assert.InRange(@long, minLong, maxLong);
        }
    }

    [Fact]
    public void XoroshiroDoubleRange()
    {
        const double minDouble = -1.0;
        const double maxDouble = 3.5;

        var rand = new Xoroshiro128Plus(500, 21);
        for (var i = 0; i < Loops; i++)
        {
            var @double = rand.NextDouble(minDouble, maxDouble);
            Assert.InRange(@double, minDouble, maxDouble);
        }
    }
}
