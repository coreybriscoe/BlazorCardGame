public static class PokerLogic
{
    public enum HandCategory
    {
        NoCategory,
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }

    private static readonly Dictionary<HandCategory, string> HandCategoryNames = new()
    {
        { HandCategory.NoCategory, "?" },
        { HandCategory.HighCard, "High Card" },
        { HandCategory.OnePair, "One Pair" },
        { HandCategory.TwoPair, "Two Pair" },
        { HandCategory.ThreeOfAKind, "Three of a Kind" },
        { HandCategory.Straight, "Straight" },
        { HandCategory.Flush, "Flush" },
        { HandCategory.FullHouse, "Full House" },
        { HandCategory.FourOfAKind, "Four of a Kind" },
        { HandCategory.StraightFlush, "Straight Flush" },
        { HandCategory.RoyalFlush, "Royal Flush" }
    };

    public static string GetHandCategoryNameForCards(List<IPlayingCard> cards)
    {
        HandCategory category = GetHandCategory(cards);
        return HandCategoryNames.GetValueOrDefault(category) ?? "?";
    }

    public static bool IsFlush(List<IPlayingCard> cards)
    {
        var suit = cards[0].GetSuit();
        return cards.Count == 5 && cards.All(c => c.GetSuit() == suit);
    }

    public static bool IsStraight(List<IPlayingCard> cards)
    {
        var ranks = cards.Select(c => c.GetRank()).OrderBy(r => r).ToList();
        var firstRank = ranks[0];
        return ranks.SequenceEqual(Enumerable.Range(firstRank, 5));
    }

    public static bool IsStraightFlush(List<IPlayingCard> cards)
    {
        return IsStraight(cards) && IsFlush(cards);
    }

    public static bool IsRoyalFlush(List<IPlayingCard> cards)
    {
        return IsStraightFlush(cards) && cards.Any(c => c.GetRank() == 14);
    }

    public static bool IsFourOfAKind(List<IPlayingCard> cards)
    {
        return cards.GroupBy(c => c.GetRank()).Any(g => g.Count() == 4);
    }

    public static bool IsFullHouse(List<IPlayingCard> cards)
    {
        return IsThreeOfAKind(cards) && IsOnePair(cards);
    }

    public static bool IsThreeOfAKind(List<IPlayingCard> cards)
    {
        return cards.GroupBy(c => c.GetRank()).Any(g => g.Count() == 3);
    }

    public static bool IsTwoPair(List<IPlayingCard> cards)
    {
        return cards.GroupBy(c => c.GetRank()).Count(g => g.Count() == 2) == 2;
    }

    public static bool IsOnePair(List<IPlayingCard> cards)
    {
        return cards.GroupBy(c => c.GetRank()).Any(g => g.Count() == 2);
    }

    public static bool IsHighCard(List<IPlayingCard> cards)
    {
        return true;
    }

    public static HandCategory GetHandCategory(List<IPlayingCard> cards)
    {
        if (cards.Count == 0)
        {
            return HandCategory.NoCategory;
        }
        if (IsRoyalFlush(cards))
        {
            return HandCategory.RoyalFlush;
        }
        if (IsStraightFlush(cards))
        {
            return HandCategory.StraightFlush;
        }
        if (IsFourOfAKind(cards))
        {
            return HandCategory.FourOfAKind;
        }
        if (IsFullHouse(cards))
        {
            return HandCategory.FullHouse;
        }
        if (IsFlush(cards))
        {
            return HandCategory.Flush;
        }
        if (IsStraight(cards))
        {
            return HandCategory.Straight;
        }
        if (IsThreeOfAKind(cards))
        {
            return HandCategory.ThreeOfAKind;
        }
        if (IsTwoPair(cards))
        {
            return HandCategory.TwoPair;
        }
        if (IsOnePair(cards))
        {
            return HandCategory.OnePair;
        }
        if (cards.Count > 0)
        {
            return HandCategory.HighCard;
        }
        return HandCategory.NoCategory;
    }
}