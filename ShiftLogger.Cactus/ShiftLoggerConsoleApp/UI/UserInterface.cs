using Spectre.Console;

namespace ShiftLoggerConsoleApp.UI;

public class UserInterface
{
    public static void ShowShifts(List<Shift> shifts)
    {
        if (shifts is null || shifts.Count == 0) return;

        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("EmplyeeName");
        table.AddColumn("ShiftDate");
        table.AddColumn("ShiftStartTime");
        table.AddColumn("ShiftEndTime");
        table.AddColumn("TotalHoursWorked");

        foreach (Shift shift in shifts)
        {
            table.AddRow(shift.Id.ToString(), shift.EmployeeName, shift.ShiftDate.ToString(),
                shift.ShiftStartTime.ToString(), shift.ShiftEndTime.ToString(),
                shift.TotalHoursWorked.ToString());
        }

        AnsiConsole.Write(table);

        Console.WriteLine("Press any key to go back to Main Menu");
        Console.ReadLine();
        Console.Clear();
    }

    public static void ShowShift(Shift shift)
    {
        if (shift == null) return;

        var panel = new Panel($@"ShiftId: {shift.Id}  EmployeeName: {shift.EmployeeName}  
                    ShiftDate: {shift.ShiftDate}  ShiftStartTime: {shift.ShiftStartTime}
                    ShiftEndTime: {shift.ShiftEndTime}  TotalHoursWorked: {shift.TotalHoursWorked}"
                    );
        panel.Header = new PanelHeader("Contact Info");
        panel.Padding = new Padding(2, 2, 2, 2);

        AnsiConsole.Write(panel);

        BackToMainMenuPrompt();
    }

    public static void BackToMainMenuPrompt()
    {
        Console.WriteLine("Press any key to go back to Main Menu");
        Console.ReadLine();
        Console.Clear();
    }
}

