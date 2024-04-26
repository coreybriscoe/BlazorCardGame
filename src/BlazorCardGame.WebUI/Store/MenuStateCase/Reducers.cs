namespace BlazorCardGame.WebUI.Store.MenuStateCase;

public static class Reducers
{
    [ReducerMethod]
    public static MenuState ReduceSetMenuAction(MenuState state, SetMenuAction action)
    {
        return new MenuState(activeMenu: action.ActiveMenu);
    }
}