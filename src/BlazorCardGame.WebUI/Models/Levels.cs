public static class Levels
{
    public static readonly Dictionary<int, long> baseAnteLevels = new()
    {
        { 1, 300 },
        { 2, 800 },
        { 3, 2800 },
        { 4, 6000 },
        { 5, 11000 },
        { 6, 20000 },
        { 7, 35000 },
        { 8, 50000 },
        { 9, 110000 },
        { 10, 560000 },
        { 11, 7200000 },
        { 12, 300000000 },
        { 13, 47000000000 },
    };

    public static readonly float BIG_BLIND_FACTOR = 1.5F;
}