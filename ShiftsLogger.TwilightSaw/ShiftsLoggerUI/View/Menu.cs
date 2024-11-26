using ShiftsLoggerUI.Controllers;
using ShiftsLoggerUI.Helpers;
using ShiftsLoggerUI.Model;
using Spectre.Console;

namespace ShiftsLoggerUI.View;

public class Menu(ShiftController shiftController)
{
    public async Task MainMenu()
    {
        while (true)
        {
            Console.Clear();
            var userInput = UserInput.CreateChoosingList(["Show your shifts", "Start a shift"], "Exit");
            switch (userInput)
            {
                case "Exit": break;
                case "Show your shifts":
                    var shifts = await shiftController.GetShifts();
                    var table = new Table();
                    table.AddColumns(["Date", "Start", "End", "Duration"]).Centered();
                    foreach (var shift in shifts)
                    {
                        table.AddRow($"{shift.Date}", $"{shift.StartTime}", $"{shift.EndTime}", $"{shift.Duration}");
                    }
                    AnsiConsole.Write(table);
                    Validation.EndMessage("");
                    break;
                case "Start a shift":
                    await StartShift();
                    break;
            }
        }
    }

    private async Task StartShift()
    {
        TimeOnly startTime = TimeOnly.FromDateTime(DateTime.Now);
        await shiftController.CreateShift(new Shift(startTime, null, null, DateOnly.FromDateTime(DateTime.Now)));
        Validation.EndMessage("");
    }
}