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

            GameEngine.GameState.AddRoundScore(GameEngine.GameState.GetHandScore() * GameEngine.GameState.GetHandMultiplier());
            // Replace selected cards
            List<BasePlayingCard> handCopy = GameEngine.GameState.HandCards.ToList();
            for (int i = handCopy.Count - 1; i >= 0; i--)
            {
                if (((IPlayingCard) handCopy[i]).IsSelected())
                {
                    handCopy[i].ToggleSelect();
                    handCopy.RemoveAt(i);
                }
            }
            List<BasePlayingCard> replacementCards = GameEngine.HandDrawStrategy.DrawCards(GameEngine.GameState.HandLimit - handCopy.Count);
            handCopy.AddRange(replacementCards);

            GameEngine.GameState.SetHandCards(handCopy);
            GameEngine.GameState.DecrementHandsRemaining();
        }
    }
}