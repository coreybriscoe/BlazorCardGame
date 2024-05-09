using BlazorCardGame.Domain.Models;
namespace BlazorCardGame.Domain.Patterns.Command
{
    public abstract class Command : ICommand
    {
        protected GameEngine GameEngine { get; set; }
        protected GameState.Memento Backup { get; set; }

        protected Command(GameEngine gameEngine)
        {
            GameEngine = gameEngine;
        }

        public void SaveBackup()
        {
            Backup = GameEngine.CreateSnapshot();
        }

        public void Undo()
        {
            if (Backup != null)
            {
                Backup.Restore();
            }
        }

        public abstract void Execute();
    }
}