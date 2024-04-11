using ExtraRandom.PRNG;
using Xunit;

namespace ExtraRandom.TestHelper;

public class PRNGRandom64s : TheoryData<Random64>
{
    public PRNGRandom64s()
    {
        Add(new RomuDuo(500));
        Add(new RomuDuoJr(500));
        Add(new RomuTrio(500));
        Add(new Seiran(500));
        Add(new Xoroshiro128Plus(500));
        Add(new Xoroshiro128PlusPlus(500));
        Add(new Xoroshiro128StarStar(500));
    }
}