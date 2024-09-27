using Spectre.Console;
using ShiftsLoggerUI.Models;

namespace ShiftsLoggerUI;

public class UserInput
{
    static ShiftsLoggerHttpClient client = new ShiftsLoggerHttpClient();

    public static string ShowMenu()
    {
        List<string> menuOptions = ["Add a shift", "Delete a shift", "Edit a shift", "View all shifts", "Exit"];
        return AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose an option:")
            .AddChoices(menuOptions));
    }
    
    public static async Task<bool> SwitchMenuChoice(string menuChoice)
    {
        switch (menuChoice)
        {
            case "Add a shift":
                Shift shift = new();
                return await client.PostShift(EnterShiftProperties(shift));
            case "Delete a shift":
                string shiftId = await ChooseShift();
                PresentationLayer.ShowSingleShift(shiftId, "Deleting");
                return await client.DeleteShift(shiftId);
            case "Edit a shift":
                shiftId = await ChooseShift();
                PresentationLayer.ShowSingleShift(shiftId, "Editing");
                return await client.EditShift(shiftId);
            case "View all shifts":
                return await PresentationLayer.ShowAllShifts();
            case "Exit":
                return true;
        }
        return false;
    }

    public static async Task<string> ChooseShift()
    {
        List<Shift> shifts = await client.GetShifts();
        List<string> choices = [];
        foreach (var shift in shifts)
        {
            choices.Add($"{shift.Id}, {shift.Date}, {shift.StartTime}, {shift.EndTime}, {shift.ShiftDuration}");
        }

        string userChoice = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose a shift:")
            .AddChoices(choices));

        return userChoice.Remove(userChoice.IndexOf(","));
    }  

    public static Shift EnterShiftProperties(Shift shift)
    {
        shift.Date = Validator.ValidateDate();
        shift.StartTime = Validator.ValidateTime("start");
        shift.EndTime = Validator.ValidateTime("end");
        return shift;
    }
}
