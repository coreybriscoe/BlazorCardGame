namespace BlazorCardGame.Domain.Models
{
    public interface ICard
    {
        string GetRankString();
        char GetSuit();
        bool IsFaceUp();
        void FlipCard();
    }
}