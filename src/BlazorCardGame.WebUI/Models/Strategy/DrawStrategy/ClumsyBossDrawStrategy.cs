public class ClumsyBossDrawStrategy<T> : BaseDrawStrategy<T> where T : ICard
{
    public ClumsyBossDrawStrategy(Deck deck) : base(deck)
    {
        FaceUpRate = .857F;
    }
}