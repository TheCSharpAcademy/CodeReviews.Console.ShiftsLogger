using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace ConsoleFrontEnd.UserInterface;
internal class LocationMenu
{
	public void DisplayLocationMenu( )
	{
		AnsiConsole.Clear();
		while (true)
		{
			AnsiConsole.Write(new Rule("[bold yellow]Location Menu[/]").RuleStyle("yellow").Centered());
			AnsiConsole.WriteLine("Please select an option from the menu below:");
			var choice = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("[yellow]Select an option:[/]")
					.AddChoices("Create Location" , "View Locations" , "Edit Location" , "Delete Location" , "Back to Main Menu")
			);
			//	switch (choice)
			//	{
			//		case "Create Location":
			//			return "Create Location";
			//		case "View Locations":
			//			return "View Locations";
			//		case "Edit Location":
			//			return "Edit Location";
			//		case "Delete Location":
			//			return "Delete Location";
			//		case "Back to Main Menu":
			//			return "Main Menu";
			//		default:
			//			AnsiConsole.MarkupLine("[red]Invalid choice, please try again.[/]");
			//			break;
			//	}
			//}
		}
	}
}

