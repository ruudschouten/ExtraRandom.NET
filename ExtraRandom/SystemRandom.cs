namespace ExtraRandom;

public class SystemRandom : Random64
{
    private System.Random _random;

    public SystemRandom(int seed)
    {
        State = new ulong[1];
        _random = new System.Random(seed);
    }

    public override void SetSeed(params ulong[] seed)
    {
        _random = new System.Random((int)seed[0]);
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
