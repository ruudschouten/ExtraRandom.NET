using Xunit;

namespace ExtraRandom.TestHelper;

public class PRNGWithSeedsTestData : TheoryData<IRandom, long>
{
    private readonly long[] _seeds = { 500L };

    public PRNGWithSeedsTestData()
    {
        foreach (var data in new PRNGTestData())
        {
            var prng = data[0] as IRandom ?? throw new InvalidOperationException();

            foreach (var seed in _seeds)
            {
                Add(prng, seed);
            }
        }
    }
}
