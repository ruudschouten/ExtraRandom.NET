namespace ExtraRandom.PRNG;

/// <summary>
/// <para>SplitMix64 PRNG implementation.</para>
/// </summary>
/// <remarks>
/// <para>Source: https://prng.di.unimi.it/splitmix64.c</para>
/// </remarks>
public sealed class SplitMix64 : Random64
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SplitMix64"/> class.
    /// </summary>
    /// <param name="seed">Seed to use for number generation. </param>
    public SplitMix64(ulong seed)
    {
        State = new ulong[1];
        SetSeed(seed);
    }

    /// <inheritdoc />
    protected override ulong Next()
    {
        var z = (State[0] += 0x9e3779b97f4a7c15);
        z = (z ^ (z >> 30)) * 0xbf58476d1ce4e5b9;
        z = (z ^ (z >> 27)) * 0x94d049bb133111eb;
        return z ^ (z >> 31);
    }
}
