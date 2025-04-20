

public class ManageShiftMenuController : MenuControllerBase
{
    internal override async Task<bool> HandleMenuSelectionAsync()
    {
        MenuEnums.Shift input = DisplayMenu.ShiftsMenu();
        switch (input)
        {
            case MenuEnums.Shift.CREATESHIFT:
                await CreateShiftAsync();
                break;
            case MenuEnums.Shift.READSHIFT:
                await ReadShiftAsync();
                break;
            case MenuEnums.Shift.UPDATESHIFT:
                await UpdateShiftAsync();
                break;
            case MenuEnums.Shift.DELETESHIFT:
                await DeleteShiftAsync();
                break;
            case MenuEnums.Shift.BACK:
                return true;
        }
        return false;
    }

    private async Task CreateShiftAsync()
    {
        throw new NotImplementedException();
    }

    private async Task ReadShiftAsync()
    {
        throw new NotImplementedException();
    }

    private async Task UpdateShiftAsync()
    {
        throw new NotImplementedException();
    }

    private async Task DeleteShiftAsync()
    {
        throw new NotImplementedException();
    }
}