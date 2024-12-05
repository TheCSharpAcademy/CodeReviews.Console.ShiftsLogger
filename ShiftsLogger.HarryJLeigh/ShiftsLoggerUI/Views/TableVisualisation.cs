using Shared.Models;
using Spectre.Console;

namespace ShiftsLoggerUI.Views;

public static class TableVisualisation
{
    internal static void DisplayTable(List<Shift>? shifts)
    {
        if (shifts == null || shifts.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold red]No shifts were found.[/]");
            return;
        }
        var table = new Table();
        table.AddColumns("ID", "Start Time", "End Time", "Hours");

        foreach (Shift shift in shifts)
        {
            table.AddRow(
                shift.Id.ToString(),
                shift.StartTime.ToString("yyyy-MM-dd HH:mm"),
                shift.EndTime.ToString("yyyy-MM-dd HH:mm"),
                shift.ShiftTime.ToString()
            );
        }
        AnsiConsole.Write(table);
    }
}