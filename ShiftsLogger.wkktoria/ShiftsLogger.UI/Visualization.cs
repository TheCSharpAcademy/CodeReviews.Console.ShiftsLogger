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

        table.AddColumn("Worker");
        table.AddColumn("Started");
        table.AddColumn("Finished");

        foreach (var shift in shifts)
            table.AddRow(
                shift.WorkerName,
                shift.StartedAt.ToString(CultureInfo.InvariantCulture),
                shift.FinishedAt.ToString(CultureInfo.InvariantCulture)
            );

        AnsiConsole.Write(table);
    }

    public static void ShowShiftDetails(ShiftViewDetailsDto shift)
    {
        var panel = new Panel($"""
                               Worker: {shift.WorkerName}
                               Started: {shift.StartedAt}
                               Finished: {shift.FinishedAt}
                               Duration: {shift.Duration}
                               """)
        {
            Header = new PanelHeader("Details"),
            Padding = new Padding(1)
        };

        AnsiConsole.Write(panel);
    }
}