using ShiftsLoggerUI.Controllers;
using ShiftsLoggerUI.Helpers;
using ShiftsLoggerUI.Model;
using Spectre.Console;

namespace ShiftsLoggerUI.View;

public class Menu(ShiftController shiftController)
{
    
    public async Task MainMenu()
    {
        var end = true;
        while (end)
        {
            var pendingShift = await IsShiftPending();
            AnsiConsole.Write(new Rule("[olive]Here you can start your shift![/]"));
            var userInput = !pendingShift 
                ? UserInput.CreateChoosingList(["Show your shifts", "Start the shift", "Delete the shift - admin only"], "Exit") 
                : UserInput.CreateChoosingList(["Show your shifts", "End the shift", "Delete the shift - admin only"], "Exit");
            Console.Clear();
            switch (userInput)
            {
                case "Exit":
                    end = false;
                    break;
                case "Show your shifts":
                    await ShowShifts();
                    break;
                case "Start the shift":
                    await StartShift();
                    break;
                case "End the shift":
                    await EndShift();
                    break;
                case "Delete the shift - admin only":
                    await DeleteShift();
                    break;
            }
        }
    }

    private async Task DeleteShift()
    {
        var shifts = await shiftController.GetShifts();
        var deleteInput = UserInput.CreateShiftsChoosingList(shifts.Select(s => s.ToDto()).ToList(),new ShiftDto("Return", null, null, ""));
        if (deleteInput.startTime == "Return") return;
        await shiftController.Delete(deleteInput.id);
    }  
    
    private async Task StartShift()
    {
        var startTime = TimeOnly.FromDateTime(DateTime.Now);
        await shiftController.CreateShift(new Shift(startTime, null, null, DateOnly.FromDateTime(DateTime.Now)));
    }

    private async Task EndShift()
    {
        var endTime = TimeOnly.FromDateTime(DateTime.Now);
        var shifts = await shiftController.GetShifts();
        var lastShift = shifts.Last();

        var newShift = new Shift(lastShift.StartTime, endTime, null, DateOnly.FromDateTime(DateTime.Now))
        {
            Id = lastShift.Id
        };
        newShift.CalculateDuration();
        await shiftController.UpdateShift(newShift, lastShift.Id);
    }

    private async Task ShowShifts()
    {
        var shifts = await shiftController.GetShifts();
        var table = new Table();
        table.AddColumns(["Date", "Start", "End", "Duration"]).Centered();
        foreach (var shift in shifts)
        {
            table.AddRow($"{shift.Date}", $"{shift.StartTime}", $"{shift.EndTime}", $"{shift.Duration}");
        }
        AnsiConsole.Write(table);
        Validation.EndMessage("");
    }

    private async Task<bool> IsShiftPending()
    {
        var shift = await shiftController.GetShifts();
        try
        {
            var lastShift = shift.Last();
            return lastShift.EndTime == null;
        }
        catch(Exception e)
        {
            return false;
        }
    }
}
