using Spectre.Console;
using shiftloggerconsole.UserInterface;
using static shiftloggerconsole.UserInterface.Enums;


namespace shiftloggerconsole.UserInterface;

internal class MainMenu
{
    internal static void ShowMenu()
    {
        var appIsRunning = true;
        while(appIsRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOptions>()
                .Title("What would you like to do?")
                .AddChoices(
                    MenuOptions.AddShift,
                    MenuOptions.ShowShift,
                    MenuOptions.ShowShiftById,
                    MenuOptions.EditShiftById,
                    MenuOptions.DeleteShiftById,
                    MenuOptions.Quit
                    ));
        }
    }
}
