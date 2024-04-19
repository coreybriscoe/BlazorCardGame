using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorCardGame.WebUI.Components.Pages;

public partial class CardGame : FluxorComponent
{
    [Inject]
    private IState<RunState> RunState { get; set; }
    [Inject]
    private IState<RoundState> RoundState { get; set; }
    [Inject]
    public IDispatcher Dispatcher { get; set; }
    private int handLimit = 8;
    private void DeckClicked()
    {
        if (deck.Count() < 1 || hand.Count() >= handLimit) return;
        ICard card = deck.Draw();
        hand.Add((PlayingCard) card);
    }

    List<BasePlayingCard> hand = new List<BasePlayingCard>
    {
        // new PlayingCard(1, '♦', isFaceUp: true, isSelectable: true),
    };

    static Deck deck;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        char[] suits = { '♦', '♣', '♥', '♠' };
        List<ICard> deckCards = new List<ICard>();
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
                deckCards.Add(new PlayingCard(i, suits[j], rankString: rankString, isFaceUp: true, isSelectable: true));
            }
        }
        deck = new Deck(deckCards);
        deck.Shuffle();

        // Draw cards
        for (int i = 0; i < handLimit; i++)
        {
            ICard card = deck.Draw();
            hand.Add((PlayingCard) card);
        }

        // Set initial hands remaining and discards remaining state
        Dispatcher.Dispatch(new SetHandsRemainingAction(3));
        Dispatcher.Dispatch(new SetDiscardsRemainingAction(3));
    }

    private int phaseLevel = 1; // [1, 2, 3] for small, big, and boss blinds

    private string handCategory = "?";
    private int handScore = 0;
    private int handMultiplier = 0;
    private void OnHandCardsSelected(List<BasePlayingCard> handCards)
    {
        PokerLogic.HandCategory selectedCardsCategory = PokerLogic.GetHandCategory(handCards.ToList<IPlayingCard>());
        handCategory = PokerLogic.HandCategoryNames.GetValueOrDefault(selectedCardsCategory, "?");
        if (handCategory == "?") {
            handScore = 0;
            handMultiplier = 0;
            return;
        }
        // Determine points: sum of scoring cards' value and base category points
        int points = HandScores.handBasePoints.GetValueOrDefault<PokerLogic.HandCategory, int>(selectedCardsCategory, 0);
        List<BasePlayingCard> selectedCards = handCards.Where(c => ((IPlayingCard) c).IsSelected()).ToList();
        // Scoring cards are cards for which their exclusion would change the hand category
        // unless the hand category is high card, in which case score only the highest card
        if (selectedCardsCategory != PokerLogic.HandCategory.HighCard)
        {
            points += selectedCards.Where(
                c => selectedCardsCategory != PokerLogic.GetHandCategory(selectedCards.Where(
                    c2 => !c2.Equals(c)).ToList<IPlayingCard>())
            ).ToList<BasePlayingCard>().Sum(c => c.GetPoints());
        }
        else
        {
            points += selectedCards.Max(c => c.GetPoints());
        }
        handScore = points;
        handMultiplier = HandScores.handBaseMultiplier.GetValueOrDefault<PokerLogic.HandCategory, int>(selectedCardsCategory, 0);
    }

    private void PlayHandClicked()
    {
        Dispatcher.Dispatch(new AddRoundScoreAction(handScore * handMultiplier));
        // Replace selected cards
        List<BasePlayingCard> selectedCards = hand.Where(c => ((IPlayingCard) c).IsSelected()).ToList<BasePlayingCard>();
        hand.RemoveAll(c => ((IPlayingCard) c).IsSelected());
        selectedCards.ForEach(c => {
            c.ToggleSelect();
            ICard replacementCard = deck.Draw();
            hand.Add((PlayingCard) replacementCard);
        });
        // Update info area values
        handCategory = "?";
        handScore = 0;
        handMultiplier = 0;
        Dispatcher.Dispatch(new DecrementHandsRemainingAction());
    }

    private void DiscardHandClicked()
    {
        // Replace selected cards
        List<BasePlayingCard> selectedCards = hand.Where(c => ((IPlayingCard) c).IsSelected()).ToList<BasePlayingCard>();
        hand.RemoveAll(c => ((IPlayingCard) c).IsSelected());
        selectedCards.ForEach(c => {
            c.ToggleSelect();
            ICard replacementCard = deck.Draw();
            hand.Add((PlayingCard) replacementCard);
        });
        // Update info area values
        handCategory = "?";
        handScore = 0;
        handMultiplier = 0;
        Dispatcher.Dispatch(new DecrementDiscardsRemainingAction());
    }
}