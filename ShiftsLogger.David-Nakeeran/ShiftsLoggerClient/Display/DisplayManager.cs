using ShiftsLoggerClient.Models;
using ShiftsLoggerClient.Utilities;
using Spectre.Console;

namespace ShiftsLoggerClient.Display;

class DisplayManager
{
    private readonly CalculateDuration _calculateDuration;

    public DisplayManager(CalculateDuration calculateDuration)
    {
        _calculateDuration = calculateDuration;
    }

    internal void RenderGetAllEmployeesTable(List<EmployeeDTO> employees)
    {
        Console.Clear();
        var table = new Table();
        table.AddColumns("DisplayId", "Employee Name", "Shift Ids");
        int count = 1;
        foreach (var employee in employees)
        {
            var employeeName = employee.Name.ToString();

            string shiftId = string.Join(", ", employee.ShiftId);

            table.AddRow($"{count}", $"{employeeName}", $"{shiftId}");
            count++;
        }
        AnsiConsole.Write(table);
    }

    internal void RenderGetAllShiftsTable(List<ShiftDTO> shifts)
    {
        Console.Clear();
        var table = new Table();
        table.AddColumns("DisplayId", "Employee Name", "Shift Start", "Shift End", "Duration");
        int count = 1;
        foreach (var shift in shifts)
        {
            var duration = _calculateDuration.CalcDuration(shift.StartTime, shift.EndTime);
            var start = shift.StartTime.ToString("dd-MM-yyyy HH:mm");
            var end = shift.EndTime.ToString("dd-MM-yyyy HH:mm");
            table.AddRow($"{count}", $"{shift.Name}", $"{start}", $"{end}", $"{duration.ToString(@"hh\:mm")}");
            count++;
        }
        AnsiConsole.Write(table);
    }

    internal void RenderGetShiftByIdTable(ShiftDTO shift)
    {
        Console.Clear();
        var table = new Table();
        table.AddColumns("Employee Name", "Shift Start", "Shift End", "Duration");

        var duration = _calculateDuration.CalcDuration(shift.StartTime, shift.EndTime);
        var start = shift.StartTime.ToString("HH:mm");
        var end = shift.EndTime.ToString("HH:mm");
        table.AddRow($"{shift.Name}", $"{start}", $"{end}", $"{duration.ToString(@"hh\:mm")}");

        AnsiConsole.Write(table);
    }

    internal void ShowMessage(string message)
    {
        AnsiConsole.WriteLine(message);
    }

    internal void IncorrectId()
    {
        AnsiConsole.WriteLine("Id entered does not match, returning to main menu");
    }
}