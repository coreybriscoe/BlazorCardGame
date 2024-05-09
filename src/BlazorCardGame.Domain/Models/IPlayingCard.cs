namespace BlazorCardGame.Domain.Models
{
    public interface IPlayingCard : ICard
    {
        int GetRank();
        bool IsSelected();
        bool IsSelectable();
    }
}