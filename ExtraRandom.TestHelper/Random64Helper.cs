using System.Reflection;

namespace ExtraRandom.TestHelper;

public static class Random64Helper
{
    public static ulong[] GetStateCopy(this Random64 random)
    {
        var state =
            random
                .GetType()
                .GetField("State", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.GetValue(random) as ulong[];
        return (state ?? []).ToArray();
    }
}
