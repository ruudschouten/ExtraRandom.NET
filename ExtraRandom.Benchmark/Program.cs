using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ExtraRandom.PRNG;

namespace ExtraRandom.Benchmark;

public class PRNGBenchmark
{
    private const int Seed = 500;

    // PRNGs
    private readonly SystemRandom _systemRandom = new(Seed);
    private readonly CryptoRandom _cryptoRandom = new();
    private readonly MiddleSquareWeylSequence _middleSquareWeylSequence = new(Seed);
    private readonly RomuDuo _romuDuo = new(Seed);
    private readonly RomuDuoJr _romuDuoJr = new(Seed);
    private readonly RomuTrio _romuTrio = new(Seed);
    private readonly Seiran _seiran = new(Seed);
    private readonly Xoroshiro128Plus _xoroshiro128Plus = new(Seed);
    private readonly Xoroshiro128PlusPlus _xoroshiro128PlusPlus = new(Seed);
    private readonly Xoroshiro128StarStar _xoroshiro128StarStar = new(Seed);


    [Benchmark]
    public long SystemRandom() => _systemRandom.NextLong();

    [Benchmark]
    public long CryptoRandom() => _cryptoRandom.NextLong();

    [Benchmark]
    public long MiddleSquareWeylSequence() => _middleSquareWeylSequence.NextLong();

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

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<PRNGBenchmark>();
    }
}