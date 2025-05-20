using Spectre.Console;

namespace ConsoleFrontEnd.MenuSystem;

internal class MainMenu
{


	public void DisplayMainMenu( )
	{
		while (true)
		{
			AnsiConsole.Clear();
			AnsiConsole.Write(new Rule("[bold yellow]Main Menu[/]").RuleStyle("yellow").Centered());
			AnsiConsole.WriteLine("Please select an option from the menu below:");
			var choice = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("[yellow]Select an option:[/]")
					.AddChoices("Shift Menu" , "Location Menu" , "Worker Menu" , "Exit")
			);
			switch (choice)
			{
				case "Shift Menu":
					DisplayShiftMenu();
					break;
				case "Location Menu":
					_locationMenu.DisplayLocationMenu();
					break;
				case "Worker Menu":
					_workerMenu.DisplayWorkerMenu();
					break;
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
