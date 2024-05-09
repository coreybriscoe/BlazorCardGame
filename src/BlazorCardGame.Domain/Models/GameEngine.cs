using BlazorCardGame.Domain.Patterns.Command;
using BlazorCardGame.Domain.Patterns.Strategy.DrawStrategy;
namespace BlazorCardGame.Domain.Models
{
    public class GameEngine
    {
        public GameState GameState { get; set; }
        public CommandHistory CommandHistory { get; set; } = new CommandHistory();
        public IDrawStrategy<BasePlayingCard> HandDrawStrategy { get; set; }

        public GameEngine()
        {
            GameState = new GameState(this);
            HandDrawStrategy = new DefaultDrawStrategy<BasePlayingCard>(GameState.Deck);
            // Draw cards
            GameState.SetHandCards(HandDrawStrategy.DrawCards(GameState.HandLimit));
        }

        // Memento pattern
        public GameState.Memento CreateSnapshot()
        {
            return new GameState.Memento(this, GameState);   
        }

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            CommandHistory.Push(command);
        }
    }
}