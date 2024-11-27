using ShiftsLoggerUI.Controllers;
using ShiftsLoggerUI.Helpers;
using ShiftsLoggerUI.Model;
using Spectre.Console;

namespace ShiftsLoggerUI.View;

public class Menu(ShiftController shiftController)
{
    
    public async Task MainMenu()
    {
        var pendingShift = false;
        var end = true;
        while (end)
        {
            Console.Clear();
            var userInput = !pendingShift 
                ? UserInput.CreateChoosingList(["Show your shifts", "Start the shift", "Delete the shift - admin only"], "Exit") 
                : UserInput.CreateChoosingList(["Show your shifts", "End the shift", "Delete the shift - admin only"], "Exit");
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
                    pendingShift = true;
                    break;
                case "End the shift":
                    await EndShift();
                    pendingShift = false;
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
        Validation.EndMessage("You successfully deleted this shift.");
    }  
    
    private async Task StartShift()
    {
        TimeOnly startTime = TimeOnly.FromDateTime(DateTime.Now);
        await shiftController.CreateShift(new Shift(startTime, null, null, DateOnly.FromDateTime(DateTime.Now)));
        Validation.EndMessage("Your shift successfully started!");
    }

    private async Task EndShift()
    {
        TimeOnly endTime = TimeOnly.FromDateTime(DateTime.Now);
        var shifts = await shiftController.GetShifts();
        var lastShift = shifts.Last();

        var newShift = new Shift(lastShift.StartTime, endTime, null, DateOnly.FromDateTime(DateTime.Now))
        {
            Id = lastShift.Id
        };
        newShift.CalculateDuration();
        await shiftController.UpdateShift(newShift, lastShift.Id);
        Validation.EndMessage("Your shift successfully ended! Good Job!");
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
}