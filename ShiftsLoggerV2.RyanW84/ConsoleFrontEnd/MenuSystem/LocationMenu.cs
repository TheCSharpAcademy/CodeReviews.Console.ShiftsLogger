using ConsoleFrontEnd.Controller;
using Spectre.Console;

namespace ConsoleFrontEnd.MenuSystem;

public class LocationMenu(MainMenu mainMenu)
{
    public static async Task DisplayLocationMenu()
    {
        LocationController frontEndLocationController = new();

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
                        "Create Location",
                        "View Locations",
                        "Edit Location",
                        "Delete Location",
                        "Back to Main Menu"
                    )
            );

            switch (choice)
            {
                case "Create Location":
                    //var newLocation = UserInterface.CreateLocationUI();
                    // Call the service to create the location
                    // await locationService.CreateLocation(newLocation);
                    break;
            }
        }
    }
}
