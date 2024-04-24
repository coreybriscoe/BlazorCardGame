public class SetPhaseAction
{
    public Levels.Phase Phase { get; }
    public SetPhaseAction() { }
    public SetPhaseAction(Levels.Phase phase)
    {
        Phase = phase;
    }
}