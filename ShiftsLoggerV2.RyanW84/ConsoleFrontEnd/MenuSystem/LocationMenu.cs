using ShiftsLoggerV2.RyanW84.Services; // Assuming UserInterface is in this namespace

using Spectre.Console;

namespace ConsoleFrontEnd.MenuSystem;

public static class LocationMenu
{
	// Pass an instance of UserInterface to handle user input and service calls
	public static async Task DisplayLocationMenu(LocationService location)
	{
		AnsiConsole.Clear();
		while (true)
		{
			AnsiConsole.Write(
				new Rule("[bold yellow]Location Menu[/]").RuleStyle("yellow").Centered()
			);
			AnsiConsole.WriteLine("Please select an option from the menu below:");
			var choice = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("[yellow]Select an option:[/]")
					.AddChoices(
						"Create Location" ,
						"View Locations" ,
						"Edit Location" ,
						"Delete Location" ,
						"Back to Main Menu"
					)
			);
			switch (choice)
			{
				case "Create Location":
					await UserInterface.CreateLocationUI();
					break;
				case "View Locations":
					await UserInterface.GetLocationsUI(locationService);
					break;
					case "Edit Location":
					await UserInterface.UpdateLocationUI(locationService);
					break;
					case "Delete Location":
					await UserInterface.DeleteLocationUI(locationService);
					break;
					case "Back to Main Menu":
					MainMenu.DisplayMainMenu();
					break;
			}
		}
	}
}

