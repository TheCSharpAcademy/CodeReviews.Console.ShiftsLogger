using Spectre.Console;
using ShiftsLogger.frockett.UI.Helpers;
using ShiftsLogger.frockett.UI.Dtos;
using System.Globalization;

namespace ShiftsLogger.frockett.UI.UI;

public class UserInput
{
    private readonly InputValidation inputValidation;

    public UserInput(InputValidation inputValidation)
    {
        this.inputValidation = inputValidation;
    }

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

        if (shiftStart > shiftEnd)
        {
            AnsiConsole.MarkupLine("[red]Invalid, shift must start before it ends[/]");
            shiftStart = GetDateTime("Shift start (format yyyy-MM-dd HH:mm): ");
            shiftStart = GetDateTime("Shift end (format yyyy-MM-dd HH:mm): ");
        }

        return new ShiftCreateDto() { StartTime = shiftStart, EndTime = shiftEnd, EmployeeId = employeeId };
    }
    public int GetShiftId()
    {
        int systemId = AnsiConsole.Ask<int>("Enter the System ID corresponding to the shift you'd like to delete: ");
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
