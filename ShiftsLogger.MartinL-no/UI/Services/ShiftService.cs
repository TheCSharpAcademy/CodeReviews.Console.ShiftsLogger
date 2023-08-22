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

            var startTimeString = AnsiConsole.Ask<String>("What did the shift start (enter in this format - yyyy-MM-dd HH:mm): ");
            var endTimeString = AnsiConsole.Ask<String>("What did the shift start (enter in this format - yyyy-MM-dd HH:mm): ");

            if (!InputValidator.AreValidDates(startTimeString, endTimeString))
            {
                ShowMessage("Invalid entry, please try again");
                continue;
            }

            var shift = new Shift();
            shift.StartTime = InputValidator.ParseDateTime(startTimeString);
            shift.EndTime = InputValidator.ParseDateTime(endTimeString);

            await ShiftDataAccess.InsertShift(shift);
        }
    }

    private static void ShowMessage(string message)
    {
        Console.WriteLine(message);
        Thread.Sleep(2000);
        Console.Clear();
    }
}