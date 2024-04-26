using BenchmarkDotNet.Running;

namespace ExtraShuffle.Benchmark;

public static class Program
{
    public static void Main(string[] _)
    {
        BenchmarkRunner.Run<ShuffleBenchmark>();
    }
}
