using BlazorCardGame.Domain.Models;

namespace BlazorCardGame.Domain.Patterns.Command
{
    public class DiscardHandCommand : Command
    {
        public DiscardHandCommand(GameEngine gameEngine) : base(gameEngine)
        {

        }

        public override void Execute()
        {
            SaveBackup();

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
            GameEngine.GameState.DecrementDiscardsRemaining();
        }
    }
}