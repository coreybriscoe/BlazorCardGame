namespace BlazorCardGame.WebUI.Store.CardsStateCase;

public static class Reducers
{
    [ReducerMethod]
    public static CardsState ReduceSetHandCardsAction(CardsState state, SetHandCardsAction action)
    {
        return new CardsState(handCards: action.HandCards);
    }
}