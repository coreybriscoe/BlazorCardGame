using BlazorCardGame.Domain.Models;
namespace BlazorCardGame.Domain.Patterns.Strategy.DrawStrategy
{
    public abstract class BaseDrawStrategy<T> : IDrawStrategy<T> where T : ICard
    {
        protected Deck<T> deck;
        protected double FaceUpRate = 1;

        protected BaseDrawStrategy(Deck<T> deck)
        {
            this.deck = deck;
        }

        protected virtual T DrawCard()
        {
            ICard card = deck.Draw();
            if (Random.Shared.NextDouble() < (1 - FaceUpRate))
            {
                card.FlipCard();
            }
            return (T) card;
        }

        public virtual List<T> DrawCards(int count)
        {
            List<T> cards = [];
            for (int i = 0; i < count; i++)
            {
                cards.Add(DrawCard());
            }
            return cards;
        }
    }
}