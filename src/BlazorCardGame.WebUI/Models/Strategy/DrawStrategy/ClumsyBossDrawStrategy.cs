public class ClumsyBossDrawStrategy<T> : BaseDrawStrategy<T> where T : ICard
{
    public ClumsyBossDrawStrategy(Deck<T> deck) : base(deck)
    {
        FaceUpRate = .857F;
    }
}