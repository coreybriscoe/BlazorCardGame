namespace BlazorCardGame.WebUI.Store.RunStateCase;

public static class Reducers
{
    [ReducerMethod]
    public static RunState ReduceAddCashAction(RunState state, AddCashAction action)
    {
        return new RunState(cash: state.Cash + action.Amount, ante: state.Ante, round: state.Round, phase: state.Phase);
    }

    [ReducerMethod]
    public static RunState ReduceIncrementAnteAction(RunState state, IncrementAnteAction action)
    {
        return new RunState(cash: state.Cash, ante: state.Ante + action.Amount, round: state.Round, phase: state.Phase);
    }

    [ReducerMethod]
    public static RunState ReduceIncrementRoundAction(RunState state, IncrementRoundAction action)
    {
        return new RunState(cash: state.Cash, ante: state.Ante, round: state.Round + action.Amount, phase: state.Phase);
    }

    [ReducerMethod]
    public static RunState ReduceSetPhaseAction(RunState state, SetPhaseAction action)
    {
        return new RunState(cash: state.Cash, ante: state.Ante, round: state.Round, phase: action.Phase);
    }
}