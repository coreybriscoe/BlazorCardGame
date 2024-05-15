using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using BlazorCardGame.Domain.Models;

namespace BlazorCardGame.WebUI.Components.InfoArea;

public partial class PlayPhaseCellComponent : FluxorComponent
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