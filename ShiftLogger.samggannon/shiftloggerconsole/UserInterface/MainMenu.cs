using Spectre.Console;
using shiftloggerconsole.UserInterface;
using static shiftloggerconsole.UserInterface.Enums;
using shiftloggerconsole.Services;

namespace shiftloggerconsole.UserInterface;

internal static class MainMenu
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
                    MenuOptions.ShowAllShifts,
                    MenuOptions.EditShiftById,
                    MenuOptions.DeleteShiftById,
                    MenuOptions.Quit
                    ));

            switch (option)
            {
                case MenuOptions.AddShift:
                    ShiftLoggerService.InsertShiftAsync();
                    break;
                case MenuOptions.ShowAllShifts:
                    ShiftLoggerService.GetAllShifts();
                    break;
                case MenuOptions.EditShiftById:
                    ShiftLoggerService.EditShift();
                    break;
                case MenuOptions.DeleteShiftById:
                    ShiftLoggerService.DeleteShiftById();
                    break;
                case MenuOptions.Quit:
                    appIsRunning = false;
                    Environment.Exit(0);
                    break;

            }
        }
    }
}
