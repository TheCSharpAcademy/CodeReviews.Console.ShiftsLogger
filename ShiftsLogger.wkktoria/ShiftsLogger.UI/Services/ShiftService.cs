using ShiftsLogger.UI.Controllers;
using ShiftsLogger.UI.Models.DTOs;

namespace ShiftsLogger.UI.Services;

public static class ShiftService
{
    public static void ShowShifts()
    {
        var shifts = ShiftController.GetShifts();

        if (shifts == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Cannot get shifts.");
            Console.ResetColor();
        }
        else if (shifts.Any())
        {
            var shiftsForView = shifts.Select(shift =>
                new ShiftViewDto
                {
                    WorkerName = shift.WorkerName,
                    StartAt = shift.StartAt,
                    FinishAt = shift.FinishAt,
                    Duration = shift.Duration
                }).ToList();

            Visualization.ShowShiftsTable(shiftsForView);
        }
        else
        {
            Console.WriteLine("No shifts found.");
        }
    }
}