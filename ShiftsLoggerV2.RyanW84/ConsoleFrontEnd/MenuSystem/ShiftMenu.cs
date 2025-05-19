using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace ConsoleFrontEnd.UserInterface;
internal class ShiftMenu
{
	public void DisplayShiftMenu()
	{
		AnsiConsole.Clear();
		while (true)
		{
			AnsiConsole.Write(new Rule("[bold yellow]Shift Menu[/]").RuleStyle("yellow").Centered());
			AnsiConsole.WriteLine("Please select an option from the menu below:");
			var choice = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("[yellow]Select an option:[/]")
					.AddChoices("Create Shift", "View Shifts", "Edit Shift", "Delete Shift", "Back to Main Menu")
			);
			//switch (choice)
			//{
			//	case "Create Shift":
			//		return "Create Shift";
			//	case "View Shifts":
			//		return "View Shifts";
			//	case "Edit Shift":
			//		return "Edit Shift";
			//	case "Delete Shift":
			//		return "Delete Shift";
			//	case "Back to Main Menu":
			//		return "Main Menu";
			//	default:
			//		AnsiConsole.MarkupLine("[red]Invalid choice, please try again.[/]");
			//		break;
			}
		}
	}

