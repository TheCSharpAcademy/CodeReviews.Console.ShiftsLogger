using System.Globalization;
using ShiftsLogger.UI.Models.DTOs;
using Spectre.Console;

namespace ShiftsLogger.UI;

public static class Visualization
{
    public static void ShowShiftsTable(List<ShiftViewDto> shifts)
    {
        var table = new Table
        {
            Title = new TableTitle("Shifts")
        };

        table.AddColumn("Worker Name");
        table.AddColumn("Start At");
        table.AddColumn("Finish At");
        table.AddColumn("Duration");

        foreach (var shift in shifts)
            table.AddRow(
                shift.WorkerName,
                shift.StartAt.ToString(CultureInfo.InvariantCulture),
                shift.FinishAt.ToString(CultureInfo.InvariantCulture),
                shift.Duration.ToString()
            );

        AnsiConsole.Write(table);
    }
}