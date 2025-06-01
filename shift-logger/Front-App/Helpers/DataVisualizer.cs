using API.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front_App.Helpers;

public static class DataVisualizer
{
    public static void DisplayShifts(IEnumerable<Shift> shifts)
    {
        if (shifts == null || !shifts.Any())
        {
            AnsiConsole.WriteLine("[Red] No shifts available.[/]");
            return;
        }
        var table = new Table();
        table.AddColumn(new TableColumn("Shift ID").Centered());
        table.AddColumn(new TableColumn("Start").Centered());
        table.AddColumn(new TableColumn("End").Centered());
        table.AddColumn(new TableColumn("Duration").Centered());
        table.AddColumn(new TableColumn("Employee Name").Centered());
        foreach (var shift in shifts)
        {
            table.AddRow(
                shift.Id.ToString(),
                shift.Start.ToString("g"),
                shift.End.ToString("g"),
                shift.Duration.ToString("g"),
                shift.EmployeeName);
        }
        AnsiConsole.Write(table);
    }
}