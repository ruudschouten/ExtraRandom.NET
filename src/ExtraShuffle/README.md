## ExtraShuffle

Contains shuffle algorithms which are added as extension methods to `IList<T>`.

- [Balanced Shuffle](https://keyj.emphy.de/balanced-shuffle/)
- [Faro Shuffle](https://en.wikipedia.org/wiki/Faro_shuffle)
- [Fibonacci Hashing Shuffle](https://pncnmnp.github.io/blogs/fibonacci-hashing.html)
- [Fisher-Yates Shuffle](https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle)
- [MillerShuffle](https://github.com/RondeSC/Miller_Shuffle_Algo)

There are two ways to call these;
1. Using a for or foreach loop with the iterator method; `list.FaroShuffleIterator(min, max)`
2. Calling the method directly; `list.FaroShuffle()`