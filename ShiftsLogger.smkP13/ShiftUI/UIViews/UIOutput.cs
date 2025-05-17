using ShiftUI.UIModels;
using Spectre.Console;

namespace ShiftUI.UIViews;

class UIOutput
{
    internal static void PrintAllShifts(List<UIShift> shifts)
    {
        Panel panel;
        Table table;
        if(shifts.Count == 0)
        {
            AnsiConsole.WriteLine("No shift registered");
            return;
        }

        foreach (UIShift shift in shifts)
        {
            table = new();
            table.AddColumns("", "Date", "Time");
            table.AddRow("Start", shift.startTime.ToString("yyyy.MM.dd"), shift.startTime.ToString("hh:mm:ss"));
            table.AddRow("End", shift.endTime.ToString("yyyy.MM.dd"), shift.endTime.ToString("hh:mm:ss"));
            panel = new(table);
            panel.Header($"Shift:{shift.id} / User: {shift.user.firstName} - {shift.user.lastName}");
            panel.DoubleBorder();
            AnsiConsole.Write(panel);
            AnsiConsole.WriteLine();
        }
    }

    internal static void PrintAllUsers(List<UIUser>? users)
    {
        Panel panel;
        string isActive;
        if(users.Count == 0)
        {
            AnsiConsole.WriteLine("No user registered.");
            return;
        }

        foreach (UIUser user in users)
        {
            isActive = user.isActive ? "Yes" : "No";
            panel = new($"First name: {user.firstName}\nLast name: {user.lastName}\nIs active: {isActive}");
            panel.Header($"User id: {user.userId}");
            AnsiConsole.Write(panel);
            AnsiConsole.WriteLine();
        }
    }

    internal static void PrintShiftForSingleUser(UIUser user)
    {
        Panel panel;
        if (user.Shifts.Count == 0)
        { panel = new("No Data found for this user.");
            AnsiConsole.Write(panel);
            return;
        }

        Table table = new();
        table.AddColumns("Id", "Start", "End");
        table.ShowRowSeparators();
        foreach (UIShift shift in user.Shifts)
        {
            table.AddRow(shift.id.ToString(), shift.startTime.ToString("yyyy.MM.dd hh:mm:ss"), shift.endTime.ToString("yyyy.MM.dd hh:mm:ss"));
        }
        panel = new(table);
        panel.Header($"{user.lastName}  -  {user.firstName}");
        AnsiConsole.Write(panel);
        AnsiConsole.WriteLine();
    }

    internal static void PrintSingleUser(UIUser user)
    {
        Panel panel;
        if (user.Shifts.Count != 0)
        {
            string? firstShiftDate = user.Shifts.FirstOrDefault().startTime.ToString("yyyy.MM.dd");
            string? lastShiftDate = user.Shifts.LastOrDefault().endTime.ToString("yyyy.MM.dd");
            panel = new($"First name: {user.firstName}\nLast name: {user.lastName}\nFirst shift: {firstShiftDate}\nLast shift: {lastShiftDate}\nTotal shifts: {user.Shifts.Count}");
        }
        else
        {
            panel = new($"First name: {user.firstName}\nLast name: {user.lastName}\nUser has no shift registered yet");
        }
        panel.Header($"User id: {user.userId}");
        AnsiConsole.Write(panel);
    }
}