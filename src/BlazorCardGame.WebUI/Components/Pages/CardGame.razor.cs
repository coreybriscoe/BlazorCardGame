using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using BlazorCardGame.Domain.Models;

namespace BlazorCardGame.WebUI.Components.Pages;

public partial class CardGame : FluxorComponent
{
    [Inject]
    public GameEngineUIFacade GameService { get; set; }
    [Inject]
    private IState<RunState> RunState { get; set; }
    [Inject]
    private IState<RoundState> RoundState { get; set; }
    [Inject]
    private IState<MenuState> MenuState { get; set; }
    [Inject]
    public IDispatcher Dispatcher { get; set; }

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