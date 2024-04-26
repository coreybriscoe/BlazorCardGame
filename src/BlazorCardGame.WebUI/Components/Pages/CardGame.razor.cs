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
        List<BasePlayingCard> hand = [];
        for (int i = 0; i < handLimit; i++)
        {
            ICard card = deck.Draw();
            hand.Add((PlayingCard) card);
        }
        // Set hand cards state
        Dispatcher.Dispatch(new SetHandCardsAction(hand));

        // Set initial hands remaining and discards remaining state
        Dispatcher.Dispatch(new SetHandsRemainingAction(3));
        Dispatcher.Dispatch(new SetDiscardsRemainingAction(3));
    }

    private string handCategory = "?";
    private int handScore = 0;
    private int handMultiplier = 0;

    private void PlayHandClicked()
    {
        Dispatcher.Dispatch(new AddRoundScoreAction(handScore * handMultiplier));
        // Replace selected cards
        List<BasePlayingCard> newHand = CardsState.Value.HandCards.ToList();
        newHand.RemoveAll(c => ((IPlayingCard) c).IsSelected());
        List<BasePlayingCard> selectedCards = newHand.Where(c => ((IPlayingCard) c).IsSelected()).ToList<BasePlayingCard>();
        selectedCards.ForEach(c => {
            c.ToggleSelect();
            ICard replacementCard = deck.Draw();
            newHand.Add((PlayingCard) replacementCard);
        });
        Dispatcher.Dispatch(new SetHandCardsAction(newHand));
        Dispatcher.Dispatch(new DecrementHandsRemainingAction());
    }

    private void DiscardHandClicked()
    {
        // Replace selected cards
        List<BasePlayingCard> newHand = CardsState.Value.HandCards.ToList();
        newHand.RemoveAll(c => ((IPlayingCard) c).IsSelected());
        newHand.ForEach(c => {
            c.ToggleSelect();
            ICard replacementCard = deck.Draw();
            newHand.Add((PlayingCard) replacementCard);
        });
        Dispatcher.Dispatch(new DecrementDiscardsRemainingAction());
    }
}