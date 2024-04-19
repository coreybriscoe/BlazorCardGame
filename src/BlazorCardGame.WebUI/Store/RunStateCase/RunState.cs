[FeatureState]
public class RunState
{
    public int Ante { get; }
    public int Round { get; }
    public int Cash { get; }

    public RunState() { }
    public RunState(int ante, int round, int cash)
    {
        Ante = ante;
        Round = round;
        Cash = cash;
    }
}