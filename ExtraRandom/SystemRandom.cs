namespace ExtraRandom;

public class SystemRandom : Random64
{
    private System.Random _random;

    public SystemRandom(int seed)
    {
        _random = new System.Random(seed);
    }

    /// <inheritdoc/>
    public override void Reseed()
    {
        var seed = _random.Next();
        _random = new System.Random(seed);
    }

    /// <inheritdoc/>
    protected override ulong Next()
    {
        return (ulong)_random.NextInt64();
    }
}