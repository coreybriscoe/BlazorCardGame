using static PokerLogic;

public static class HandScores
{
    private static readonly Dictionary<HandCategory, int> handBasePoints = new()
    {
        { HandCategory.NoCategory, 0 },
        { HandCategory.HighCard, 5 },
        { HandCategory.OnePair, 10 },
        { HandCategory.TwoPair, 20 },
        { HandCategory.ThreeOfAKind, 30 },
        { HandCategory.Straight, 30 },
        { HandCategory.Flush, 35 },
        { HandCategory.FullHouse, 40 },
        { HandCategory.FourOfAKind, 60 },
        { HandCategory.StraightFlush, 100 },
        { HandCategory.FiveOfAKind, 120 },
        { HandCategory.FlushHouse, 140 },
        { HandCategory.FlushFive, 160 },
    };

    private static readonly Dictionary<HandCategory, int> handBaseMultiplier = new()
    {
        { HandCategory.NoCategory, 0 },
        { HandCategory.HighCard, 1 },
        { HandCategory.OnePair, 2 },
        { HandCategory.TwoPair, 2 },
        { HandCategory.ThreeOfAKind, 3 },
        { HandCategory.Straight, 4 },
        { HandCategory.Flush, 4 },
        { HandCategory.FullHouse, 4 },
        { HandCategory.FourOfAKind, 7 },
        { HandCategory.StraightFlush, 8 },
        { HandCategory.FiveOfAKind, 12 },
        { HandCategory.FlushHouse, 14 },
        { HandCategory.FlushFive, 16 },
    };
}