using ExtraRandom.PRNG;
using ExtraRandom.TestHelper;
using FluentAssertions;

namespace ExtraRandom.UnitTests;

public class Random64Tests
{
    [Theory]
    [ClassData(typeof(PRNGRandom64s))]
    public void SettingSeed_WithSeed_SetsSeed(Random64 random)
    {
        var originalState = random.GetStateCopy();
        const ulong seed = 123ul;

        random.SetSeed(seed);

        var state = random.GetStateCopy();
        state.Should().NotEqual(originalState);
    }

    [Theory]
    [ClassData(typeof(PRNGRandom64s))]
    public void SettingSeed_WithSeedArray_SetsSeed(Random64 random)
    {
        var originalState = random.GetStateCopy();
        var seed = new ulong[originalState.Length];
        for (var i = 0; i < originalState.Length; i++)
        {
            seed[i] = 123ul * (ulong)(i + 1);
        }

        random.SetSeed(seed);

        var state = random.GetStateCopy();
        state.Should().NotEqual(originalState);
        state.Should().Equal(seed);
    }

    [Theory]
    [ClassData(typeof(PRNGRandom64s))]
    public void SettingSeed_WithEmptySeedArray_ThrowsArgumentNullException(Random64 random)
    {
        var seed = Array.Empty<ulong>();

        var act = () => random.SetSeed(seed);

        act.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [ClassData(typeof(PRNGRandom64s))]
    public void SettingSeed_WithNullSeedArray_ThrowsArgumentNullException(Random64 random)
    {
        ulong[]? seed = null;

        var act = () => random.SetSeed(seed!);

        act.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [ClassData(typeof(PRNGRandom64s))]
    public void SettingSeed_WithShortSeedArray_ThrowsArgumentException(Random64 random)
    {
        // SplitMix64's state consists of a single ulong, so this would not throw an exception.
        if (random is SplitMix64)
        {
            return;
        }

        var seed = new[] { 123ul };

        var act = () => random.SetSeed(seed);

        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [ClassData(typeof(PRNGRandom64s))]
    public void SettingSeed_WithLongSeedArray_ThrowsArgumentException(Random64 random)
    {
        var seed = new[] { 123ul, 456ul, 789ul, 1234ul, 256ul };

        var act = () => random.SetSeed(seed);

        act.Should().Throw<ArgumentException>();
    }
}
