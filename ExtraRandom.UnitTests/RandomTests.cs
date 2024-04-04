using System.Numerics;
using ExtraRandom.TestHelper;
using FluentAssertions;

namespace ExtraRandom.UnitTests;

public class RandomTests
{
    #region Boolean
    [Theory]
    [ClassData(typeof(PRNGWithSeedsTestData))]
    public void GeneratingBoolean_IsTrueOrFalse(IRandom random, ulong seed)
    {
        // Arrange
        random.SetSeed(seed);

        // Act
        var result = random.NextBoolean();

        // Assert
        Assert.True(result || !result);
    }
    #endregion

    #region Byte

    [Theory]
    [ClassData(typeof(PRNGWithSeedsTestData))]
    public void GeneratingByte_IsInRange(IRandom random, ulong seed)
    {
        // Arrange
        random.SetSeed(seed);

        // Act
        var result = random.NextByte();

        // Assert
        result.Should().BeInRange(byte.MinValue, byte.MaxValue);
    }

    [Theory]
    [ClassData(typeof(PRNGWithSeedsTestData))]
    public void GeneratingByte_WithMinAndMax_IsInRange(IRandom random, ulong seed)
    {
        // Arrange
        random.SetSeed(seed);
        const byte min = 10;
        const byte max = 20;

        // Act
        var result = random.NextByte(min, max);

        // Assert
        Assert.InRange(result, min, max);
    }

    [Theory]
    [ClassData(typeof(PRNGWithSeedsTestData))]
    public void GeneratingByte_WhereMaxHitsExclusiveUpperBound_ReturnsLowerBound(
        IRandom random,
        ulong seed
    )
    {
        // Arrange
        random.SetSeed(seed);
        const byte min = 11;
        const byte max = 12;

        // Act
        var result = random.NextByte(min, max);

        // Assert
        result.Should().Be(min);
    }

    [Theory]
    [ClassData(typeof(PRNGWithSeedsTestData))]
    public void GeneratingByte_WhereMinAndMaxAreTheSameValue_ShouldReturnMin(
        IRandom random,
        ulong seed
    )
    {
        // Arrange
        random.SetSeed(seed);
        const byte min = 10;
        const byte max = 10;

        // Act
        var result = random.NextByte(min, max);

        // Assert
        result.Should().Be(min);
    }

    [Theory]
    [ClassData(typeof(PRNGWithSeedsTestData))]
    public void GeneratingByte_WhereMinIsHigherThanMax_ShouldThrowException(
        IRandom random,
        ulong seed
    )
    {
        // Arrange
        random.SetSeed(seed);
        const byte min = 20;
        const byte max = 10;

        // Act
        var call = () => random.NextByte(min, max);

        // Assert
        call.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage("Min value must be smaller than or equal to max value. (Parameter 'min')");
    }

    #endregion

    #region Int

    [Theory]
    [ClassData(typeof(PRNGWithSeedsTestData))]
    public void GeneratingInt_IsInRange(IRandom random, ulong seed)
    {
        // Arrange
        random.SetSeed(seed);

        // Act
        var result = random.NextInt();

        // Assert
        result.Should().BeInRange(int.MinValue, int.MaxValue);
    }

    [Theory]
    [ClassData(typeof(PRNGWithSeedsTestData))]
    public void GeneratingInt_WithMinAndMax_IsInRange(IRandom random, ulong seed)
    {
        // Arrange
        random.SetSeed(seed);
        const int min = 10;
        const int max = 20;

        // Act
        var result = random.NextInt(min, max);

        // Assert
        result.Should().BeInRange(min, max);
    }

    [Theory]
    [ClassData(typeof(PRNGWithSeedsTestData))]
    public void GeneratingInt_WhereMaxHitsExclusiveUpperBound_ReturnsLowerBound(
        IRandom random,
        ulong seed
    )
    {
        // Arrange
        random.SetSeed(seed);
        const int min = 11;
        const int max = 12;

        // Act
        var result = random.NextInt(min, max);

        // Assert
        result.Should().Be(min);
    }

    [Theory]
    [ClassData(typeof(PRNGWithSeedsTestData))]
    public void GeneratingInt_WhereMinAndMaxAreTheSameValue_ShouldReturnMin(
        IRandom random,
        ulong seed
    )
    {
        // Arrange
        random.SetSeed(seed);
        const int min = 10;
        const int max = 10;

        // Act
        var result = random.NextInt(min, max);

        // Assert
        result.Should().Be(min);
    }

    [Theory]
    [ClassData(typeof(PRNGWithSeedsTestData))]
    public void GeneratingInt_WhereRangeIsNegative_ShouldReturnNegativeNumber(
        IRandom random,
        ulong seed
    )
    {
        // Arrange
        random.SetSeed(seed);
        const int min = -10;
        const int max = -1;

        // Act
        var result = random.NextInt(min, max);

        // Assert
        result.Should().BeNegative();
        result.Should().BeInRange(min, max);
    }

    [Theory]
    [ClassData(typeof(PRNGWithSeedsTestData))]
    public void GeneratingInt_WhereMinIsNegative_ShouldReturnNumber(IRandom random, ulong seed)
    {
        // Arrange
        random.SetSeed(seed);
        const int min = -10;
        const int max = 10;

        // Act
        var result = random.NextInt(min, max);

        // Assert
        result.Should().BeInRange(min, max);
    }

    [Theory]
    [ClassData(typeof(PRNGWithSeedsTestData))]
    public void GeneratingInt_WhereMinIsHigherThanMax_ShouldThrowException(
        IRandom random,
        ulong seed
    )
    {
        // Arrange
        random.SetSeed(seed);
        const int min = 20;
        const int max = 10;

        // Act
        var call = () => random.NextInt(min, max);

        // Assert
        call.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage("Min value must be smaller than or equal to max value. (Parameter 'min')");
    }

    #endregion
}
