using ExtraRandom.PRNG;
using ExtraRandom.Specialised;
using Xunit.Abstractions;

namespace ExtraRandom.UnitTests;

public class NewWeightedRandomTest
{
    private readonly ITestOutputHelper _output;

    public NewWeightedRandomTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void WeightedTest()
    {
        var weightedRandom = new NewWeightedRandom<string>(new Shishua(500));
        weightedRandom.Add("1", 1);
        weightedRandom.Add("10", 10);
        weightedRandom.Add("50", 50);
        weightedRandom.Add("5", 5);
        weightedRandom.Add("25", 25);
        weightedRandom.Add("125", 125);
        weightedRandom.Add("12500", 12500);
        weightedRandom.Add("0", 0);
        weightedRandom.Add("17", 17);

        var s = "";
    }
}
