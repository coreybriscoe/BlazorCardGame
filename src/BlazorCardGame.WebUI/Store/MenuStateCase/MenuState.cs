[FeatureState]
public class MenuState
{
    public MenuType ActiveMenu { get; } = MenuType.START;

    public MenuState() { }
    public MenuState(MenuType activeMenu)
    {
        ActiveMenu = activeMenu;
    }

    public enum MenuType
    {
        PLAY,
        PAYOUT,
        SHOP,
        BLIND,
        PACK,
        START,
    }
}