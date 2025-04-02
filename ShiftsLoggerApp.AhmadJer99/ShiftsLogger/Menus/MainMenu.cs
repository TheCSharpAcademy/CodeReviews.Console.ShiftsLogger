using ShiftsLoggerUI.Services;
using Spectre.Console;

namespace ShiftsLoggerUI.Menus;

internal class MainMenu : BaseMenu
{
    private readonly SeedingService _seedingService;
    private readonly EmployeeMenu _employeeMenu;
    private readonly ShiftsMenu _shiftsMenu;

    public MainMenu(SeedingService seedingService, EmployeeMenu employeeMenu, ShiftsMenu shiftsMenu)
    {
        _seedingService = seedingService;
        _employeeMenu = employeeMenu;
        _shiftsMenu = shiftsMenu;
    }

    private enum MenuOptions
    {
        Employees,
        Shifts,
        Seed,
        Exit
    }

    public override async Task ShowMenuAsync()
    {
        while (true)
        {
            Console.Clear();

            var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<MenuOptions>()
            .Title("[teal]Main Menu[/]")
            .AddChoices(Enum.GetValues<MenuOptions>()));

            switch (selectedOption)
            {
                case MenuOptions.Employees:
                    await _employeeMenu.ShowMenuAsync();
                    break;
                case MenuOptions.Shifts:
                    await _shiftsMenu.ShowMenuAsync();
                    // Redirect to shifts menu
                    break;
                case MenuOptions.Seed:
                    var rows = AnsiConsole.Ask<int>("How many rows would you like to seed?");
                    await _seedingService.SeedDbAsync(rows);
                    break;
                case MenuOptions.Exit:
                    AnsiConsole.MarkupLine("[Green]Cya![/]");
                    return;
            }
        }
    }
}