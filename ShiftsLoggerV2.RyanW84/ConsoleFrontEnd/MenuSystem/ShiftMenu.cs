using ConsoleFrontEnd.Controller;

using Spectre.Console;

namespace ConsoleFrontEnd.MenuSystem;

public class ShiftMenu
{
    ShiftController shiftController = new ShiftController();

    public async Task DisplayShiftMenu()
    {
        AnsiConsole.Clear();
        while (true)
        {
            Console.Clear();
            AnsiConsole.Write(
                new Rule("[bold yellow]Shift Menu[/]").RuleStyle("yellow").Centered()
            );
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Select an option:[/]")
                    .AddChoices(
                        "Create Shift",
                        "View Shifts",
                        "View Shift by ID",
                        "Update Shift",
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
                case "View Shift by ID":
                    await shiftController.GetShiftById();
                    break;
                case "Update Shift":
                    await shiftController.UpdateShift();
                    break;
                case "Delete Shift":
                    await shiftController.DeleteShift();
                    break;
                case "Back to Main Menu":
                    return;
                default:
                    AnsiConsole.MarkupLine("[red]Invalid choice, please try again.[/]");
                    break;
            }
        }
    }
}
