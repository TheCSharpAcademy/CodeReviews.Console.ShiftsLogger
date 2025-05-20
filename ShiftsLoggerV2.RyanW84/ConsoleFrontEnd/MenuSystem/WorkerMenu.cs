using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;


namespace ConsoleFrontEnd.UserInterface;
internal static class WorkerMenu
{
	internal static void DisplayWorkerMenu()
	{
		AnsiConsole.Clear();
		while (true)
		{
			AnsiConsole.Write(new Rule("[bold yellow]Worker Menu[/]").RuleStyle("yellow").Centered());
			AnsiConsole.WriteLine("Please select an option from the menu below:");
			var choice = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("[yellow]Select an option:[/]")
					.AddChoices("Create Worker", "View Workers", "Edit Worker", "Delete Worker", "Back to Main Menu")
			);
			switch (choice)
			{
				case "Create Worker":
					return "Create Worker";
				case "View Workers":
					return "View Workers";
				case "Edit Worker":
					return "Edit Worker";
				case "Delete Worker":
					return "Delete Worker";
				case "Back to Main Menu":
					return "Main Menu";
				default:
					AnsiConsole.MarkupLine("[red]Invalid choice, please try again.[/]");
					break;
			}
		}
	}

