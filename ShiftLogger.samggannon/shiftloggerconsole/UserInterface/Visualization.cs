using shiftloggerconsole.Models;
using Spectre.Console;

namespace shiftloggerconsole.UserInterface;

internal class Visualization
{
    internal static void ShowRow(Shift? shift)
    {
        Console.Clear();
        if(shift.Id != 0)
        {
            var table = new Table();
            table.AddColumn("Shift Id");
            table.AddColumn("Worker Id");
            table.AddColumn("Clock In Time");
            table.AddColumn("Clock Out Time");
            table.AddColumn("Shift Duration");

            table.AddRow(
                shift.Id.ToString(),
                shift.WorkerId.ToString(),
                shift.ClockIn.ToString(),
                shift.ClockOut.ToString(),
                shift.Duration
                );
        }
        else
        {
            Utilities.Utilities.InformUser(false, "Not a valid Id.");
        }
        
    }

    internal static void ShowTable(List<Shift>? shifts)
    {
        Console.Clear();
        var table = new Table();
        table.AddColumn("Shift Id");
        table.AddColumn("Worker Id");
        table.AddColumn("Clock In Time");
        table.AddColumn("Clock Out Time");
        table.AddColumn("Shift Duration");

        foreach (var shift in shifts)
        {
            table.AddRow(
                shift.Id.ToString(),
                shift.WorkerId.ToString(),
                shift.ClockIn.ToString(),
                shift.ClockOut.ToString(),
                shift.Duration
                );
        }

        AnsiConsole.Write(table);
    }
}
