using Spectre.Console;
using ShiftsLogger.frockett.UI.Dtos;
using System.Globalization;

namespace ShiftsLogger.frockett.UI.UI;

public class UserInput
{
    public EmployeeCreateDto GetNewEmployee()
    {
        string name = AnsiConsole.Ask<string>("Enter employee's name: ");

        EmployeeCreateDto employee = new EmployeeCreateDto();
        employee.Name = name;
        return employee;
    }

    public ShiftCreateDto GetNewShift(int employeeId)
    {
        DateTime shiftStart = GetDateTime("Shift start (format yyyy-MM-dd HH:mm): ");
        DateTime shiftEnd = GetDateTime("Shift end (format yyyy-MM-dd HH:mm): ");

        while (shiftStart > shiftEnd)
        {
            AnsiConsole.MarkupLine("[red]Invalid, shift must start before it ends[/]");
            shiftStart = GetDateTime("Shift start (format yyyy-MM-dd HH:mm): ");
            shiftEnd = GetDateTime("Shift end (format yyyy-MM-dd HH:mm): ");
        }

        return new ShiftCreateDto() { StartTime = shiftStart, EndTime = shiftEnd, EmployeeId = employeeId };
    }

    public ShiftDto GetUpdatedShift(int shiftId)
    {
        DateTime shiftStart = GetDateTime("Shift start (format yyyy-MM-dd HH:mm): ");
        DateTime shiftEnd = GetDateTime("Shift end (format yyyy-MM-dd HH:mm): ");

        if (shiftStart > shiftEnd)
        {
            AnsiConsole.MarkupLine("[red]Invalid, shift must start before it ends[/]");
            shiftStart = GetDateTime("Shift start (format yyyy-MM-dd HH:mm): ");
            shiftStart = GetDateTime("Shift end (format yyyy-MM-dd HH:mm): ");
        }

        return new ShiftDto() { StartTime = shiftStart, EndTime= shiftEnd, Id = shiftId };
    }

    public EmployeeDto GetUpdatedEmployee(EmployeeDto employeeToUpdate)
    {
        string name = AnsiConsole.Ask<string>("Enter new name: ");

        employeeToUpdate.Name = name;
        return employeeToUpdate;
    }
    public int GetShiftId(string prompt)
    {
        int systemId = AnsiConsole.Ask<int>(prompt);
        return systemId;
    }

    private DateTime GetDateTime(string prompt)
    {
        DateTime dateTime;
        string validFormat = "yyyy-MM-dd HH:mm";

        var sDateTime = AnsiConsole.Ask<string>(prompt);

        while (!DateTime.TryParseExact(sDateTime, validFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
        {
            AnsiConsole.WriteLine("\nIncorrect date/time format.");
            sDateTime = AnsiConsole.Ask<string>(prompt);
        }

        return dateTime;
    }

}
