using shiftloggerconsole.Models;
using Spectre.Console;

namespace shiftloggerconsole.UserInterface;

internal class Visualization
{
    internal static void ShowTable(List<Shift>? shifts)
    {
        Console.Clear();
        var table = new Table();
        table.AddColumn("Shhift Id");
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
                "8:00:00"
                );
        }

        AnsiConsole.Write(table);
        Console.WriteLine("Press [enter] to continue");

        Utilities.Utilities.ConfirmKey();
    }
}
