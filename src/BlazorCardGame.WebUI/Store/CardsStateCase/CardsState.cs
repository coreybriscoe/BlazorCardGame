[FeatureState]
public class CardsState
{
    public List<BasePlayingCard> HandCards { get; } = [];

    public CardsState() { }
    public CardsState(List<BasePlayingCard> handCards)
    {
        HandCards = handCards;
    }

    public enum MenuType
    {
        PLAY,
        PAYOUT,
        SHOP,
        BLIND,
        PACK,
        START,
    }
}