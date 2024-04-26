using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorCardGame.WebUI.Components.PlayArea;

public partial class CardComponent<T> : FluxorComponent where T : ICard
{
    [Parameter]
    public T Card { get; set; }

    [Parameter]
    public EventCallback<T> CardClicked { get; set; }

    private async Task OnClick()
    {
        await CardClicked.InvokeAsync(Card);
        StateHasChanged();
    }

    public bool IsRed()
    {
        return Card.GetSuit() == '♥' || Card.GetSuit() == '♦';
    }

    private string cardColorCssClass()
    {
        return IsRed() ? "red" : "black";
    }

    private string cardCssClass()
    {
        return (Card is IPlayingCard && ((IPlayingCard) Card).IsSelected()) ? "playing-card selected" : "playing-card";
    }
}