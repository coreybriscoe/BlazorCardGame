namespace BlazorCardGame.Domain.Models
{
    public class PlayingCard(int rank, char suit, string rankString = ".", bool isFaceUp = false, bool isSelected = false, bool isSelectable = false) : BasePlayingCard(rank, suit, rankString, isFaceUp, isSelected, isSelectable)
    {
    }
}