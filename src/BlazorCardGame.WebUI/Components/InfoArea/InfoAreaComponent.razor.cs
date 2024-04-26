using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;

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
    private IState<CardsState> CardsState { get; set; }
    [Inject]
    public IDispatcher Dispatcher { get; set; }

    private long scoreToWin = 300;

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

        scoreToWin = getScoreToWin();
    }

    private string handCategoryName = "?";
    private long handScore = 0;
    private int handMultiplier = 0;

    private PokerLogic.HandCategory selectedCardsCategory = PokerLogic.HandCategory.NoCategory;
    private void getHandCategory()
    {
        selectedCardsCategory = PokerLogic.GetHandCategory(CardsState.Value.HandCards.ToList<IPlayingCard>());
        handCategoryName = PokerLogic.HandCategoryNames.GetValueOrDefault(selectedCardsCategory, "?");
    }

    private void getHandPoints()
    {
        // Determine points: sum of scoring cards' value and base category points
        long points = HandScores.handBasePoints.GetValueOrDefault<PokerLogic.HandCategory, int>(selectedCardsCategory, 0);
        // Scoring cards are cards for which their exclusion would change the hand category
        // unless the hand category is high card, in which case score only the highest card
        if (selectedCardsCategory != PokerLogic.HandCategory.HighCard)
        {
            points += CardsState.Value.HandCards.Where(
                c => selectedCardsCategory != PokerLogic.GetHandCategory(CardsState.Value.HandCards.Where(
                    c2 => !c2.Equals(c)).ToList<IPlayingCard>())
            ).ToList<BasePlayingCard>().Sum(c => c.GetPoints());
        }
        else
        {
            points += CardsState.Value.HandCards.Max(c => c.GetPoints());
        }
        handScore = points;
    }

    private void getHandMultiplier()
    {
        handMultiplier = HandScores.handBaseMultiplier.GetValueOrDefault<PokerLogic.HandCategory, int>(selectedCardsCategory, 0);
    }

    /*
            if (handCategory == "?") {
            handScore = 0;
            handMultiplier = 0;
        }
    */
}