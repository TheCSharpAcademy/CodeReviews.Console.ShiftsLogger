using ShiftsLoogerUI.Records;
using ShiftsLoogerUI.Util;
using Spectre.Console;

namespace ShiftsLoogerUI;

enum MenuOptions
{
    ViewShifts,
    AddShift,
    ManageShift,
    Exit
}

public class Menu
{

    private ShiftService _shiftService { get; set; }

    public Menu()
    {
        _shiftService = new ShiftService();
    }
    public void display()
    {
        bool exit = false;
        do
        {
            var prompt = new SelectionPrompt<MenuOptions>()
                .AddChoices(
                    MenuOptions.ViewShifts,
                    MenuOptions.AddShift,
                    MenuOptions.ManageShift,
                    MenuOptions.Exit);
            var choice = AnsiConsole.Prompt(prompt);
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
        } while (!exit);
    }

    public void AddShift()
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

        _shiftService.AddShift(new Shift(Name: name, ShiftId: 0, ShiftStart: dateStart, ShiftEnd: dateEnd, comment));
    }
    public void DisplayShifts()
    {
        var shifts = _shiftService.GetShifts();
        var table = new Table();
        table.AddColumns("Shift Id","Name","Shift Start", "Shift End", "Duration in seconds");
        foreach (var shift in shifts)
        {
            table.AddRow(shift.ShiftId.ToString(), shift.Name, shift.ShiftStart.ToString(), shift.ShiftEnd.ToString(), shift.Duration.ToString());
        }
        
        AnsiConsole.Write(table);
    }

    private void ManageShifts()
    {
        var shifts = _shiftService.GetShifts();
        var prompt = new SelectionPrompt<Shift>().Title("Which shift would you like to manage?");
        foreach (var shift in shifts)
        {
            prompt.AddChoice(shift);
        }
        var choice = AnsiConsole.Prompt(prompt);
        
        
    }
}