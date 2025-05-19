using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Spectre.Console;

namespace ConsoleFrontEnd.MenuSystem;

internal class MainMenu
{
    public void DisplayMainMenu()
    {
        AnsiConsole.Clear();
        while (true)
        {
            AnsiConsole.Write(new Rule("[bold yellow]Main Menu[/]").RuleStyle("yellow").Centered());
            AnsiConsole.WriteLine("Please select an option from the menu below:");
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Select an option:[/]")
                    .AddChoices("Shift Menu", "Location Menu", "Worker Menu", "Exit")
            );
            switch (choice)
            {
                //case "Shift Menu":
                //    return "Shift Menu";
                //case "Location Menu":
                //    return "Location Menu";
                case "Worker Menu":
                    return "Worker Menu";
                case "Exit":
                    AnsiConsole.MarkupLine("[red]Exiting application...[/]");
                    Environment.Exit(0);
					break;
                default:
                    AnsiConsole.MarkupLine("[red]Invalid choice, please try again.[/]");
                    break;
            }
        }
    }
}
