using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using BlazorCardGame.Domain.Models;

namespace BlazorCardGame.WebUI.Components.PlayArea;

public partial class CardPoolComponent<T> : FluxorComponent where T : ICard
{
    [Parameter]
    public List<T> cards { get; set; }

    [Parameter]
    public bool cardsSelectable { get; set; } = false;

    [Parameter]
    public bool isHand { get; set; } = false;

    [Inject]
    public IState<MenuState> MenuState { get; set; }
    [Inject]
    public IDispatcher Dispatcher { get; set; }

    private int maxSelectableCards = 5;

    private void CardClicked(T card)
    {
        if (isHand)
        {
            if (typeof(T) != typeof(BasePlayingCard))
            {
                return;
            }
            if (cardsSelectable && card is IPlayingCard && ((IPlayingCard) card).IsSelectable())
            {
                if (((IPlayingCard) card).IsSelected() || cards.Count(c => c is IPlayingCard && ((IPlayingCard) c).IsSelected()) < maxSelectableCards)
                {
                    ((BasePlayingCard)(object) card).ToggleSelect();
                    Dispatcher.Dispatch(new SetHandCardsAction(cards.Select(c => (BasePlayingCard)(object) c).ToList()));
                }
            }
        }
    }
}