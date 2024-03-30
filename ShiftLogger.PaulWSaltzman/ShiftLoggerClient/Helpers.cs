using ShiftLoggerClient.ClientControllers;
using Spectre.Console;
using System.Reflection.Metadata.Ecma335;

namespace ShiftLoggerClient;

internal class Helpers
{
    internal static DateTime GetDateTime()
    {
        var punchDateTime = new DateTime();
        int year = AnsiConsole.Prompt(
        new TextPrompt<int>("Year YYYY:")
        .ValidationErrorMessage("[red] That is not a valid year[/]")
        .Validate(year =>
        {
            if (year > DateTime.Now.Year)
            {
                return ValidationResult.Error("[red]You may not enter a future date[/]");
            }
            else if (year <= 2000)
            {
                return ValidationResult.Error("[red]You may not enter a date older than they year 2000.[/]");
            }
            else
            {
                return ValidationResult.Success();
            }
        }));

        int month = AnsiConsole.Prompt(
         new TextPrompt<int>("Month MM:")
         .ValidationErrorMessage("[red] That is not a valid month[/]")
         .Validate(month =>
         {

             if (month < 1 || month > 12)
             {
                 return ValidationResult.Error("[red]Valid months are 1-12 [/]");
             }
             else
             {

                 var punchDateTime = new DateTime(year, month,1, 0, 0, 0);
                 if (punchDateTime > DateTime.Now)
                 {
                     return ValidationResult.Error("[red]You may not enter a future date[/]");
                 }
                 else
                 {
                     return ValidationResult.Success();
                 }
             }
         }));

        int day = AnsiConsole.Prompt(
        new TextPrompt<int>("Day DD:")
        .ValidationErrorMessage("[red] That is not a valid day[/]")
        .Validate(day =>
        {
            if (day < 1 || day > 31)
            {
                return ValidationResult.Error("[red]Valid days are 1-31 [/]");
            }
            else if ((punchDateTime = new DateTime(year, month, day, 0, 0, 0)) > DateTime.Now)
            {
                return ValidationResult.Error("[red]You may not enter a future date[/]");
            }
            else
            {
                int daysInMonth = DateTime.DaysInMonth(year, month);

                if (day > daysInMonth)
                {
                    return ValidationResult.Error("[red]There are not that many days in the month.[/]");
                }
                else
                {
                    return ValidationResult.Success();
                }
            }
        }));

        int hour = AnsiConsole.Prompt(
         new TextPrompt<int>("24 Hour hh:")
         .ValidationErrorMessage("[red] That is not a valid hour[/]")
         .Validate(hour =>
         {
             if (0 > hour || hour > 23)
             {
                 return ValidationResult.Error("[red]Valid days are 0 - 23[/]");
             }
             else
             {
                 return ValidationResult.Success();
             }
         }));

        int min = AnsiConsole.Prompt(
         new TextPrompt<int>("Min mm:")
         .ValidationErrorMessage("[red] That is not a valid minute[/]")
         .Validate(min =>
         {
             if (0 > min || min > 59)
             {
                 return ValidationResult.Error("[red]Valid minutes are 0 - 59[/]");
             }
             else
             {
                 return ValidationResult.Success();
             }
         }));

        punchDateTime = new DateTime(year, month, day, hour, min, 0);
        return punchDateTime;
    }

    internal static bool ValidateEmployeeID(int employeeId)
    {
        var employee = EmployeeClientController.GetEmployeeDTO(employeeId);
        Console.Clear();
        if (employee.Id == 0)
        {
            Console.WriteLine($"An Employee with the ID: {employeeId} Does not exist.");
            Console.ReadKey();
            Console.WriteLine("Press any key to continue.");
            return false;

        }
        else
        {
            return true;
        }
    }

}
