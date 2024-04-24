﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ExtraRandom;
using ExtraRandom.PRNG;
using ExtraShuffle.TestHelper;

namespace ExtraShuffle.Benchmark;

public class ShuffleBenchmark
{
    private readonly List<Song> _list = Playlist.GetTestPlaylist();
    private readonly IRandom _random = new RomuTrio(500);

    [Benchmark]
    public void FisherYates() => _list.FischerYatesShuffle(_random);

    [Benchmark]
    public void FaroShuffle() => _list.FaroShuffle();

    [Benchmark]
    public void BalancedShuffle() =>
        _list.BalancedShuffle(_random, song => new { song.Genre, song.Artist.Name });

    [Benchmark]
    public void FibonacciHashingShuffle() =>
        _list.FibonacciHashingShuffle(_random, song => new { song.Genre, song.Artist.Name });

    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<ShuffleBenchmark>();
    }
}
