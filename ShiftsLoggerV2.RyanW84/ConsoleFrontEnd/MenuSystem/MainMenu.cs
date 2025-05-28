using ConsoleFrontEnd.UserInterface;
using Spectre.Console;

namespace ConsoleFrontEnd.MenuSystem;

internal class MainMenu
{
    public static void DisplayMainMenu()
    {
        while (true)
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
                    ShiftMenu.DisplayShiftMenu();
                    break;
                case "Location Menu":
                    LocationMenu.DisplayLocationMenu();
                    break;
                case "Worker Menu":
                    WorkerMenu.DisplayWorkerMenu();
                    break;
                case "Exit":
                    AnsiConsole.MarkupLine("[red]Exiting the application...[/]");
                    return;
                default:
                    Console.WriteLine("invalid choice entered");
                    DisplayMainMenu();
                    break;
            }
        }
    }
}
