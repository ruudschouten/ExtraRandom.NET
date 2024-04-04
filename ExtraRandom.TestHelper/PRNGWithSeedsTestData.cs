using Xunit;

namespace ExtraRandom.TestHelper;

public class PRNGWithSeedsTestData : TheoryData<IRandom, long>
{
    public PRNGWithSeedsTestData()
    {
        foreach (var data in new PRNGTestData())
        {
            var prng = data[0] as IRandom 
                       ?? throw new InvalidOperationException();

            Add(prng, 500L);
            Add(prng, 1000L);
            Add(prng, 250L);
            Add(prng, 24L);
            Add(prng, 48L);
        }
    }
}
