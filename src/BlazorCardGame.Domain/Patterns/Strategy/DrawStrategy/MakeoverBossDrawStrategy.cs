using BlazorCardGame.Domain.Models;
namespace BlazorCardGame.Domain.Patterns.Strategy.DrawStrategy
{
    public class MakeoverBossDrawStrategy<T> : BaseDrawStrategy<T> where T : ICard
    {
        public MakeoverBossDrawStrategy(Deck<T> deck) : base(deck) { }

        public override List<T> DrawCards(int count)
        {
            List<T> cards = [];
            for (int i = 0; i < count; i++)
            {
                T card = DrawCard();
                int cardRank = ((BasePlayingCard) (ICard) card).GetRank();
                if (cardRank > 10 && cardRank != 14)
                {
                    card.FlipCard();
                }
                cards.Add(card);
            }
            return cards;
        }
    }
}