using ConsoleFrontEnd.Controller;
using ConsoleFrontEnd.UserInterface;
using Spectre.Console;

namespace ConsoleFrontEnd.UserInterface;

public class ShiftMenu
{
    public static async Task DisplayShiftMenu()
    {
        var shiftController = new ShiftController();

        AnsiConsole.Clear();
        while (true)
        {
            AnsiConsole.Write(
                new Rule("[bold yellow]Shift Menu[/]").RuleStyle("yellow").Centered()
            );
            AnsiConsole.WriteLine("Please select an option from the menu below:");
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Select an option:[/]")
                    .AddChoices(
                        "Create Shift",
                        "View Shifts",
                        "Edit Shift",
                        "Delete Shift",
                        "Back to Main Menu"
                    )
            );
            switch (choice)
            {
                case "Create Shift":
                    await shiftController.CreateShift();
                    break;
            }
        }
    }
}
