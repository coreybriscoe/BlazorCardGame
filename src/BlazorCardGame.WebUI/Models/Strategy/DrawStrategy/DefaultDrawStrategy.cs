public class DefaultDrawStrategy<T> : BaseDrawStrategy<T> where T : ICard
{
    public DefaultDrawStrategy(Deck deck) : base(deck) { }
}