[FeatureState]
public class RoundState
{
    public int HandsRemaining { get; }
    public int DiscardsRemaining { get; }
    public int RoundScore { get; }

    public RoundState() { }
    public RoundState(int handsRemaining, int discardsRemaining, int roundScore)
    {
        HandsRemaining = handsRemaining;
        DiscardsRemaining = discardsRemaining;
        RoundScore = roundScore;
    }
}