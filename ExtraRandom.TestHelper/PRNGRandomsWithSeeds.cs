﻿using Xunit;

namespace ExtraRandom.TestHelper;

public class PRNGRandomsWithSeeds : TheoryData<IRandom, long>
{
    private readonly long[] _seeds = { 500L, 250, 125, 70, 5 };

    public PRNGRandomsWithSeeds()
    {
        foreach (var data in new PRNGRandoms())
        {
            var prng = data[0] as IRandom ?? throw new InvalidOperationException();

            foreach (var seed in _seeds)
            {
                Add(prng, seed);
            }
        }
    }
}
