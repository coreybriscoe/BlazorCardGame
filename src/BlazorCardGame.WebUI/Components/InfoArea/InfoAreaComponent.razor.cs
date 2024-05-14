using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using BlazorCardGame.Domain.Models;

namespace BlazorCardGame.WebUI.Components.InfoArea;

public partial class InfoAreaComponent : FluxorComponent
{
    [Inject]
    private IState<RunState> RunState { get; set; }
    [Inject]
    private IState<RoundState> RoundState { get; set; }
    [Inject]
    private IState<MenuState> MenuState { get; set; }
    [Inject]
    private GameEngineUIFacade GameService { get; set; }
    [Inject]
    public IDispatcher Dispatcher { get; set; }

    private long getScoreToWin()
    {
        long baseAnte = Levels.baseAnteLevels[RunState.Value.Ante];
        switch (RunState.Value.Phase)
        {
            case Levels.Phase.SMALL_BLIND:
                return baseAnte;
            case Levels.Phase.BIG_BLIND:
                return (long) (baseAnte * Levels.BIG_BLIND_FACTOR);
            case Levels.Phase.BOSS_BLIND:
                return (long) (baseAnte * 2); // TODO: calculate using boss's factor
            default:
                throw new InvalidOperationException($"RunState.Value.Phase had an unexpected value: {RunState.Value.Phase}");
        }
    }

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