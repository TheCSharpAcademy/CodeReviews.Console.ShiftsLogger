using Spectre.Console;

using UI.Services;
using static UI.Enums;

namespace UI.UserInterface;

static internal class UserInterface
{
    static public void MainMenu()
    {
        var isAppRunning = true;
        while (isAppRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
            new SelectionPrompt<MainMenuOptions>()
            .Title("What would you like to do?")
            .AddChoices(
                MainMenuOptions.AddShift,
                MainMenuOptions.UpdateShift,
                MainMenuOptions.DeleteShift,
                MainMenuOptions.ViewAllShifts,
                MainMenuOptions.Quit));

            switch (option)
            {
                case MainMenuOptions.AddShift:
                    ShiftService.InsertShiftAsync();
                    break;
                case MainMenuOptions.UpdateShift:
                    ShiftService.UpdateShift();
                    break;
                case MainMenuOptions.DeleteShift:
                    ShiftService.DeleteShift();
                    break;
                case MainMenuOptions.ViewAllShifts:
                    ShiftService.ViewAllShifts();
                    break;
                case MainMenuOptions.Quit:
                    Console.WriteLine("Goodbye");
                    isAppRunning = false;
                    break;
            }
        }
    }
}