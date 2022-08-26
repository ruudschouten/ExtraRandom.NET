using ExtraRandom;
using Xunit.Abstractions;

namespace ExtraRandomTester;

public class ShishuaTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ShishuaTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test1()
    {
        var rand = new RegularRandom(123);

        var result = rand.NextInt(0, 100);
        _testOutputHelper.WriteLine(result.ToString());
    }
}