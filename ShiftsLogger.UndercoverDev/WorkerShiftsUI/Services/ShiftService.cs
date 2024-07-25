
using Spectre.Console;
using WorkerShiftsUI.UserInteractions;

namespace WorkerShiftsUI.Services;
public class ShiftService : IShiftService
{
    private readonly ApiService _apiService;

    public ShiftService(ApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task ViewShifts()
    {
        Console.Clear();
        var shifts = await _apiService.GetShiftsAsync();

        if (shifts == null || shifts.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold][red]No shifts found.[/][/]");
            return;
        }
        else
        {
            UserInteraction.ShowShifts(shifts);
        }
    }

    public async Task AddShift()
    {
        var workers = await _apiService.GetWorkersAsync();

        if (workers == null || workers.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold][red]No workers found. Please add workers before adding shifts.[/][/]");
            return;
        }

        AnsiConsole.MarkupLine("[bold][blue]Shift Details[/][/]");
        var shift = UserInteraction.GetShiftDetails();

        AnsiConsole.MarkupLine("[bold][blue]Select worker to attach shift to[/][/]");
        var worker = UserInteraction.GetWorkerOptionInput(workers);

        if (worker == null || worker.Name == "Back")
        {
            return;
        }

        shift.WorkerId = worker.WorkerId;
        shift.WorkerName = worker.Name;

        var createdShift = await _apiService.CreateShiftAsync(shift);

        if (createdShift == null)
        {
            AnsiConsole.MarkupLine("[bold][red]Failed to create shift.[/][/]");
            return;
        }
        else
        {
            Console.Clear();
            AnsiConsole.MarkupLine($"[bold][green]Shift created with ID: {createdShift.ShiftId} for {shift.WorkerName}[/][/]");
        }
    }

    public async Task UpdateShift()
    {
        Console.Clear();
        var shifts = await _apiService.GetShiftsAsync();
        var workers = await _apiService.GetWorkersAsync();

        if (shifts == null || shifts.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold][red]No shifts found.[/][/]");
            return;
        }

        AnsiConsole.MarkupLine("[bold][blue]Select worker whose shift to update[/][/]");
        var worker = UserInteraction.GetWorkerOptionInput(workers);

        if (worker == null || worker.Name == "Back")
        {
            return;
        }

        AnsiConsole.MarkupLine("[bold][blue]Select shift to update[/][/]");
        var shift = UserInteraction.GetShiftOptionInput(worker.Shifts);

        if (shift == null)
        {
            AnsiConsole.MarkupLine("[bold][red]This worker has no shift[/][/]");
            return;
        }

        AnsiConsole.MarkupLine("[bold][blue]Enter updated shift details[/][/]");

        var updatedShift = AnsiConsole.Confirm("Update Shift Times?") ?
            UserInteraction.GetShiftDetails() : shift;

        shift.StartTime = updatedShift.StartTime;
        shift.EndTime = updatedShift.EndTime;

        await _apiService.UpdateShiftAsync(shift.ShiftId, shift);
        Console.Clear();

        AnsiConsole.MarkupLine($"[bold][green]Shift updated for {shift.WorkerName}[/][/]");
    }

    public async Task DeleteShift()
    {
        Console.Clear();
        var shifts = await _apiService.GetShiftsAsync();
        var workers = await _apiService.GetWorkersAsync();

        if (shifts == null || shifts.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold][red]No shifts found.[/][/]");
            return;
        }

        AnsiConsole.MarkupLine("[bold][blue]Select worker whose shift to delete[/][/]");
        var worker = UserInteraction.GetWorkerOptionInput(workers);

        if (worker == null || worker.Name == "Back")
        {
            return;
        }

        AnsiConsole.MarkupLine("[bold][blue]Select shift to delete[/][/]");
        var shift = UserInteraction.GetShiftOptionInput(worker.Shifts);

        if (shift == null)
        {
            AnsiConsole.MarkupLine("[bold][red]This worker has no shifts allocated[/][/]");
            return;
        }

        await _apiService.DeleteShiftAsync(shift.ShiftId);

        Console.Clear();
        AnsiConsole.MarkupLine($"[bold][green]Shift deleted.[/][/]");
    }
}