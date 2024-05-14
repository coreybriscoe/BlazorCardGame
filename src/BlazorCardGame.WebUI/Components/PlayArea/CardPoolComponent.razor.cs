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
    private GameEngineUIFacade GameService { get; set; }

    private int maxSelectableCards = 5;

    private void CardClicked(T card)
    {
        if (cardsSelectable)
        {
            if (isHand)
            {
                GameService.SelectCard(card);   
            }
        }
    }
}