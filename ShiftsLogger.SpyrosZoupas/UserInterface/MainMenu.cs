using ShiftsLogger.SpyrosZoupas.DAL.Model;
using Spectre.Console;
using UserInterface.SpyrosZoupas.Util;

namespace UserInterface.SpyrosZoupas;

public class MainMenu
{
    private readonly ShiftService _shiftService;

    public MainMenu(ShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    public void ShiftsMenu()
    {
        var isContactMenuRunning = true;
        while (isContactMenuRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
            new SelectionPrompt<ShiftMenuOptions>()
            .Title("Products Menu")
            .AddChoices(
                ShiftMenuOptions.AddShift,
                ShiftMenuOptions.DeleteShift,
                ShiftMenuOptions.UpdateShift,
                ShiftMenuOptions.ViewAllShifts,
                ShiftMenuOptions.ViewShift,
                ShiftMenuOptions.Quit));

            switch (option)
            {
                case ShiftMenuOptions.AddShift:
                    _shiftService.InsertShift();
                    break;
                case ShiftMenuOptions.DeleteShift:
                    _shiftService.DeleteShift();
                    break;
                case ShiftMenuOptions.UpdateShift:
                    _shiftService.UpdateShift();
                    break;
                case ShiftMenuOptions.ViewShift:
                    ShowShift(_shiftService.GetShift().Result);
                    break;
                case ShiftMenuOptions.ViewAllShifts:
                    ShowShiftTable(_shiftService.GetAllShifts().Result);
                    break;
                case ShiftMenuOptions.Quit:
                    isContactMenuRunning = false;
                    break;
            }
        }
    }

    private void ShowShiftTable(List<Shift> shifts)
    {
        if (shifts.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No data to display.[/]");
        }
        else
        {
            var table = new Table();
            table.AddColumn("Id")
                .AddColumn("Start")
                .AddColumn("End")
                .AddColumn("Total hours");

            foreach (Shift shift in shifts)
            {
                table.AddRow(
                    shift.ShiftId.ToString(),
                    shift.StartDateTime.ToString(),
                    shift.EndDateTime.ToString(),
                    (shift.DurationSeconds / 60 / 60).ToString());
            }

            AnsiConsole.Write(table);
        }

        Console.WriteLine("Enter any key to go back to Main Menu");
        Console.ReadLine();
        Console.Clear();
    }

    private void ShowShift(Shift shift)
    {
        if (shift == null)
        {
            AnsiConsole.MarkupLine("[red]No data to display.[/]");
        }
        else
        {
            var panel = new Panel($@"Id: {shift.ShiftId}
Start: {shift.StartDateTime}
End: {shift.EndDateTime}
Total hours: {shift.DurationSeconds / 60 / 60}");
            panel.Header = new PanelHeader("Shift Info");
            panel.Padding = new Padding(2, 2, 2, 2);

            AnsiConsole.Write(panel);
        }

        Console.WriteLine("Enter any key to go back to Main Menu");
        Console.ReadLine();
        Console.Clear();
    }
}
