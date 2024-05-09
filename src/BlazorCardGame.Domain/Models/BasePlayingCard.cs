namespace BlazorCardGame.Domain.Models
{
    public abstract class BasePlayingCard(int rank, char suit, string rankString = ".", bool isFaceUp = true, bool isSelected = false, bool isSelectable = false) : IPlayingCard
    {
        protected int Rank { get; set; } = rank;
        protected char Suit { get; set; } = suit;
        protected string RankString { get; set; } = rankString.Equals(".") ? rank.ToString() : rankString;
        protected bool IsFaceUp { get; set; } = isFaceUp;
        protected bool IsSelected { get; set; } = isSelected;
        protected bool IsSelectable { get; set; } = isSelectable;

        public string GetRankString()
        {
            return RankString;
        }

        public int GetRank()
        {
            return Rank;
        }

        public int GetPoints()
        {
            if (Rank < 11) return Rank;
            if (Rank == 14) return 11;
            return 10;
        }

        public char GetSuit()
        {
            return Suit;
        }

        public override string ToString()
        {
            return $"{GetRankString()} of {GetSuit()}";
        }

        // Explicit interface member implementation to avoid property name conflict
        bool ICard.IsFaceUp()
        {
            return IsFaceUp;
        }

        void ICard.FlipCard()
        {
            IsFaceUp = !IsFaceUp;
        }

        bool IPlayingCard.IsSelected()
        {
            return IsSelected;
        }

        bool IPlayingCard.IsSelectable()
        {
            return IsSelectable;
        }

        public void ToggleSelect()
        {
            if (IsSelectable)
            {
                IsSelected = !IsSelected;
            }
        }
    }
}