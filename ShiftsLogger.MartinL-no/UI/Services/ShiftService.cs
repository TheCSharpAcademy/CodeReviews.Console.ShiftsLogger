using Spectre.Console;

using UI.DAL;
using UI.Models;

namespace UI.Services;

internal static class ShiftService
{
    public static async Task InsertShiftAsync()
    {
        while (true)
        {
            Console.Clear();

            var startTime = GetDateTime("When did the shift start (enter in this format - yyyy-MM-dd HH:mm): ");
            var endTime = GetDateTime("When did the shift start (enter in this format - yyyy-MM-dd HH:mm): ");

            if (startTime > endTime)
            {
                ShowMessage("Invalid entry, please try again");
                continue;
            }

            var shift = new Shift();
            shift.StartTime = startTime;
            shift.EndTime = endTime;

            await ShiftDataAccess.InsertShift(shift);
            ShowMessage("Shift added!");
            break;
        }
    }

    public static async Task UpdateShiftAsync()
    {
        var shift = await GetShiftOptionInputAsync();
        DateTime startTime;
        DateTime endTime;

        while (true)
        {
            Console.Clear();
            UserInterface.ShowShifts(shift);

            startTime = AnsiConsole.Confirm("Update start time?") ? GetDateTime("What is the new time (enter in this format - yyyy-MM-dd HH:mm): ") : shift.StartTime;
            endTime = AnsiConsole.Confirm("Update end time?") ? GetDateTime("What is the new time (enter in this format - yyyy-MM-dd HH:mm): ") : shift.EndTime;

            if (startTime < endTime) break;
            else ShowMessage("Start time must be before end time");
        }

        shift.StartTime = startTime;
        shift.EndTime = endTime;
        await ShiftDataAccess.UpdateShift(shift.Id, shift);

        ShowMessage("Shift updated!");
    }

    public static async Task DeleteShiftAsync()
    {
        var shift = await GetShiftOptionInputAsync();

        await ShiftDataAccess.DeleteShift(shift.Id);
        ShowMessage("Shift deleted!");
    }

    public static async Task ViewAllShiftsAsync()
    {
        var shifts = await ShiftDataAccess.GetShifts();
        UserInterface.ShowShifts(shifts);
    }

    private static async Task<Shift> GetShiftOptionInputAsync()
    {
        var shifts = await ShiftDataAccess.GetShifts();
        var shiftsArray = shifts.Select(x => $"{x.Id, -5}: {x.StartTime.ToString("yyyy-MM-dd HH:mm"), 5} - {x.EndTime.ToString("yyyy-MM-dd HH:mm")}");
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose Shift")
            .AddChoices(shiftsArray));
        var id = Int32.Parse(option.Split(' ')[0]);

        return await ShiftDataAccess.GetShift(id);
    }

    private static DateTime GetDateTime(string prompt)
    {
        DateTime dateTime;

        while (true)
        {
            var dateTimeString = AnsiConsole.Ask<String>(prompt);
            if (InputValidator.IsValidDate(dateTimeString, out dateTime))
            {
                return dateTime;
            }

            Console.WriteLine("Invalid entry, please try again");
        }
    }

    private static void ShowMessage(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
        Thread.Sleep(2000);
    }
}