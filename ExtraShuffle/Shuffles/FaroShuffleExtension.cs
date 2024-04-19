using ExtraSort;

namespace ExtraShuffle.Shuffles;

public static class FaroShuffleExtension
{
    public static void FaroShuffle<T>(this IList<T> list, int min, int max)
    {
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

    public static void FaroShuffle<T>(this IList<T> list)
    {
        list.FaroShuffle(0, list.Count - 1);
    }
}