# ExtraRandom

[![NuGet Version](https://img.shields.io/nuget/v/ExtraRandom?label=ExtraRandom)](https://www.nuget.org/packages/ExtraRandom)
[![NuGet Version](https://img.shields.io/nuget/v/ExtraRandom.ExtraShuffle?label=ExtraShuffle)](https://www.nuget.org/packages/ExtraRandom.ExtraShuffle/)
[![NuGet Version](https://img.shields.io/nuget/v/ExtraRandom.ExtraSort?label=ExtraSort)](https://www.nuget.org/packages/ExtraRandom.ExtraSort/)

This is a library which adds a few random-related functionalities.
These are divided across several projects.

## ExtraRandom

This project contains the main random number generators;

- Psuedo random number generators
  - [Romu](https://arxiv.org/pdf/2002.11331)
  - [Seiran](https://github.com/andanteyk/prng-seiran/blob/master/seiran128.c)
  - [Xoroshiro](https://prng.di.unimi.it)
- Psuedo random index generators
  - [MillerShuffle](https://github.com/RondeSC/Miller_Shuffle_Algo)
- Specialised random number generators
  - BiasedRandom 
    - Rolls multiple times for a single result and picks the value that was closest to the set bias.
  - Weighted
    - Assigns weights to all entries, which influence the likeliness of the entry being chosen when rolling.

## ExtraShuffle

This project contains shuffle algorithms which are added as extension methods to `IList<T>`.

- [Balanced Shuffle](https://keyj.emphy.de/balanced-shuffle/)
- [Faro Shuffle](https://en.wikipedia.org/wiki/Faro_shuffle)
- [Fibonacci Hashing Shuffle](https://pncnmnp.github.io/blogs/fibonacci-hashing.html)
- [Fisher-Yates Shuffle](https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle)
- [MillerShuffle](https://github.com/RondeSC/Miller_Shuffle_Algo)

There are two ways to call these;
1. Using a for or foreach loop with the iterator method; `list.FaroShuffleIterator(min, max)`
2. Calling the method directly; `list.FaroShuffle()`

## ExtraSort

This project was added because the `WeightedRandom` required a sorted list of weights, and I thought it would be fun to add a few sorting algorithms it could use instead of the `OrderBy` LINQ method.

The sorts added now are;
- Binary Insertion Sort
- Insertion Sort
- Merge Sort
- Tim Sort
- Power Sort
- Peek Sort

These are added using a `Sort` extension method for `IList<T>`, in which a instance of `ISortAlgorithm` must be passed.
This instance is then used to sort the current list.
