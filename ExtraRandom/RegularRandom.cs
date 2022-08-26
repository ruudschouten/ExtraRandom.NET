namespace ExtraRandom;

public class RegularRandom : BaseRandom
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegularRandom"/> class with a random seed.
    /// </summary>
    public RegularRandom()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RegularRandom"/> class with the given <paramref name="seed"/>
    /// </summary>
    /// <param name="seed">The seed that should be used to generate random numbers.</param>
    public RegularRandom(int seed)
        : base(seed)
    {
    }

    /// <inheritdoc/>
    public override int NextInt(int min, int max)
    {
        // TODO: Do something with the bits, like https://github.com/Shiroechi/Litdex.Random/blob/main/Source/Random.cs#L227
        return (int)((ShisuaRng.Next() * (ulong)(max - min)) + (ulong)min);
    }

    /// <inheritdoc/>
    public override float NextFloat(float min, float max)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public override long NextLong(long min, long max)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public override double NextDouble(double min, double max)
    {
        throw new NotImplementedException();
    }
}