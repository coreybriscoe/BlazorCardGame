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
    private IState<CardsState> CardsState { get; set; }
    [Inject]
    private IState<MenuState> MenuState { get; set; }
    [Inject]
    public IDispatcher Dispatcher { get; set; }
    private int handLimit = 8;

    static Deck<BasePlayingCard> deck = new Deck<BasePlayingCard>([]);

    public IDrawStrategy<BasePlayingCard> DrawStrategy { get; set; }

    public CardGame()
    {
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
                deck.AddCard(new PlayingCard(i, suits[j], rankString: rankString, isFaceUp: true, isSelectable: true), false);
            }
        }
        deck.Shuffle();

        DrawStrategy = new DefaultDrawStrategy<BasePlayingCard>(deck);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        // Draw cards
        List<BasePlayingCard> hand = DrawStrategy.DrawCards(handLimit);
        // Set hand cards state
        Dispatcher.Dispatch(new SetHandCardsAction(hand));

        // Set initial hands remaining and discards remaining state
        Dispatcher.Dispatch(new SetHandsRemainingAction(3));
        Dispatcher.Dispatch(new SetDiscardsRemainingAction(3));
    }

    private PokerLogic.HandCategory selectedCardsCategory = PokerLogic.HandCategory.NoCategory;
    private bool isPlayableHand()
    {
        return PokerLogic.GetHandCategory(CardsState.Value.HandCards.Where(c => ((IPlayingCard) c).IsSelected()).ToList()) != PokerLogic.HandCategory.NoCategory;
    }

    private long getHandScore()
    {
        List<BasePlayingCard> selectedCards = CardsState.Value.HandCards.Where(c => ((IPlayingCard) c).IsSelected()).ToList();
        // Determine points: sum of scoring cards' value and base category points
        long points = HandScores.handBasePoints.GetValueOrDefault<PokerLogic.HandCategory, int>(selectedCardsCategory, 0);
        // Scoring cards are cards for which their exclusion would change the hand category
        // unless the hand category is high card, in which case score only the highest card
        if (selectedCardsCategory != PokerLogic.HandCategory.HighCard)
        {
            points += CardsState.Value.HandCards.Where(
                c => selectedCardsCategory != PokerLogic.GetHandCategory(
                    selectedCards.Where(
                        c2 => !c2.Equals(c)
                    ).ToList<IPlayingCard>()
                )
            ).Sum(c => c.GetPoints());
        }
        else
        {
            points += CardsState.Value.HandCards.Where(c => ((IPlayingCard) c).IsSelected()).Max(c => c.GetPoints());
        }
        return points;
    }

    private long getHandMultiplier()
    {
        return HandScores.handBaseMultiplier.GetValueOrDefault<PokerLogic.HandCategory, int>(selectedCardsCategory, 0);
    }

    private void PlayHandClicked()
    {
        selectedCardsCategory = PokerLogic.GetHandCategory(CardsState.Value.HandCards.Where(c => ((IPlayingCard) c).IsSelected()).ToList());
        Dispatcher.Dispatch(new AddRoundScoreAction(getHandScore() * getHandMultiplier()));
        // Replace selected cards
        List<BasePlayingCard> handCopy = CardsState.Value.HandCards.ToList();
        for (int i = handCopy.Count - 1; i >= 0; i--)
        {
            if (((IPlayingCard) handCopy[i]).IsSelected())
            {
                handCopy[i].ToggleSelect();
                handCopy.RemoveAt(i);
            }
        }
        List<BasePlayingCard> replacementCards = DrawStrategy.DrawCards(handLimit - handCopy.Count);
        handCopy.AddRange(replacementCards);

        Dispatcher.Dispatch(new SetHandCardsAction(handCopy));
        Dispatcher.Dispatch(new DecrementHandsRemainingAction());
    }

    private void DiscardHandClicked()
    {
        // Replace selected cards
        List<BasePlayingCard> handCopy = CardsState.Value.HandCards.ToList();
        for (int i = handCopy.Count - 1; i >= 0; i--)
        {
            if (((IPlayingCard) handCopy[i]).IsSelected())
            {
                handCopy[i].ToggleSelect();
                handCopy.RemoveAt(i);
            }
        }
        List<BasePlayingCard> replacementCards = DrawStrategy.DrawCards(handLimit - handCopy.Count);
        handCopy.AddRange(replacementCards);

        Dispatcher.Dispatch(new SetHandCardsAction(handCopy));
        Dispatcher.Dispatch(new DecrementDiscardsRemainingAction());
    }
}