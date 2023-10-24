using ShiftsLogger.UI.Controllers;
using ShiftsLogger.UI.Models.DTOs;
using Spectre.Console;

namespace ShiftsLogger.UI.Services;

public static class ShiftService
{
    public static void ShowShifts()
    {
        var shifts = ShiftController.GetShifts();

        if (shifts == null) return;

        if (shifts.Any())
        {
            var shiftsForView = shifts.Select(shift =>
                new ShiftViewDto
                {
                    WorkerName = shift.WorkerName,
                    StartedAt = shift.StartedAt,
                    FinishedAt = shift.FinishedAt,
                    Duration = shift.Duration
                }).ToList();

            Visualization.ShowShiftsTable(shiftsForView);
        }
        else
        {
            Console.WriteLine("No logged shifts.");
        }
    }

    public static void ShowShiftDetails()
    {
        var shiftId = GetShiftIdInput();

        if (shiftId == null) return;

        if (shiftId != long.MinValue)
        {
            var shift = ShiftController.GetShiftById(shiftId);
            var shiftForView = new ShiftViewDto
            {
                WorkerName = shift!.WorkerName,
                StartedAt = shift.StartedAt,
                FinishedAt = shift.FinishedAt,
                Duration = shift.Duration
            };

            Visualization.ShowShiftDetails(shiftForView);
        }
        else
        {
            Console.WriteLine("No logged shifts.");
        }
    }

    private static long? GetShiftIdInput()
    {
        var shifts = ShiftController.GetShifts();
        if (shifts == null) return null;
        if (!shifts.Any()) return long.MinValue;

        var shiftsOptions = shifts.Select(shift =>
            new ShiftViewDto
            {
                WorkerName = shift.WorkerName,
                StartedAt = shift.StartedAt,
                FinishedAt = shift.FinishedAt,
                Duration = shift.Duration
            }).ToList();

        var selected = AnsiConsole.Prompt(new SelectionPrompt<ShiftViewDto>()
            .Title("Choose shift")
            .AddChoices(shiftsOptions));
        var id = shifts.Find(shift =>
            shift.WorkerName == selected.WorkerName &&
            shift.StartedAt == selected.StartedAt &&
            shift.FinishedAt == selected.FinishedAt)!.Id;

        return id;
    }
}