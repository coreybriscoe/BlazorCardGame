using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using BlazorCardGame.Domain.Models;

namespace BlazorCardGame.WebUI.Components.InfoArea;

public partial class InfoAreaComponent : FluxorComponent
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

     private string GetInfoAreaColor()
    {
        string baseClasses = "";
        switch (GameService.GetPhase())
        {
            case Levels.Phase.SMALL_BLIND:
                return baseClasses + " blue";
            case Levels.Phase.BIG_BLIND:
                return baseClasses + " gold";
            case Levels.Phase.BOSS_BLIND:
                return baseClasses + " ";
            default:
                return baseClasses;
        }
    }

    private string GetInfoAreaBackgroundColor()
    {
        string baseClasses = "";
        switch (GameService.GetPhase())
        {
            case Levels.Phase.SMALL_BLIND:
                return baseClasses + " lightblue-bg";
            case Levels.Phase.BIG_BLIND:
                return baseClasses + " darkgold-bg";
            case Levels.Phase.BOSS_BLIND:
                return baseClasses + " ";
            default:
                return baseClasses;
        }
    }

    private string GetInfoAreaBorderColor()
    {
        string baseClasses = "";
        switch (GameService.GetPhase())
        {
            case Levels.Phase.SMALL_BLIND:
                return baseClasses + " lightblue-border";
            case Levels.Phase.BIG_BLIND:
                return baseClasses + " darkgold-border";
            case Levels.Phase.BOSS_BLIND:
                return baseClasses + " ";
            default:
                return baseClasses;
        }
    }

    private string GetPhaseTitle()
    {
        switch (GameService.GetPhase())
        {
            case Levels.Phase.SMALL_BLIND:
                return "Small Blind";
            case Levels.Phase.BIG_BLIND:
                return "Big Blind";
            case Levels.Phase.BOSS_BLIND:
                // TODO: Use the boss's name as the title
                return "Boss Blind";
            default:
                return "???";
        }
    }
}