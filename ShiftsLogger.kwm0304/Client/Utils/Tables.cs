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
}
