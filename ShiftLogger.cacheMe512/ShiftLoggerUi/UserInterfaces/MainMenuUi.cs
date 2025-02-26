using Spectre.Console;
using static ShiftLoggerUi.Enums;

namespace ShiftLoggerUi.UserInterfaces;

internal class MainMenuUi
{
    static internal void MainMenu()
    {
        var isAppRunning = true;
        while (isAppRunning)
        {
            var option = AnsiConsole.Prompt(
            new SelectionPrompt<MainMenuOptions>()
            .Title("What would you like to do?")
            .AddChoices(
                MainMenuOptions.ManageShifts,
                MainMenuOptions.ManageWorkers,
                MainMenuOptions.ManageDepartments,
                MainMenuOptions.Quit));

            switch (option)
            {
                case MainMenuOptions.ManageShifts:
                    ShiftsMenuUi.ShiftsMenu();
                    break;
                case MainMenuOptions.ManageWorkers:
                    WorkersMenuUi.WorkersMenu();
                    break;
                case MainMenuOptions.ManageDepartments:
                    DepartmentsMenuUi.DepartmentsMenu();
                    break;
                case MainMenuOptions.Quit:
                    Utilities.DisplayMessage("Goodbye", "cyan");
                    isAppRunning = false;
                    break;
            }
        }
    }
}
