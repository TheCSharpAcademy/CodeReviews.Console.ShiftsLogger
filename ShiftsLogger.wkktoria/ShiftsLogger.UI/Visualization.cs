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
        table.AddColumn("Started At");
        table.AddColumn("Finished At");
        table.AddColumn("Duration");

        foreach (var shift in shifts)
            table.AddRow(
                shift.WorkerName,
                shift.StartedAt.ToString(CultureInfo.InvariantCulture),
                shift.FinishedAt.ToString(CultureInfo.InvariantCulture),
                shift.Duration.ToString()
            );

        AnsiConsole.Write(table);
    }

    public static void ShowShiftDetails(ShiftViewDto shift)
    {
        var panel = new Panel($"""
                               Worker Name: {shift.WorkerName}
                               Started At: {shift.StartedAt}
                               Finished At: {shift.FinishedAt}
                               Duration: {shift.Duration}
                               """)
        {
            Header = new PanelHeader("Details"),
            Padding = new Padding(1)
        };

        AnsiConsole.Write(panel);
    }
}