using Spectre.Console;
using UI.Models;
using UI.Service;

namespace UI.View;

internal class ViewShifts
{
    public static void Show(List<Shift> shifts)
    {

        Table table = new();
        table.BorderStyle = new Style(Color.Green1);
        table.AddColumn("ID");
        table.AddColumn("Employee Name");
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Duration");

        foreach (var shift in shifts)
        {
            try
            {
                table.AddRow(shift.Id.ToString(), shift.Employee, shift.StartTime.ToString(), shift.EndTime.ToString(), shift.Duration.ToString());
            }
            catch (Exception e)
            {
                AnsiConsole.MarkupLine($"[Red]There was a problem rendering shift Id {shift.Id} data[/]");
                AnsiConsole.MarkupLine($"[Red]{e.Message}\n\n[/]");
            }
        }

        AnsiConsole.Write(table);
        Console.WriteLine("\n\n");
        AnsiConsole.WriteLine("Press any key to return");
        Console.ReadLine();
    }
}
