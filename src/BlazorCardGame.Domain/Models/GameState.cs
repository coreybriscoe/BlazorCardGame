namespace BlazorCardGame.Domain.Models
{
    public class GameState
    {
        private List<ICard> Jokers { get; set; } = [];
        private List<ICard> Consumables { get; set; } = [];
        public List<BasePlayingCard> HandCards { get; private set; } = [];
        public Deck<BasePlayingCard> Deck { get; private set; } = new Deck<BasePlayingCard>([]);
        public int HandLimit { get; private set; } = 8;
        private int HandsRemaining { get; set; } = 3;
        private int DiscardsRemaining { get; set; } = 3;
        private long RoundScore { get; set; } = 0;

        private GameEngine GameEngine { get; set; }
        public GameState(GameEngine gameEngine)
        {
            GameEngine = gameEngine;
            // TODO: Use Strategies to customize game state initialization for different decks
            // Initialize standard deck
            char[] suits = { '♦', '♣', '♥', '♠' };
            for (int i = 2; i < 15; i++)
            {
                string rankString = i switch
                {
                    11 => "J",
                    12 => "Q",
                    13 => "K",
                    14 => "A",
                    _ => i.ToString()
                };
                for (int j = 0; j < 4; j++)
                {
                    Deck.AddCard(new PlayingCard(i, suits[j], rankString: rankString, isFaceUp: true, isSelectable: true), false);
                }
            }
            Deck.Shuffle();
        }

        public GameState(GameEngine gameEngine, GameState gameState)
        {
            GameEngine = gameEngine;
            Jokers = gameState.Jokers;
            Consumables = gameState.Consumables;
            HandCards = gameState.HandCards;
            Deck = gameState.Deck;
            HandsRemaining = gameState.HandsRemaining;
            DiscardsRemaining = gameState.DiscardsRemaining;
            RoundScore = gameState.RoundScore;
        }

        public void AddRoundScore(long handScore)
        {
            RoundScore += handScore;
        }

        public void SetRoundScore(long value)
        {
            RoundScore = value;
        }

        public void SelectCard(ICard card)
        {
            if (card is BasePlayingCard && ((IPlayingCard) card).IsSelectable())
            {
                ((BasePlayingCard) card).ToggleSelect();
            }
        }

        public void SetHandCards(List<BasePlayingCard> handCards)
        {
            HandCards = handCards;
        }

        public void DecrementHandsRemaining()
        {
            HandsRemaining--;
        }

        public long GetHandScore()
        {
            List<BasePlayingCard> selectedCards = HandCards.Where(c => ((IPlayingCard) c).IsSelected()).ToList();
            PokerLogic.HandCategory selectedCardsCategory = PokerLogic.GetHandCategory<BasePlayingCard>(HandCards.Where(c => ((IPlayingCard) c).IsSelected()).ToList());
            // Determine points: sum of scoring cards' value and base category points
            long points = HandScores.handBasePoints.GetValueOrDefault<PokerLogic.HandCategory, int>(selectedCardsCategory, 0);
            // Scoring cards are cards for which their exclusion would change the hand category
            // unless the hand category is high card, in which case score only the highest card
            if (selectedCardsCategory != PokerLogic.HandCategory.HighCard)
            {
                points += HandCards.Where(
                    c => selectedCardsCategory != PokerLogic.GetHandCategory(
                        selectedCards.Where(
                            c2 => !c2.Equals(c)
                        ).ToList<IPlayingCard>()
                    )
                ).Sum(c => c.GetPoints());
            }
            else
            {
                points += HandCards.Where(c => ((IPlayingCard) c).IsSelected()).Max(c => c.GetPoints());
            }
            return points;
        }

        public long GetHandMultiplier()
        {
            return HandScores.handBaseMultiplier.GetValueOrDefault<PokerLogic.HandCategory, int>(PokerLogic.GetHandCategory<BasePlayingCard>(HandCards.Where(c => ((IPlayingCard) c).IsSelected()).ToList()), 0);
        }

        // Memento pattern
        public void Restore(Memento memento)
        {
            Jokers = memento.GameState.Jokers;
            Consumables = memento.GameState.Consumables;
            HandCards = memento.GameState.HandCards;
            Deck = memento.GameState.Deck;
            HandsRemaining = memento.GameState.HandsRemaining;
            DiscardsRemaining = memento.GameState.DiscardsRemaining;
            RoundScore = memento.GameState.RoundScore;
        }

        public class Memento
        {
            private GameEngine GameEngine { get; set; }
            public GameState GameState { get; private set; }

            public Memento(GameEngine gameEngine, GameState gameState)
            {
                GameEngine = gameEngine;
                GameState = gameState;
            }

            public void Restore()
            {
                GameEngine.GameState = GameState;
            }
        }
    }
}