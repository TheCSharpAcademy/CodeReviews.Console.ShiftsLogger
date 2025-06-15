using ConsoleFrontEnd.UserInterface;
using Spectre.Console;
using ConsoleFrontEnd.Services;

namespace ConsoleFrontEnd.MenuSystem;

public class MainMenu
{
    public static async Task DisplayMainMenu(
        IWorkerService workerService,
        IShiftService shiftService,
        ILocationService locationService)
    {
        bool continueLoop = true;
        Console.Clear();
        while (continueLoop)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold yellow]Main Menu[/]").RuleStyle("yellow").Centered());
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Select an option:[/]")
                    .AddChoices("Shift Menu", "Location Menu", "Worker Menu", "Exit")
            );

            switch (choice)
            {
                case "Shift Menu":
                    await ShiftMenu.DisplayShiftMenu(shiftService);
                    break;
                case "Location Menu":
                    await LocationMenu.DisplayLocationMenu(locationService);
                    break;
                case "Worker Menu":
                    await WorkerMenu.DisplayWorkerMenu(workerService);
                    break;
                case "Exit":
                    AnsiConsole.MarkupLine("[red]Exiting the application...[/]");
                    continueLoop = false;
                    break;
                default:
                    AnsiConsole.MarkupLine("[red]Invalid choice entered[/]");
                    break;
            }
        }
    }
}
