using System.Globalization;
using ShiftsLoggerUI.Data;
using ShiftsLoggerUI.Helpers;
using ShiftsLoggerUI.Models;
using Spectre.Console;

namespace ShiftsLoggerUI.UI;

internal class Menu
{
    internal void MainMenu()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText("Shifts Logger"));
        
        var menuSelection = new SelectionPrompt<MainMenuOptions>();
        menuSelection.Title("Please choose an option");
        menuSelection.AddChoice(MainMenuOptions.ViewShifts);
        menuSelection.AddChoice(MainMenuOptions.AddShift);
        menuSelection.AddChoice(MainMenuOptions.UpdateShift);
        menuSelection.AddChoice(MainMenuOptions.DeleteShift);
        menuSelection.AddChoice(MainMenuOptions.Exit);
        menuSelection.UseConverter(option => option.GetEnumDescription());

        var selectedOption = AnsiConsole.Prompt(menuSelection);
        
        switch (selectedOption)
        {
            case MainMenuOptions.ViewShifts:
                ViewShifts();
                break;
            case MainMenuOptions.AddShift:
                AddShift();
                break;
            case MainMenuOptions.UpdateShift:
                // DeleteShift();
                break;
            case MainMenuOptions.DeleteShift:
                // UpdateShift();
                break;
            case MainMenuOptions.Exit:
                Environment.Exit(0);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static void ViewShifts()
    {
        var shifts = ShiftsService.GetShifts();
        var sortedShifts = shifts.OrderBy(x => x.StartTime).ThenBy(x => x.EmployeeId).ToList();
        var table = new Table();
        table.AddColumns("Date", "Employee ID", "Start", "End", "Duration", "Comment");

        foreach (var shift in sortedShifts)
        {
            table.AddRow(shift.StartTime.ToShortDateString(), shift.EmployeeId.ToString(), shift.StartTime.ToShortTimeString(), shift.EndTime.ToShortTimeString(),
                $"{shift.Duration.Hours}h {shift.Duration.Minutes:00}m", shift.Comment);
        }
        
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText("Shifts"));
        AnsiConsole.Write(table);
        if (!AnsiConsole.Prompt(new ConfirmationPrompt("Press enter to continue")))
            Environment.Exit(0);
    }

    private static void AddShift()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText("Add a Shift")
            .Color(Color.Green));

        var employeeId = UserInput.GetEmployeeId();
        var date = UserInput.GetDate();
        var startTime = UserInput.GetTime(true);
        var endTime = UserInput.GetTime(false, DateTime.ParseExact(startTime, @"H\:m", CultureInfo.InvariantCulture));
        var comment = UserInput.GetComment();

        var shift = new Shift
        {
            EmployeeId = employeeId,
            StartTime = DateTime.ParseExact($"{date} {startTime}", "dd/MM/yy HH:mm", CultureInfo.InvariantCulture),
            EndTime = DateTime.ParseExact($"{date} {endTime}", "dd/MM/yy HH:mm", CultureInfo.InvariantCulture),
            Comment = comment
        };

        ShiftsService.AddShift(shift);
    }
}