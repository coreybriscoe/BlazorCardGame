using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using BlazorCardGame.Domain.Models;

namespace BlazorCardGame.WebUI.Components.PlayArea;

public partial class PlayAreaComponent : FluxorComponent
{
    [Inject]
    private GameEngineUIFacade GameService { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        GameService.NotifySubscribers += StateHasChanged;
    }

    private void Dispose()
    {
        GameService.NotifySubscribers -= StateHasChanged;
    }
}