using ShiftLoggerConsoleApp.Data;
using ShiftLoggerConsoleApp.Util;
using Spectre.Console;
using System.Text.Json;

namespace ShiftLoggerConsoleApp.Service;

public static class ShiftLoggerServiceHelper
{
    public static DateTime GetValidDate()
    {
        var dateStr = AnsiConsole.Ask<string>("Shift date (format: yyyy-MM-dd):");
        DateTime date;
        while (!Validator.IsValidDate(dateStr, out date))
        {
            dateStr = AnsiConsole.Ask<string>("Shift date (format: yyyy-MM-dd):");
        }

        return date;
    }

    public static TimeSpan GetValidEndTime(TimeSpan startTime)
    {
        TimeSpan endTime = GetValidTime();
        while (endTime < startTime)
        {
            Console.WriteLine($"End time should late than start time {startTime}.");
            var endTimeStr = AnsiConsole.Ask<string>("End time (format: hh:mm:ss): ");
            while (!Validator.IsValidTime(endTimeStr, out endTime))
            {
                endTimeStr = AnsiConsole.Ask<string>("End time (format: hh:mm:ss): ");
            }
        }
        return endTime;
    }

    public static TimeSpan GetValidTime()
    {
        var timeStr = AnsiConsole.Ask<string>("Shift time (format: hh:mm:ss): ");
        TimeSpan time;
        while (!Validator.IsValidTime(timeStr, out time))
        {
            timeStr = AnsiConsole.Ask<string>("Shift time (format: hh:mm:ss): ");
        }
        return time;
    }

    public static object InputShift()
    {
        var name = AnsiConsole.Ask<string>("Empolyee's name:");
        Console.WriteLine("Plase type date");
        DateTime date = ShiftLoggerServiceHelper.GetValidDate();
        Console.WriteLine("Plase type start time");
        TimeSpan startTime = ShiftLoggerServiceHelper.GetValidTime();
        Console.WriteLine("Plase type end time");
        TimeSpan endTime = ShiftLoggerServiceHelper.GetValidEndTime(startTime);
        var shift = new { EmployeeName = name, ShiftDate = date, ShiftStartTime = startTime, ShiftEndTime = endTime };
        return shift;
    }

    public static async Task<string> GetJson(object shift)
    {
        using (var stream = new MemoryStream())
        {
            await JsonSerializer.SerializeAsync(stream, shift, shift.GetType());
            stream.Position = 0;
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }
    }

    public static string SelectEmpolyeeName(List<Shift> shifts)
    {
        List<string> uniqueNames = shifts.Select(shift => shift.EmployeeName).Distinct().ToList();

        var selectedName = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Please choose the empolyee you like to operate?")
                .AddChoices(uniqueNames));

        return selectedName;
    }
}
