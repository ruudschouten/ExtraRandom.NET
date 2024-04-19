using ExtraSort;

namespace ExtraShuffle;

public static class FaroShuffleExtension
{
    public static void FaroShuffle<T>(this IList<T> list, int min = 0, int max = int.MinValue)
    {
        if (max == int.MinValue) max = list.Count;

        var mid = (min + max) / 2;
        var firstHalf = list.Slice(min, mid - 1).ToArray();
        var secondHalf = list.Slice(mid, max).ToArray();
        var totalSize = firstHalf.Length + secondHalf.Length;

        for (var i = 0; i < totalSize; i++)
        {
            var currentHalf = i % 2 == 0 ? firstHalf : secondHalf;
            if (i / 2 < currentHalf.Length)
            {
                list[i] = currentHalf[i / 2];
            }
        }
    }
}