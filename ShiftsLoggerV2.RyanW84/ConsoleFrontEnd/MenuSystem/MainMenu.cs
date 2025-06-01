using System.Runtime.CompilerServices;
using ConsoleFrontEnd.UserInterface;
using Spectre.Console;

namespace ConsoleFrontEnd.MenuSystem;

public class MainMenu
{
    public static async Task DisplayMainMenu()
    {
        Console.Clear();
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
                    await ShiftMenu.DisplayShiftMenu();
                    break;
                case "Location Menu":
                    await LocationMenu.DisplayLocationMenu();
                    break;
                case "Worker Menu":
                    await WorkerMenu.DisplayWorkerMenu();
                    break;
                case "Exit":
                    AnsiConsole.MarkupLine("[red]Exiting the application...[/]");
                    return;
                default:
                    Console.WriteLine("invalid choice entered");
                    await DisplayMainMenu();
                    break;
            }
        }
    }
}
