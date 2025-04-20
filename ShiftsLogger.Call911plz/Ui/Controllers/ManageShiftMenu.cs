
public class ManageShiftMenuController : MenuControllerBase
{
    internal override async Task<bool> HandleMenuSelectionAsync()
    {
        MenuEnums.Shift input = DisplayMenu.ShiftsMenu();
        switch (input)
        {
            case MenuEnums.Shift.CREATESHIFT:
                break;
            case MenuEnums.Shift.READSHIFT:
                break;
            case MenuEnums.Shift.UPDATESHIFT:
                break;
            case MenuEnums.Shift.DELETESHIFT:
                break;
            case MenuEnums.Shift.BACK:
                return true;
        }
        return false;
    }
}