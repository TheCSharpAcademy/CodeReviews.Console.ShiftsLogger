using Spectre.Console;

namespace ShiftsLoogerUI;

enum MenuOptions
{
    ViewShifts,
    AddShift,
    ManageShift,
    DeleteShift,
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
                    MenuOptions.DeleteShift,
                    MenuOptions.Exit);
            var choice = AnsiConsole.Prompt(prompt);
            switch (choice)
            {
                case MenuOptions.ViewShifts:
                    DisplayShifts();
                    break;
                case MenuOptions.Exit:
                    exit = true;
                    break;
            }
        } while (!exit);
    }

    public void DisplayShifts()
    {
        var shifts = _shiftService.GetShifts();
        var table = new Table();
        table.AddColumns("Shift Id","Name","Shift Start", "Shift End", "Duration");
        foreach (var shift in shifts)
        {
            table.AddRow(shift.ShiftId.ToString(), shift.Name, shift.ShiftStart.ToString(), shift.ShiftEnd.ToString(), shift.Duration.ToString());
        }
        
        AnsiConsole.Write(table);
    }
}