using BlazorCardGame.Domain.Models;
[FeatureState]
public class RunState
{
    public int Ante { get; } = 1;
    public int Round { get; } = 1;
    public Levels.Phase Phase { get; } = Levels.Phase.SMALL_BLIND;
    public int Cash { get; } = 0;

    public RunState() { }
    public RunState(int ante, int round, Levels.Phase phase, int cash)
    {
        Ante = ante;
        Round = round;
        Phase = phase;
        Cash = cash;
    }
}