using BlazorCardGame.Domain.Patterns.Command;

namespace BlazorCardGame.Domain.Models
{
    public class GameEngineUIFacade
    {
        private GameEngine GameEngine { get; set; }
        
        public GameEngineUIFacade(GameEngine gameEngine)
        {
            GameEngine = gameEngine;
        }

        public List<BasePlayingCard> GetHandCards()
        {
            return GameEngine.GameState.HandCards;
        }

        public int GetHandLimit()
        {
            return GameEngine.GameState.HandLimit;
        }

        public PokerLogic.HandCategory GetHandCategory()
        {
            return PokerLogic.GetHandCategory(GameEngine.GameState.HandCards.Where(c => ((IPlayingCard) c).IsSelected()).ToList());
        }

        public string GetHandCategoryName()
        {
            return PokerLogic.HandCategoryNames.GetValueOrDefault(GetHandCategory(), "?");
        }

        public bool IsPlayableHand()
        {
            return PokerLogic.GetHandCategory(GameEngine.GameState.HandCards.Where(c => ((IPlayingCard) c).IsSelected()).ToList()) != PokerLogic.HandCategory.NoCategory;
        }

        public string GetHandScore()
        {
            return GameEngine.GameState.GetHandScore().ToString();
        }

        public string GetHandMultiplier()
        {
            return GameEngine.GameState.GetHandMultiplier().ToString();
        }

        public int GetDeckRemainingCount()
        {
            return GameEngine.GameState.Deck.Count();
        }

        public int GetDeckSize()
        {
            return GameEngine.GameState.Deck.DeckSize();
        }

        public void SelectCard(ICard card)
        {
            GameEngine.GameState.SelectCard(card);
            ChangeState();
        }

        public void PlayHand()
        {
            GameEngine.ExecuteCommand(new PlayHandCommand(GameEngine));
            ChangeState();
        }

        public void DiscardHand()
        {
            // GameEngine.ExecuteCommand(new DiscardHandCommand(GameEngine));
            ChangeState();
        }

        // Observer element subscription and notification
        public event Action NotifySubscribers;
        public void ChangeState()
        {
            NotifySubscribers?.Invoke();
        }
    }
}