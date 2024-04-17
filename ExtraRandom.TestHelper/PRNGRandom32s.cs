using ExtraRandom.PRNG;
using Xunit;

namespace ExtraRandom.TestHelper;

public class PRNGRandom32s : TheoryData<Random32>
{
    public PRNGRandom32s()
    {
        Add(new MiddleSquareWeylSequence(500));
    }
}
