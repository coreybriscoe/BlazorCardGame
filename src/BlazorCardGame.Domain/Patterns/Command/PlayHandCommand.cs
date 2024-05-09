using BlazorCardGame.Domain.Models;
namespace BlazorCardGame.Domain.Patterns.Command
{
    public class PlayHandCommand : Command
    {
        public PlayHandCommand(GameEngine gameEngine) : base(gameEngine)
        {
            
        }

        public override void Execute()
        {
            SaveBackup();
            // TODO
        }
    }
}