using ConsoleFrontEnd.MenuSystem;
using ConsoleFrontEnd.Models;
using ConsoleFrontEnd.Services;
using Spectre.Console;

public class ShiftController()
{
    // This class acts as a controller for managing shifts, handling user input and interaction with the ShiftService.
    // It provides methods to create a shift and retrieve all shifts with optional filtering.
    public readonly UserInterface userInterface = new UserInterface();
    internal readonly ShiftService shiftService = new ShiftService();
    internal ShiftFilterOptions shiftFilterOptions = new()
    {
        WorkerId = null,
        LocationId = null,
        StartTime = null,
        EndTime = null,
    };

    public async Task CreateShift()
    {
        try
        {
            var shift = userInterface.CreateShiftUi();
            var createdShift = await shiftService.CreateShift(shift);
            if (createdShift == null)
            {
                AnsiConsole.MarkupLine("[red]Error: Failed to create shift.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[green]Shift created successfully![/]");
                AnsiConsole.MarkupLine($"[green]Shift ID: {createdShift.Data.ShiftId}[/]");
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Exception: {ex.Message}[/]");
        }
    }

    public async Task GetAllShifts()
    {
        try
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new Rule("[bold yellow]View All Shifts[/]").RuleStyle("yellow").Centered()
            );

            var filterOptions = userInterface.FilterShiftsUi();

            shiftFilterOptions = filterOptions;
            var shifts = await shiftService.GetAllShifts(shiftFilterOptions);
            userInterface.DisplayShiftsTable(shifts.Data);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Exception: {ex.Message}[/]");
        }
    }

    public async Task GetShiftById()
    {
        try
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new Rule("[bold yellow]View Shift by ID[/]").RuleStyle("yellow").Centered()
            );
            var shiftId = userInterface.GetShiftByIdUi();
            var shift = await shiftService.GetShiftById(shiftId);
            if (shift == null)
            {
                AnsiConsole.MarkupLine("[red]Error: Shift not found.[/]");
            }
            else
            {
                userInterface.DisplayShiftsTable(shift.Data);
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Exception: {ex.Message}[/]");
        }
    }
    public async Task UpdateShift()
    {
        try
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new Rule("[bold yellow]Update Shift[/]").RuleStyle("yellow").Centered()
            );
            var shift = userInterface.UpdateShiftUi();
            var updatedShift = await shiftService.UpdateShift(shift);
            if (updatedShift == null)
            {
                AnsiConsole.MarkupLine("[red]Error: Failed to update shift.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[green]Shift updated successfully![/]");
                AnsiConsole.MarkupLine($"[green]Shift ID: {updatedShift.Data.ShiftId}[/]");
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Exception: {ex.Message}[/]");
        }
	}
}
