
public class ManageWorkerMenuController : MenuControllerBase
{
    internal override async Task<bool> HandleMenuSelectionAsync()
    {
        MenuEnums.Worker input = DisplayMenu.WorkersMenu();
        switch (input)
        {
            case MenuEnums.Worker.CREATEWORKER:
                break;
            case MenuEnums.Worker.READWORKER:
                break;
            case MenuEnums.Worker.READWORKERBYID:
                break;
            case MenuEnums.Worker.UPDATEWORKER:
                break;
            case MenuEnums.Worker.DELETEWORKER:
                break;
            case MenuEnums.Worker.BACK:
                return true;
        }
        return false;
    }
}