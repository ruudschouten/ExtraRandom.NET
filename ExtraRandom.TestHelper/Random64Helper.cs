using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ExtraRandom.TestHelper;

public static class Random64Helper
{
    [SuppressMessage(
        "ReflectionAnalyzers.SystemReflection",
        "REFL008:Specify binding flags for better performance and less fragile code",
        Justification = "With the bindings this linter suggests, the State field is not found."
    )]
    public static ulong[] GetStateCopy(this Random64 random)
    {
        var state =
            random
                .GetType()
                // With the bindings this linter suggests, the State field is not found.
                .GetField("State", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.GetValue(random) as ulong[];
        return (state ?? []).ToArray();
    }
}
