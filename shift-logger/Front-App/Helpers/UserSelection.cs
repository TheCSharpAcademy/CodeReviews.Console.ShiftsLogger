using API.Migrations;
using API.Models;
using Microsoft.Identity.Client;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front_App.Helpers;

public static class UserSelection
{
    public static Shift CreateShift()
    {
        string name = AnsiConsole.Prompt(new TextPrompt<string>("[Blue]Insert employee name[/]")
            );
        string start = AnsiConsole.Prompt(new TextPrompt<string>("[Blue]Insert shift's start date[/]")
            .Validate(s =>
            {
                return ValidateStartDate(s);
            }));
        DateTime startDate = DateTime.ParseExact(start, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
        string end = AnsiConsole.Prompt(new TextPrompt<string>("[Blue]Insert shift's end date[/]")
            .Validate(s =>
            {
                return ValidateEndDate(startDate, s);
            }));
        DateTime endDate = DateTime.ParseExact(end, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);

        return new Shift()
        {
            EmployeeName = name,
            Start = startDate,
            End = endDate,
            Duration = endDate - startDate
        };
    }

    private static ValidationResult ValidateStartDate
        (string s)
    {
        if (!DateTime.TryParseExact(s, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture,
                  DateTimeStyles.None, out var parsed))
        {
            return ValidationResult.Error("Use dd.mm.yyyy HH:mm format");
        }
        if (parsed >= DateTime.Now)
        {
            return ValidationResult.Error("Can't Use a date from the future as a starting date");
        }
        return ValidationResult.Success();
    }

    private static ValidationResult ValidateEndDate(DateTime startDate, string end)
    {
        var initialValidation = ValidateStartDate(end);
        if (!initialValidation.Successful)
        {
            return initialValidation;
        }
        DateTime endDate = DateTime.ParseExact(end, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
        if (startDate > endDate)
        {
            return ValidationResult.Error("Invalid time interval");
        }
        if (endDate - startDate > TimeSpan.FromHours(24))
        {
            return ValidationResult.Error("Shifts can't be longer than 24h!");
        }
        return ValidationResult.Success();
    }

    public static int GetId(IEnumerable<Shift> shifts)
    {
        int id = AnsiConsole.Prompt(new TextPrompt<int>("Select User ID")
       .Validate(id =>
       {
           var selected = shifts.FirstOrDefault(shift => shift.Id == id);
           if (selected == null)
           {
               return ValidationResult.Error("[red]No shift with that ID[/]");
           }
           return ValidationResult.Success();
       }));
        return id;
    }
}