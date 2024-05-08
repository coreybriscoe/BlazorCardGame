public class DefaultDrawStrategy<T> : BaseDrawStrategy<T> where T : ICard
{
    public DefaultDrawStrategy(Deck<T> deck) : base(deck) { }
}