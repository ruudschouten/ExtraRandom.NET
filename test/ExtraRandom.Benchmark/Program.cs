using BenchmarkDotNet.Running;

namespace ExtraRandom.Benchmark;

public static class Program
{
    public static void Main(string[] _)
    {
        BenchmarkRunner.Run<PRNGBenchmark>();
    }
}
