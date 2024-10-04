using ShiftsLoggerUI.Models;
using Spectre.Console;

namespace ShiftsLoggerUI;
public class PresentationLayer
{
    static ShiftsLoggerHttpClient client = new ShiftsLoggerHttpClient();

    public static async Task<bool> ShowAllShifts()
    {
        List<Shift> shifts = await client.GetShifts();
        var table = new Table();
        table.Title = new TableTitle("All shifts");
        string[] columns = ["Id", "Date", "StartTime", "EndTime", "ShiftDuration"];
        table.AddColumns(columns);

        foreach (var shift in shifts)
        {
            table.AddRow(shift.Id.ToString(), shift.Date.ToString(), shift.StartTime.ToString(), shift.EndTime.ToString(), shift.ShiftDuration.ToString());
        }
        AnsiConsole.Write(table);
        Console.ReadLine();
        return false;
    }

    public static async void ShowSingleShift(string shiftId, string title)
    {
        Shift shift = await client.GetSingleShift(shiftId);
        var table = new Table();
        table.Title = new TableTitle(title);
        string[] columns = ["Id", "Date", "StartTime", "EndTime", "ShiftDuration"];
        table.AddColumns(columns);

        table.AddRow(shift.Id.ToString(), shift.Date.ToString(), shift.StartTime.ToString(), shift.EndTime.ToString(), shift.ShiftDuration.ToString());

        AnsiConsole.Write(table);
    }
}
