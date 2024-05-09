namespace BlazorCardGame.Domain.Models
{
    public class Deck<T>(List<T> cards) where T : ICard
    {
        private List<T> Cards { get; set; } = cards;
        private List<T> DiscardPile { get; set; } = [];
        public void Shuffle()
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                int j = Random.Shared.Next(i, Cards.Count);
                T temp = Cards[i];
                Cards[i] = Cards[j];
                Cards[j] = temp;
            }
        }

        public T Draw()
        {
            T card = Cards[0];
            DiscardPile.Add(card);
            Cards.RemoveAt(0);
            return card;
        }

        public void Reset()
        {
            Cards.AddRange(DiscardPile);
            DiscardPile.Clear();
            Shuffle();
        }

        public int Count()
        {
            return Cards.Count;
        }

        public int DeckSize()
        {
            return Cards.Count + DiscardPile.Count;
        }

        public void AddCard(T card, bool insertRandomly = true)
        {
            if (insertRandomly)
            {
                Cards.Insert(Random.Shared.Next(Cards.Count), card);
            } else
            {
                Cards.Add(card);
            }
        }
    }
}