## ExtraRandom

Contains the main random number generators:

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
