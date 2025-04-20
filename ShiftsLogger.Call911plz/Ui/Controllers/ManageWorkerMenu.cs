

public class ManageWorkerMenuController : MenuControllerBase
{
    internal override async Task<bool> HandleMenuSelectionAsync()
    {
        MenuEnums.Worker input = DisplayMenu.WorkersMenu();
        switch (input)
        {
            case MenuEnums.Worker.CREATEWORKER:
                await CreateWorkerAsync();
                break;
            case MenuEnums.Worker.READWORKER:
                await ReadWorkerAsync();
                break;
            case MenuEnums.Worker.READWORKERBYID:
                await ReadWorkerByIdAsync();
                break;
            case MenuEnums.Worker.UPDATEWORKER:
                await UpdateWorkerAsync();
                break;
            case MenuEnums.Worker.DELETEWORKER:
                await DeleteWorkerAsync();
                break;
            case MenuEnums.Worker.BACK:
                return true;
        }
        return false;
    }

    private async Task CreateWorkerAsync()
    {
        throw new NotImplementedException();
    }

    private async Task ReadWorkerAsync()
    {
        throw new NotImplementedException();
    }

    private async Task ReadWorkerByIdAsync()
    {
        throw new NotImplementedException();
    }

    private async Task UpdateWorkerAsync()
    {
        throw new NotImplementedException();
    }

    private async Task DeleteWorkerAsync()
    {
        throw new NotImplementedException();
    }
}