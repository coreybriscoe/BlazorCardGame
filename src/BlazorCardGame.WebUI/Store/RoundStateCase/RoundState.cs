[FeatureState]
public class RoundState
{
    public int HandsRemaining { get; }
    public int DiscardsRemaining { get; }
    public long RoundScore { get; }

    public RoundState() { }
    public RoundState(int handsRemaining, int discardsRemaining, long roundScore)
    {
        HandsRemaining = handsRemaining;
        DiscardsRemaining = discardsRemaining;
        RoundScore = roundScore;
    }
}