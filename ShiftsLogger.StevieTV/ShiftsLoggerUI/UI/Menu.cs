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
                UpdateShift();
                break;
            case MainMenuOptions.DeleteShift:
                DeleteShift();
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

       var result = ShiftsService.AddShift(shift);

       if (!result)
       {
           AnsiConsole.Prompt(new ConfirmationPrompt("Unable to record this shift. Press enter to continue"));
       }
       else if (!AnsiConsole.Prompt(new ConfirmationPrompt("Shift Added - Do you wish to do more?")))
       {
           Environment.Exit(0);
       }
    }
 
    private static void DeleteShift()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText("Delete a Shift")
            .Color(Color.Red));

        var selectedShift = SelectShift();

        if (selectedShift.ShiftId == 0) return;
        
        var result = ShiftsService.DeleteShift(selectedShift);
        
        if (!result)
        {
            AnsiConsole.Prompt(new ConfirmationPrompt("Unable to delete this shift. Press enter to continue"));
        }
        else if (!AnsiConsole.Prompt(new ConfirmationPrompt("Shift Deleted - Do you wish to do more?")))
        {
            Environment.Exit(0);
        }
    }

    private static void UpdateShift()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText("Update a Shift")
            .Color(Color.Yellow));

        var selectedShift = SelectShift();

        if (selectedShift.ShiftId == 0) return;
        
        var finishedUpdating = false;

        while (!finishedUpdating)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new FigletText("Updating Shift")
                .Color(Color.Yellow));
                
            var table = new Table();
            table.AddColumns("Date", "Employee ID", "Start", "End", "Duration", "Comment");

            table.AddRow(selectedShift.StartTime.ToShortDateString(), selectedShift.EmployeeId.ToString(), selectedShift.StartTime.ToShortTimeString(), selectedShift.EndTime.ToShortTimeString(),
                $"{selectedShift.Duration.Hours}h {selectedShift.Duration.Minutes:00}m", selectedShift.Comment);
            AnsiConsole.Write(table);

            var fieldMenu = new SelectionPrompt<UpdateMenuOptions>();
            fieldMenu.Title("Please choose an option");
            fieldMenu.AddChoice(UpdateMenuOptions.EmployeeId);
            fieldMenu.AddChoice(UpdateMenuOptions.Date);
            fieldMenu.AddChoice(UpdateMenuOptions.StartTime);
            fieldMenu.AddChoice(UpdateMenuOptions.EndTime);
            fieldMenu.AddChoice(UpdateMenuOptions.Comment);
            fieldMenu.AddChoice(UpdateMenuOptions.Save);

            var fieldOption = AnsiConsole.Prompt(fieldMenu);
            
            switch (fieldOption)
            {
                case UpdateMenuOptions.EmployeeId:
                {
                    selectedShift.EmployeeId = UserInput.GetEmployeeId(selectedShift.EmployeeId);
                    break;
                }
                case UpdateMenuOptions.Date:
                {
                    var date = UserInput.GetDate(selectedShift.StartTime);
                    var startTime = selectedShift.StartTime.ToShortTimeString();
                    var endTime = selectedShift.EndTime.ToShortTimeString();
                    selectedShift.StartTime = DateTime.ParseExact($"{date} {startTime}", "dd/MM/yy HH:mm", CultureInfo.InvariantCulture);
                    selectedShift.EndTime = DateTime.ParseExact($"{date} {endTime}", "dd/MM/yy HH:mm", CultureInfo.InvariantCulture);
                    break;
                }
                case UpdateMenuOptions.StartTime:
                {
                    var date = selectedShift.StartTime.Date;
                    var startTime = UserInput.GetTime(true, selectedShift.StartTime);
                    var newStartTime = date.Add(TimeSpan.ParseExact(startTime, @"h\:mm", CultureInfo.InvariantCulture));
                    selectedShift.StartTime = newStartTime;
                    selectedShift.Duration = selectedShift.EndTime - selectedShift.StartTime;
                    break;
                }
                case UpdateMenuOptions.EndTime:
                {
                    var date = selectedShift.EndTime.Date;
                    var endTime = UserInput.GetTime(false, selectedShift.StartTime);
                    var newEndTime = date.Add(TimeSpan.ParseExact(endTime, @"h\:mm", CultureInfo.InvariantCulture));
                    selectedShift.EndTime = newEndTime;
                    selectedShift.Duration = selectedShift.EndTime - selectedShift.StartTime;
                    break;
                }
                case UpdateMenuOptions.Comment:
                {
                    selectedShift.Comment = UserInput.GetComment(selectedShift.Comment);
                    break;
                } case UpdateMenuOptions.Save:
                    finishedUpdating = true;
                    break;
            }
        }

        var result = ShiftsService.UpdateShift(selectedShift);
        
        if (!result)
        {
            AnsiConsole.Prompt(new ConfirmationPrompt("Unable to update this shift. Press enter to continue"));
        }
        else if (!AnsiConsole.Prompt(new ConfirmationPrompt("Shift updated - Do you wish to do more?")))
        {
            Environment.Exit(0);
        }
    }
    
    private static Shift SelectShift()
    {
        var shifts = ShiftsService.GetShifts();
        var sortedShifts = shifts.OrderBy(x => x.StartTime).ThenBy(x => x.EmployeeId).ToList();
        
        var selectOptions = new SelectionPrompt<Shift>();
        selectOptions.AddChoice(new Shift {ShiftId = 0});
        selectOptions.AddChoices(sortedShifts);
        selectOptions.UseConverter(shift => (shift.ShiftId == 0 ? "CANCEL" : $"{shift.EmployeeId} on {shift.StartTime.ToShortDateString()} @ {shift.StartTime.ToShortTimeString()} - ({shift.Comment})"));

        var selectedShift = AnsiConsole.Prompt(selectOptions);

        return selectedShift;
    }
    
}