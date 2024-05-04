using ShiftLoggerConsoleApp;
using Spectre.Console;
enum MenuOptions
{
    AddShift,
    DeleteShift,
    UpdateShift,
    ViewShift,
    ViewAllShifts,
    Quit
}

public static class Application
{
    public static void Main()
    {
        bool isAppRunning = true;
        while (isAppRunning)
        {
            var option = AnsiConsole.Prompt(
            new SelectionPrompt<MenuOptions>()
            .Title("What would you like to do?")
            .AddChoices(
                MenuOptions.AddShift,
                MenuOptions.DeleteShift,
                MenuOptions.UpdateShift,
                MenuOptions.ViewAllShifts,
                MenuOptions.ViewShift,
                MenuOptions.Quit));

            switch (option)
            {
                case MenuOptions.AddShift:
                    break;
                case MenuOptions.DeleteShift:
                    break;
                case MenuOptions.UpdateShift:
                    break;
                case MenuOptions.ViewShift:
                    ShiftLoggerService.GetShift();
                    break;
                case MenuOptions.ViewAllShifts:
                    break;
                case MenuOptions.Quit:
                    isAppRunning = false;
                    break;
            }
        }
    }
}
