using ShiftsLogger.Dejmenek.UI.Models;
using Spectre.Console;

namespace ShiftsLogger.Dejmenek.UI.Helpers;
public static class DataVisualizer
{
    public static void DisplayShifts(List<ShiftReadDTO>? shifts)
    {
        if (shifts?.Count == 0 || shifts is null)
        {
            AnsiConsole.MarkupLine("No shifts found.");
            return;
        }

        var table = new Table().Title("SHIFTS");

        table.AddColumn("Employee First Name");
        table.AddColumn("Employee Last Name");
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Duration in minutes");

        foreach (var shift in shifts)
        {
            table.AddRow(shift.EmployeeFirstName, shift.EmployeeLastName, shift.StartTime.ToString("yyyy-MM-dd HH:mm"), shift.EndTime.ToString("yyyy-MM-dd HH:mm"), shift.Duration);
        }

        AnsiConsole.Write(table);
    }

    public static void DisplayEmployeeShifts(List<ShiftReadDTO>? shifts)
    {
        if (shifts?.Count == 0 || shifts is null)
        {
            AnsiConsole.MarkupLine("No shifts found.");
            return;
        }

        var table = new Table().Title($"{shifts[0].EmployeeFirstName.ToUpper()} {shifts[0].EmployeeLastName.ToUpper()} SHIFTS");

        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Duration in minutes");

        foreach (var shift in shifts)
        {
            table.AddRow(shift.StartTime.ToString("yyyy-MM-dd HH:mm"), shift.EndTime.ToString("yyyy-MM-dd HH:mm"), shift.Duration);
        }

        AnsiConsole.Write(table);
    }

    public static void DisplayEmployees(List<EmployeeReadDTO>? employees)
    {
        if (employees?.Count == 0 || employees is null)
        {
            AnsiConsole.MarkupLine("No employees found.");
            return;
        }

        var table = new Table().Title("EMPLOYEES");

        table.AddColumn("First Name");
        table.AddColumn("Last Name");

        foreach (var employee in employees)
        {
            table.AddRow(employee.FirstName, employee.LastName);
        }

        AnsiConsole.Write(table);
    }
}
