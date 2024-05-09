using BlazorCardGame.Domain.Models;
public class SetHandCardsAction
{
    public List<BasePlayingCard> HandCards { get; } = [];
    public SetHandCardsAction() { }
    public SetHandCardsAction(List<BasePlayingCard> handCards)
    {
        HandCards = handCards;
    }
}