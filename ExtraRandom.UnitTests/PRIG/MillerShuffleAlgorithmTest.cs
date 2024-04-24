using ExtraRandom.PRIG;
using FluentAssertions;

namespace ExtraRandom.UnitTests.PRIG;

public class MillerShuffleAlgorithmTest
{
    [Theory]
    [InlineData(MillerShuffleVariant.MS_Lite)]
    [InlineData(MillerShuffleVariant.MS_XLite)]
    [InlineData(MillerShuffleVariant.MSA_d)]
    [InlineData(MillerShuffleVariant.MSA_e)]
    [InlineData(MillerShuffleVariant.MSA_b)]
    public void MillerShuffle_ShouldAlwaysGiveAUniqueIndex_WhenCalledInALoop(
        MillerShuffleVariant variant
    )
    {
        const int seed = 500;
        const int itemCount = 9000;
        var indices = new List<int>();

        for (var i = 0; i < itemCount; i++)
        {
            var index = MillerShuffleAlgorithm.MillerShuffle(i, seed, itemCount, variant);
            indices.Add((int)index);
        }

        var uniqueIndices = indices.GroupBy(x => x).ToDictionary(x => x.Key, y => y.Count());
        var duplicateIndices = uniqueIndices.Where(x => x.Value > 1).ToList();
        uniqueIndices.Count.Should().Be(itemCount);
        duplicateIndices.Count.Should().Be(0);
    }
}
