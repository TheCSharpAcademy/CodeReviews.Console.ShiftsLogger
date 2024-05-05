using ShiftLoggerConsoleApp;
using ShiftLoggerConsoleApp.UI;
using Spectre.Console;

enum MenuOptions
{
    AddShift,
    DeleteShift,
    ViewShift,
    ViewAllShifts,
    UpdateShift,
    Quit
}

public static class Application
{
    public static async Task Main()
    {
        bool isAppRunning = true;
        while (isAppRunning)
        {
            var option = AnsiConsole.Prompt(
            new SelectionPrompt<MenuOptions>()
            .Title("What would you like to do?")
            .AddChoices(
                MenuOptions.AddShift,
                MenuOptions.ViewAllShifts,
                MenuOptions.ViewShift,
                MenuOptions.UpdateShift,
                MenuOptions.DeleteShift,
                MenuOptions.Quit));

            switch (option)
            {
                case MenuOptions.AddShift:
                    var addedShift = await ShiftLoggerService.AddNewShift();
                    UserInterface.ShowShift(addedShift);
                    UserInterface.BackToMainMenuPrompt();
                    break;
                case MenuOptions.DeleteShift:
                    var deletedShifts = await ShiftLoggerService.DeleteShift();
                    UserInterface.ShowShifts(deletedShifts);
                    UserInterface.BackToMainMenuPrompt();
                    break;
                case MenuOptions.ViewShift:
                    var shifts = await ShiftLoggerService.GetShiftByName();
                    UserInterface.ShowShifts(shifts);
                    UserInterface.BackToMainMenuPrompt();
                    break;
                case MenuOptions.ViewAllShifts:
                    shifts = await ShiftLoggerService.GetShifts();
                    UserInterface.ShowShifts(shifts);
                    UserInterface.BackToMainMenuPrompt();
                    break;
                case MenuOptions.UpdateShift:
                    var updatedShift = await ShiftLoggerService.UpdateShift();
                    UserInterface.ShowShift(updatedShift);
                    UserInterface.BackToMainMenuPrompt();
                    break;
                case MenuOptions.Quit:
                    isAppRunning = false;
                    break;
            }
        }
    }
}
