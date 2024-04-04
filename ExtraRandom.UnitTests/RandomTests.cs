using ExtraRandom.TestHelper;
using FluentAssertions;

namespace ExtraRandom.UnitTests;

public class RandomTests
{
    [Theory]
    [ClassData(typeof(PRNGWithSeedsTestData))]
    public void Generating_NextBoolean_IsTrueOrFalse(IRandom random, ulong seed)
    {
        // Arrange
        random.SetSeed(seed);

        // Act
        var result = random.NextBoolean();

        // Assert
        Assert.True(result || !result);
    }
}
