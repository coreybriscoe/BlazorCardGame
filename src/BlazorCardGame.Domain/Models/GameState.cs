namespace BlazorCardGame.Domain.Models
{
    public class GameState
    {
        public List<ICard> Jokers { get; private set; } = [];
        public List<ICard> Consumables { get; private set; } = [];
        public List<BasePlayingCard> HandCards { get; private set; } = [];
        public Deck<BasePlayingCard> Deck { get; private set; }
        public int HandsRemaining { get; private set; }
        public int DiscardsRemaining { get; private set; }
        public long RoundScore { get; private set; }

        private GameEngine GameEngine { get; set; }
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