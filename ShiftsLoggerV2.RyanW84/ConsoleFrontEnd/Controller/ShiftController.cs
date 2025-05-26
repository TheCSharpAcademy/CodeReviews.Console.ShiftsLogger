using ConsoleFrontEnd.MenuSystem;
using ConsoleFrontEnd.Models;
using ConsoleFrontEnd.Services;
using Spectre.Console;

public class ShiftController()
{
    private static readonly ShiftService shiftService = new ShiftService();
    private static ShiftFilterOptions shiftFilterOptions = new()
    {
        WorkerId = null,
        LocationId = null,
        StartTime = null,
        EndTime = null,
    };

    public async Task CreateShift()
    {
        var shift = UserInterface.CreateShiftUi();

        var createdShift = await shiftService.CreateShift(shift);
        if (createdShift == null)
        {
            AnsiConsole.MarkupLine("[red]Error: Failed to create shift.[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[green]Shift created successfully![/]");
            AnsiConsole.MarkupLine($"[green]Shift ID: {createdShift.ShiftId}[/]");
        }
    }

    public async Task GetAllShifts()
    {
        var shifts = await shiftService.GetAllShifts();
        UserInterface.DisplayAllShiftsTable(shifts.ToList());
    }
}
