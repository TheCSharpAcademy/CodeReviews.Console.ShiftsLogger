using ShiftsLogger.Dejmenek.UI.Enums;
using ShiftsLogger.Dejmenek.UI.Models;
using ShiftsLogger.Dejmenek.UI.Services.Interfaces;
using Spectre.Console;

namespace ShiftsLogger.Dejmenek.UI.Services;
public class UserInteractionService : IUserInteractionService
{
    public Employee GetEmployee(List<Employee> employees)
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<Employee>()
                .Title("Select employee")
                .UseConverter(e => $"{e.FirstName} {e.LastName}")
                .AddChoices(employees)
            );
    }

    public string GetFirstName()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("Enter employee first name: ")
                .ValidationErrorMessage("Your input must not be empty")
                .Validate(Validation.IsValidString)
            );
    }

    public string GetLastName()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("Enter employee last name: ")
                .ValidationErrorMessage("Your input must not be empty")
                .Validate(Validation.IsValidString)
            );
    }

    public bool GetConfirmation(string title)
    {
        return AnsiConsole.Confirm(title);
    }

    public void GetUserInputToContinue()
    {
        AnsiConsole.MarkupLine("Press Enter to continue...");
        Console.ReadLine();
    }

    public string GetDateTime()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("Enter date time in 'yyyy-MM-dd HH:mm' format: ")
                .ValidationErrorMessage("This is not a valid date format")
                .Validate(Validation.IsValidDateTimeFormat)
            );
    }

    public Shift GetShift(List<Shift> shifts)
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<Shift>()
                .Title("Select shift")
                .UseConverter(s => $"{s.EmployeeFirstName} {s.EmployeeLastName} {s.StartTime:yyyy-MM-dd HH:mm} {s.EndTime:yyyy-MM-dd HH:mm}")
                .AddChoices(shifts)
            );
    }

    public MenuOptions GetMenuOption()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<MenuOptions>()
                .Title("What would you like to do?")
                .AddChoices(Enum.GetValues<MenuOptions>())
            );
    }

    public ManageShiftsOptions GetManageShiftsOptions()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<ManageShiftsOptions>()
                .Title("What would you like to do with shifts?")
                .AddChoices(Enum.GetValues<ManageShiftsOptions>())
            );
    }

    public ManageEmployeesOptions GetManageEmployeesOptions()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<ManageEmployeesOptions>()
                .Title("What would you like to do with employees?")
                .AddChoices(Enum.GetValues<ManageEmployeesOptions>())
            );
    }
}
