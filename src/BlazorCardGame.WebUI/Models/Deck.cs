public class Deck(List<ICard> cards)
{
    private List<ICard> Cards { get; set; } = cards;
    private List<ICard> DiscardPile { get; set; } = new List<ICard>();
    public void Shuffle()
    {
        Random random = new Random();
        for (int i = 0; i < Cards.Count; i++)
        {
            int j = random.Next(i, Cards.Count);
            ICard temp = Cards[i];
            Cards[i] = Cards[j];
            Cards[j] = temp;
        }
    }

    public ICard Draw()
    {
        ICard card = Cards[0];
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
}