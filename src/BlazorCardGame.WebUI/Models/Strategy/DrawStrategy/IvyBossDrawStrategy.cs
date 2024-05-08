public class IvyBossDrawStrategy<T> : BaseDrawStrategy<T> where T : ICard
{
    public IvyBossDrawStrategy(Deck<T> deck) : base(deck) { }

    public override List<T> DrawCards(int count)
    {
        List<T> cards = [];
        count = Math.Min(count, 3);
        for (int i = 0; i < count; i++)
        {
            cards.Add(DrawCard());
        }
        return cards;
    }
}