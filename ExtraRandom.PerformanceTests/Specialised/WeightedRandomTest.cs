using ExtraRandom.Specialised;
using ExtraRandom.TestHelper;

namespace ExtraRandom.PerformanceTests.Specialised;

public class WeightedRandomTest
{
    [Theory]
    [ClassData(typeof(PRNGRandoms))]
#pragma warning disable S2699 // There is nothing to really test here, just need to see if it generates stuff.
    public void Weighted_Speed_Test(IRandom rand)
#pragma warning restore S2699
    {
        const int rolls = 1_000_000;

        var weightedRandom = new WeightedRandom<string>(rand);
        weightedRandom.Add("1", 1);
        weightedRandom.Add("10", 10);
        weightedRandom.Add("50", 50);
        weightedRandom.Add("5", 5);
        weightedRandom.Add("25", 25);
        weightedRandom.Add("125", 125);
        weightedRandom.Add("20", 20);
        weightedRandom.Add("45", 45);
        weightedRandom.Add("0", 0);
        weightedRandom.Add("17", 17);

        for (var i = 0; i < rolls; i++)
        {
            weightedRandom.Next();
        }
    }
}
