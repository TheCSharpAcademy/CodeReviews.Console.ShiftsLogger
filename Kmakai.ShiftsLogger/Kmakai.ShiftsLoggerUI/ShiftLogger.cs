

using Kmakai.ShiftsLoggerUI.Services;
using Spectre.Console;

namespace Kmakai.ShiftsLoggerUI;

public class ShiftLogger
{
    private readonly ShiftsLoggerService ShiftsLoggerService = new ShiftsLoggerService(new HttpClient());
    private bool IsRunning = true;

    public void Start()
    {
        while (IsRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOption>()
                .Title("What do you want to do?")
                .AddChoices(MenuOption.ClockIn, MenuOption.ClockOut, MenuOption.ListShifts, MenuOption.Exit));

            switch (option)
            {
                case MenuOption.ListShifts:
                    ShiftsLoggerService.ListShifts();
                    break;
                case MenuOption.ClockIn:
                    ShiftsLoggerService.ClockIn();
                    break;
                case MenuOption.ClockOut:
                    ShiftsLoggerService.ClockOut();
                    break;
                case MenuOption.Exit:
                    IsRunning = false;
                    break;
                default:
                    break;
            }
        }
    }


}

public enum MenuOption
{
    ListShifts = 1,
    ClockIn,
    ClockOut,
    Exit
}