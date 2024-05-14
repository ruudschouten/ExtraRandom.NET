using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using ExtraRandom.PRNG;

namespace ExtraRandom.Benchmark;

[SuppressMessage(
    "ReSharper",
    "InconsistentNaming",
    Justification = "PRNG is an abbreviation, so the naming is fine."
)]
[SuppressMessage(
    "Minor Code Smell",
    "S101:Types should be named in PascalCase",
    Justification = "PRNG is an abbreviation, so the naming is fine."
)]
public class PRNGBenchmark
{
    private const int Seed = 500;

    // PRNGs
    private readonly RomuDuo _romuDuo = new(Seed);
    private readonly RomuDuoJr _romuDuoJr = new(Seed);
    private readonly RomuTrio _romuTrio = new(Seed);
    private readonly Seiran _seiran = new(Seed);
    private readonly Xoroshiro128Plus _xoroshiro128Plus = new(Seed);
    private readonly Xoroshiro128PlusPlus _xoroshiro128PlusPlus = new(Seed);
    private readonly Xoroshiro128StarStar _xoroshiro128StarStar = new(Seed);

    [Benchmark]
    public long SystemRandom() => new System.Random().NextInt64();

    [Benchmark]
    public long CryptoRandom()
    {
        using var generator = RandomNumberGenerator.Create();
        var buffer = new byte[sizeof(ulong)];
        generator.GetBytes(buffer);
        return BitConverter.ToInt64(buffer, 0);
    }

    [Benchmark]
    public long RomuDuo() => _romuDuo.NextLong();

    [Benchmark]
    public long RomuDuoJr() => _romuDuoJr.NextLong();

    [Benchmark]
    public long RomuTrio() => _romuTrio.NextLong();

    [Benchmark]
    public long Seiran() => _seiran.NextLong();

    [Benchmark]
    public long Xoroshiro128Plus() => _xoroshiro128Plus.NextLong();

    [Benchmark]
    public long Xoroshiro128PlusPlus() => _xoroshiro128PlusPlus.NextLong();

    [Benchmark]
    public long Xoroshiro128StarStar() => _xoroshiro128StarStar.NextLong();
}
