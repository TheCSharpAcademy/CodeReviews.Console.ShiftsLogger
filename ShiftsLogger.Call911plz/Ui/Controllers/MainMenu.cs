
using Spectre.Console;

public class MainMenuController : MenuControllerBase
{
    internal override void OnReady()
    {
        Console.Clear();
        AnsiConsole.Write(
            new FigletText("Shifts Logger")
                .Centered()
                .Color(Color.Aquamarine1_1)
        );
    }

    internal override async Task<bool> HandleMenuSelection()
    {
        MenuEnums.Main input = DisplayMenu.MainMenu();
        switch (input)
        {
            case MenuEnums.Main.MANAGESHIFT:
                break;
            case MenuEnums.Main.MANAGEWORKER:
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