using FrontEnd.Services;

using ShiftsLogger.Ryanw84.Services;

using Spectre.Console;

namespace FrontEnd.UserInterface;
internal class MainMenu
	{
	public MainMenu( )
		{
		bool isRunning = true;

		while(isRunning)
			{
			// Display the menu
			var choice = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("[yellow]What would you like to do?[/]")
					.AddChoices("Add Shift" , "View Shifts" , "Exit")
			);

			// Handle the user's choice
			switch(choice)
				{
				//case "Add Shift":
					//UiShiftService.GetAllShifts();
					//break;
				case "View Shifts":
					UIShiftService.
					break;
				case "Exit":
					isRunning = false;
					AnsiConsole.MarkupLine("[green]Exiting the application. Goodbye![/]");
					break;
				}
			}
		}


	}
