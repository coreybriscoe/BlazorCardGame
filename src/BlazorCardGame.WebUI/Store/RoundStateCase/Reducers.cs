namespace BlazorCardGame.WebUI.Store.RoundStateCase;

public static class Reducers
{
    [ReducerMethod]
    public static RoundState ReduceDecrementHandsRemainingAction(RoundState state, DecrementHandsRemainingAction action)
    {
        return new RoundState(handsRemaining: state.HandsRemaining - action.Amount, discardsRemaining: state.DiscardsRemaining, roundScore: state.RoundScore);
    }

    [ReducerMethod]
    public static RoundState ReduceSetHandsRemainingAction(RoundState state, DecrementHandsRemainingAction action)
    {
        return new RoundState(handsRemaining: action.Amount, discardsRemaining: state.DiscardsRemaining, roundScore: state.RoundScore);
    }

    [ReducerMethod]
    public static RoundState ReduceDecrementDiscardsRemainingAction(RoundState state, DecrementDiscardsRemainingAction action)
    {
        return new RoundState(handsRemaining: state.HandsRemaining, discardsRemaining: state.DiscardsRemaining - action.Amount, roundScore: state.RoundScore);
    }

    [ReducerMethod]
    public static RoundState ReduceSetDiscardsRemainingAction(RoundState state, DecrementDiscardsRemainingAction action)
    {
        return new RoundState(handsRemaining: state.HandsRemaining, discardsRemaining: action.Amount, roundScore: state.RoundScore);
    }

    [ReducerMethod]
    public static RoundState ReduceAddRoundScoreAction(RoundState state, AddRoundScoreAction action)
    {
        return new RoundState(handsRemaining: state.HandsRemaining, discardsRemaining: state.DiscardsRemaining, roundScore: state.RoundScore + action.Amount);
    }

    [ReducerMethod]
    public static RoundState ReduceSetRoundScoreAction(RoundState state, SetRoundScoreAction action)
    {
        return new RoundState(handsRemaining: state.HandsRemaining, discardsRemaining: state.DiscardsRemaining, roundScore: action.Amount);
    }
}