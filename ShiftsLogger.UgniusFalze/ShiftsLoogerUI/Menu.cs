using System.Globalization;
using System.Text.RegularExpressions;
using ShiftsLoogerUI.Records;
using ShiftsLoogerUI.Util;
using Spectre.Console;

namespace ShiftsLoogerUI;

internal enum MenuOptions
{
    ViewShifts,
    AddShift,
    ManageShift,
    Exit
}

internal enum ModifyShift
{
    DeleteShift,
    ChangeName,
    ChangeComment,
    ChangeStartDate,
    ChangeEndDate,
    Exit
}

public class Menu
{

    private ShiftService ShiftService { get; set; } = new();

    public void Display()
    {
        var exit = false;
        do
        {
            AnsiConsole.Clear();
            var prompt = new SelectionPrompt<MenuOptions>()
                .AddChoices(
                    MenuOptions.ViewShifts,
                    MenuOptions.AddShift,
                    MenuOptions.ManageShift,
                    MenuOptions.Exit);
            prompt.Converter =
                options => Regex.Replace(options.ToString(), "(\\B[A-Z])",
                    " $1");
            var choice = AnsiConsole.Prompt(prompt);
            try
            {
                switch (choice)
                {
                    case MenuOptions.ViewShifts:
                        DisplayShifts();
                        break;
                    case MenuOptions.AddShift:
                        AddShift();
                        break;
                    case MenuOptions.ManageShift:
                        ManageShifts();
                        break;
                    case MenuOptions.Exit:
                        exit = true;
                        break;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Failed to connect to the api. Press any key to restart..");
                Console.ReadKey();
            }
            
        } while (!exit);
    }

    private void AddShift()
    {
        var name = UserInput.GetName();
        var dateStart = UserInput.GetStartDate();
        var dateEnd = UserInput.GetEndDate(dateStart);
        var prompt = new SelectionPrompt<string>().AddChoices("Yes", "No").Title("Do you want to add comment?");
        var choice = AnsiConsole.Prompt(prompt);
        string? comment = null;
        switch (choice)
        {
            case "Yes":
                comment = UserInput.GetComment();
                break;
            case "No":
                break;
        }

        Console.WriteLine(ShiftService.AddShift(new Shift(name, 0, dateStart, dateEnd,
            comment)) == false
            ? "Failed to insert shift."
            : "New shift inserted.");
        UserInput.GetKeyToContinue();
    }

    private void DisplayShifts()
    {
        var shifts = ShiftService.GetShifts();
        if (shifts.Count == 0)
        {
            Console.WriteLine("There are no shifts to manage. Press any key to continue.");
            Console.ReadKey();
            return;
        }
        var table = new Table();
        table.AddColumns("Shift Id","Name","Shift Start", "Shift End", "Duration in hours");
        foreach (var shift in shifts)
        {
            table.AddRow(shift.ShiftId.ToString(), shift.Name, shift.ShiftStart.ToString(CultureInfo.CurrentCulture), shift.ShiftEnd.ToString(CultureInfo.CurrentCulture), shift.Duration.ToString(CultureInfo.CurrentCulture));
        }
        
        AnsiConsole.Write(table);
        UserInput.GetKeyToContinue();
    }

    private void ManageShifts()
    {
        var shifts = ShiftService.GetShifts();
        if (shifts.Count == 0)
        {
            Console.WriteLine("There are no shifts to manage. Press any key to continue.");
            Console.ReadKey();
            return;
        }
        var prompt = new SelectionPrompt<Shift>().Title("Which shift would you like to manage?");
        prompt.Converter = shift => shift.ShiftId + " " + shift.Name;
        foreach (var shift in shifts)
        {
            prompt.AddChoice(shift);
        }
        var choice = AnsiConsole.Prompt(prompt);
        
        AnsiConsole.Clear();
        var modifyPrompt = new SelectionPrompt<ModifyShift>()
            .AddChoices(
                ModifyShift.ChangeName,
                ModifyShift.ChangeComment,
                ModifyShift.ChangeStartDate,
                ModifyShift.ChangeEndDate,
                ModifyShift.DeleteShift,
                ModifyShift.Exit);
        modifyPrompt.Converter =
            options => Regex.Replace(options.ToString(), "(\\B[A-Z])",
                " $1");
        var modifyChoice = AnsiConsole.Prompt(modifyPrompt);
        var success = false;
        var successMessage = "Shift update successfully.";
        switch (modifyChoice)
        {
            case ModifyShift.ChangeName:
                var name = UserInput.GetName();
                choice.Name = name;
                success = ShiftService.UpdateShift(choice);
                break;
            case ModifyShift.ChangeComment:
                var comment = UserInput.GetComment();
                choice.Comment = comment;
                success = ShiftService.UpdateShift(choice);
                break;
            case ModifyShift.ChangeStartDate:
                var startDate = UserInput.GetStartDate(choice.ShiftEnd);
                choice.ShiftStart = startDate;
                success = ShiftService.UpdateShift(choice);
                break;
            case ModifyShift.ChangeEndDate:
                var endDate = UserInput.GetEndDate(choice.ShiftStart);
                choice.ShiftEnd = endDate;
                success = ShiftService.UpdateShift(choice);
                break;
            case ModifyShift.DeleteShift:
                success = ShiftService.DeleteShift(choice);
                successMessage = "Shift deleted successfully.";
                break;
            case ModifyShift.Exit:
                return;
        }

        Console.WriteLine(success ? successMessage : "Failed to update shift.");
        UserInput.GetKeyToContinue();
    }
}