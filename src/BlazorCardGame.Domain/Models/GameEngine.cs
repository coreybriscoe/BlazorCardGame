namespace BlazorCardGame.Domain.Models
{
    public class GameEngine
    {
        public GameState GameState { private get; set; }

        public GameState.Memento CreateSnapshot()
        {
            return new GameState.Memento(this, GameState);   
        }
    }
}