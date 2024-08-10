using Shared;
using Spectre.Console;

namespace Client.Utils;

public class Tables
{
    public static void ShowShiftDetails(Shift shift)
    {
        var table = new Table()
      .Title("Current Shift Attributes")
      .Centered();
        table.AddColumns("Id", "Classification", "Start Time", "End Time");
        table.AddRow(shift.ShiftId.ToString(), shift.Classification.ToString(), shift.StartTime.ToString("yyyy-MM-dd hh:mm:ss"), shift.EndTime.ToString("yyyy-MM-dd hh:mm:ss"));
        AnsiConsole.Write(table);
    }

    public static void ShowEmployeeDetails(Employee employee)
    {
        var table = new Table()
            .Title("Current Employee Attributes")
            .Centered();
        table.AddColumns("Id", "Name", "Shift", "Pay/hr.");
        table.AddRow(employee.EmployeeId.ToString(), employee.Name!, employee.ShiftAssignment.ToString(), "$" + employee.PayRate.ToString());
        AnsiConsole.Write(table);
    }

    public static void ShowEmployeesForShift(string title, List<EmployeeShift> employeeShifts)
    {
        BaseTable<EmployeeShift> table = new(title, employeeShifts);
        table.Show();
        AnsiConsole.MarkupLine("[bold blue]Press any key to exit..[/]");
        Console.ReadKey(true);
    }

    public static void EmployeeShiftsTable(string title, string name, List<EmployeeShift> shifts)
    {
        if (shifts == null || shifts.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold red]No shifts to display.[/]");
            AnsiConsole.MarkupLine("[bold blue]Press any key to continue...[/]");
            Console.ReadKey(true);
            return;
        }
        var table = new Table()
            .Title(title)
            .Border(TableBorder.Markdown)
            .BorderStyle(new Style(foreground: Color.DarkCyan, decoration: Decoration.Bold));
        table.AddColumns("EmployeeId", "Employee", "ShiftId", "Started On", "Ended On", "Hours worked");
        foreach (var shift in shifts)
        {
            if (shift == null)
            {
                continue;
            }
            double hoursWorked = (shift.ClockOutTime - shift.ClockInTime).TotalHours;
            table.AddRow(
                shift.EmployeeId.ToString(),
                name,
                shift.ShiftId.ToString(),
                shift.ClockInTime.ToString("yyyy-MM-dd HH:mm:ss"),
                shift.ClockOutTime.ToString("yyyy-MM-dd HH:mm:ss"),
                hoursWorked.ToString());
        }
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[bold blue]Press any key to exit..[/]");
        Console.ReadKey(true);
    }

    public static void AllShiftsTable(string title, List<Shift> shifts)
    {
        var table = new Table()
        .Title(title)
        .Border(TableBorder.Markdown)
        .BorderStyle(new Style(foreground: Color.DarkCyan, decoration: Decoration.Bold));
        table.AddColumns("ShiftId", "Started On", "Ended On", "Classification");
        foreach (var shift in shifts)
        {
            table.AddRow(shift.ShiftId.ToString(), shift.StartTime.ToString("yyyy-MM-dd HH:mm:ss"), shift.EndTime.ToString("yyyy-MM-dd HH:mm:ss"), shift.Classification.ToString());
        }
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[bold blue]Press any key to exit..[/]");
        Console.ReadKey(true);
    }
}