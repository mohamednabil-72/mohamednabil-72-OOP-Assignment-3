using System.Diagnostics;

namespace RefactoringLab.Part03.BlockedUsers;

public static class BlockedUserChecker
{
    // blockedIds is a HashSet: Contains is O(1) on average (List.Contains is O(n)),
    // so the whole method is O(m) for m request ids instead of O(n * m).
    public static int CountBlocked(HashSet<int> blockedIds, int[] requestIds)
    {
        var blocked = 0;
        foreach (var id in requestIds)
        {
            if (blockedIds.Contains(id))
                blocked++;
        }
        return blocked;
    }

    public static HashSet<int> BuildBlockedIds(int count)
    {
        var set = new HashSet<int>(count);
        for (var i = 0; i < count; i++)
            set.Add(i);
        return set;
    }

    public static int[] BuildRequestIds(int count, int maxId, int seed = 42)
    {
        var rnd = new Random(seed);
        var ids = new int[count];
        for (var i = 0; i < count; i++)
            ids[i] = rnd.Next(maxId);
        return ids;
    }

    public static long MeasureMs(HashSet<int> blockedIds, int[] requestIds, out int found)
    {
        var sw = Stopwatch.StartNew();
        found = CountBlocked(blockedIds, requestIds);
        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
}
