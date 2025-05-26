using ConsoleFrontEnd.ApiShiftService;
using ConsoleFrontEnd.Services;
using Spectre.Console;

namespace ConsoleFrontEnd.MenuSystem;

public class ShiftMenu
{
    public static async Task DisplayShiftMenu()
    {
        ShiftController shiftController = new ShiftController();

        AnsiConsole.Clear();
        while (true)
        {
            Console.Clear();
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
                        "View Shift by ID",
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
                case "View Shifts":
                    await shiftController.GetAllShifts();
                    break;
                //case "View Shift by ID":
                //	await shiftController.GetShiftById();
                //	break;
            }
        }
    }
}
