public class SetMenuAction
{
    public MenuState.MenuType ActiveMenu { get; }
    public SetMenuAction() { }
    public SetMenuAction(MenuState.MenuType activeMenu)
    {
        ActiveMenu = activeMenu;
    }
}