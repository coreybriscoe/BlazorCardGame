namespace BlazorCardGame.Domain.Models
{
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
            FiveOfAKind,
            FlushHouse,
            FlushFive,
        }

        public static readonly Dictionary<HandCategory, string> HandCategoryNames = new()
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
            { HandCategory.FiveOfAKind, "Five of a Kind" },
            { HandCategory.FlushHouse, "Flush House" },
            { HandCategory.FlushFive, "Flush Five" },
        };

        public static string GetHandCategoryName<T>(List<T> cards) where T : IPlayingCard
        {
            HandCategory category = GetHandCategory(cards);
            return HandCategoryNames.GetValueOrDefault(category) ?? "?";
        }

        public static bool IsFlush<T>(List<T> cards) where T : IPlayingCard
        {
            var suit = cards[0].GetSuit();
            return cards.Count == 5 && cards.All(c => c.GetSuit() == suit);
        }

        public static bool IsStraight<T>(List<T> cards) where T : IPlayingCard
        {
            var ranks = cards.Select(c => c.GetRank()).OrderBy(r => r).ToList();
            var firstRank = ranks[0];
            if (ranks.Count != 5) return false;
            return ranks.SequenceEqual(Enumerable.Range(firstRank, 5)) || (ranks.GetRange(0, 4).SequenceEqual(Enumerable.Range(firstRank, 4)) && firstRank == 2 && ranks[4] == 14);
        }

        public static bool IsStraightFlush<T>(List<T> cards) where T : IPlayingCard
        {
            return IsStraight(cards) && IsFlush(cards);
        }

        public static bool IsFiveOfAKind<T>(List<T> cards) where T : IPlayingCard
        {
            return cards.GroupBy(c => c.GetRank()).Any(g => g.Count() == 5);
        }

        public static bool IsFlushHouse<T>(List<T> cards) where T : IPlayingCard
        {
            return IsFlush(cards) && IsFullHouse(cards);
        }

        public static bool IsFlushFive<T>(List<T> cards) where T : IPlayingCard
        {
            return IsFlush(cards) && IsFiveOfAKind(cards);
        }

        public static bool IsFourOfAKind<T>(List<T> cards) where T : IPlayingCard
        {
            return cards.GroupBy(c => c.GetRank()).Any(g => g.Count() == 4);
        }

        public static bool IsFullHouse<T>(List<T> cards) where T : IPlayingCard
        {
            return IsThreeOfAKind(cards) && IsOnePair(cards);
        }

        public static bool IsThreeOfAKind<T>(List<T> cards) where T : IPlayingCard
        {
            return cards.GroupBy(c => c.GetRank()).Any(g => g.Count() == 3);
        }

        public static bool IsTwoPair<T>(List<T> cards) where T : IPlayingCard
        {
            return cards.GroupBy(c => c.GetRank()).Count(g => g.Count() == 2) == 2;
        }

        public static bool IsOnePair<T>(List<T> cards) where T : IPlayingCard
        {
            return cards.GroupBy(c => c.GetRank()).Any(g => g.Count() == 2);
        }

        public static bool IsHighCard<T>(List<T> cards) where T : IPlayingCard
        {
            return true;
        }

        public static HandCategory GetHandCategory<T>(List<T> cards) where T : IPlayingCard
        {
            if (cards.Count == 0)
            {
                return HandCategory.NoCategory;
            }
            if (IsFlushFive(cards))
            {
                return HandCategory.FlushFive;
            }
            if (IsFlushHouse(cards))
            {
                return HandCategory.FlushHouse;
            }
            if (IsFiveOfAKind(cards))
            {
                return HandCategory.FiveOfAKind;
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
}