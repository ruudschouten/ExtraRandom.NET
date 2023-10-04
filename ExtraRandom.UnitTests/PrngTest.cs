namespace ExtraRandom.UnitTests;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1600:Elements should be documented",
    Justification = "These are tests"
)]
public class PrngTest
{
    [Fact]
    public void XoroshiroStabilityTest()
    {
        const int loops = 1_000;

        const int minInt = 1;
        const int maxInt = 100;

        const float minFloat = 10f;
        const float maxFloat = 20f;

        const double minDouble = 1.0;
        const double maxDouble = 3.5;

        const long minLong = 2_000L;
        const long maxLong = 10_000L;

        for (var i = 0; i < loops; i++)
        {
            var rand = new XoroshiroRandom(i);

            var @int = rand.NextInt(minInt, maxInt);
            var @float = rand.NextFloat(minFloat, maxFloat);
            var @double = rand.NextDouble(minDouble, maxDouble);
            var @long = rand.NextLong(minLong, maxLong);

            Assert.InRange(@int, minInt, maxInt);
            Assert.InRange(@float, minFloat, maxFloat);
            Assert.InRange(@double, minDouble, maxDouble);
            Assert.InRange(@long, minLong, maxLong);
        }
    }
}
