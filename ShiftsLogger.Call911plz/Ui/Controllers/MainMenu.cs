
using Spectre.Console;

public class MainMenuController : MenuControllerBase
{
    internal override void OnReady()
    {
        base.OnReady();
        AnsiConsole.Write(
            new FigletText("Shifts Logger")
                .Centered()
                .Color(Color.Aquamarine1_1)
        );
    }

    internal override async Task<bool> HandleMenuSelectionAsync()
    {
        MenuEnums.Main input = DisplayMenu.MainMenu();
        switch (input)
        {
            case MenuEnums.Main.MANAGESHIFT:
                ManageShiftMenuController manageShiftMenu = new();
                await manageShiftMenu.StartAsync();
                break;
            case MenuEnums.Main.MANAGEWORKER:
                ManageWorkerMenuController manageWorkerMenu = new();
                await manageWorkerMenu.StartAsync();
                break;
            case MenuEnums.Main.EXIT:
                return true;
        }
        return false;
    }

    internal override void OnExit()
    {
        Console.Clear();
        AnsiConsole.Write(
            new FigletText("Goodbye!")
                .Centered()
                .Color(Color.Green)
        );
    }
}