using ShiftLoggerConsoleApp.Data;
using Spectre.Console;

namespace ShiftLoggerConsoleApp.UI;

public class UserInterface
{
    public static void ShowShifts(List<Shift> shifts)
    {
        if (shifts is null || shifts.Count == 0)
        {
            Console.WriteLine("No Shift.");
            return;
        }

        var table = new Table();
        table.Title("Shifts Info");
        table.AddColumn("Id");
        table.AddColumn("EmplyeeName");
        table.AddColumn("ShiftDate");
        table.AddColumn("ShiftStartTime");
        table.AddColumn("ShiftEndTime");
        table.AddColumn("TotalHoursWorked");

        foreach (Shift shift in shifts)
        {
            table.AddRow(shift.Id.ToString(), shift.EmployeeName, shift.ShiftDate.ToShortDateString(),
                shift.ShiftStartTime.ToString(), shift.ShiftEndTime.ToString(),
                $"{(int)shift.TotalHoursWorked}h");
        }

        AnsiConsole.Write(table);
    }

    public static void ShowShift(Shift shift)
    {
        if (shift == null)
        {
            Console.WriteLine("No Shift.");
            return;
        }

        var panel = new Panel($"{shift.EmployeeName}: {shift.ShiftDate.ToShortDateString()} " +
                                $"{shift.ShiftStartTime} - {shift.ShiftEndTime} {(int)shift.TotalHoursWorked}h")
            .Header("[bold]Employee Shift Info[/]")
            .BorderColor(Color.Blue);

        panel.Padding(2, 2, 2, 2);

        AnsiConsole.Write(panel);
    }

    public static void BackToMainMenuPrompt()
    {
        Console.WriteLine("Press any key to go back to Main Menu");
        Console.ReadLine();
        Console.Clear();
    }
}

