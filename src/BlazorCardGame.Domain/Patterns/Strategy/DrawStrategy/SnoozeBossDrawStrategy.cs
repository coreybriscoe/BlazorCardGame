using BlazorCardGame.Domain.Models;
namespace BlazorCardGame.Domain.Patterns.Strategy.DrawStrategy
{
    public class SnoozeBossDrawStrategy<T> : BaseDrawStrategy<T> where T : ICard
    {
        public SnoozeBossDrawStrategy(Deck<T> deck) : base(deck) { }

        public override List<T> DrawCards(int count)
        {
            List<T> cards = [];
            bool isStartingHand = deck.Count() == deck.DeckSize();
            for (int i = 0; i < count; i++)
            {
                T card = DrawCard();
                if (isStartingHand)
                {
                    card.FlipCard();
                }
                cards.Add(card);
            }
            return cards;
        }
    }
}