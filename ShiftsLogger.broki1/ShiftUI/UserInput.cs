using Microsoft.IdentityModel.Tokens;
using ShiftApi.DTOs;
using ShiftApi.Models;
using Spectre.Console;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace ShiftUI;

internal class UserInput
{
    internal static string GetMainMenuChoice()
    {
        return AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("-----MAIN MENU---")
            .AddChoices(
                "Current employee",
                "New employee",
                "Update employee",
                "Delete employee",
                "Quit application"
                )
            );
    }

    internal static int GetEmployeeChoiceById(List<string> employees)
    {
        var employeeString = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("-----EMPLOYEES-----\n\nEmployee ID\t\tFirst name\t\tLast name")
            .AddChoices(employees)
            );

        var employeeId = int.Parse(employeeString.Split(" ")[0]);

        return employeeId;
    }

    internal static string GetEmployeeShiftLoggerMenuChoice(Employee employee)
    {
        var menuChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title($"{employee.FirstName.ToUpper()} {employee.LastName.ToUpper()} - #{employee.EmployeeId.ToString().PadLeft(4, '0')}")
            .AddChoices(
                "Add shift",
                "View shifts",
                "Update shift",
                "Delete shift",
                "Return to main menu"));

        return menuChoice;
    }

    internal static DateTime GetDate(string startOrEnd)
    {
        Console.Clear();
        Console.WriteLine($"Enter the shift {startOrEnd} date (format: MM-dd-yyyy), press Enter to return to main menu:");
        var dateString = Console.ReadLine().Trim();

        if (dateString.IsNullOrEmpty()) return new DateTime();

        while (!ValidationEngine.ValidDate(dateString))
        {
            Console.WriteLine($"\nInvalid input. Enter the shift {startOrEnd} date in MM-dd-yyyy format, press Enter to return to main menu:");
            dateString = Console.ReadLine().Trim();

            if (dateString.IsNullOrEmpty()) return new DateTime();
        }

        var startDate = DateTime.ParseExact(dateString, "MM-dd-yyyy", new CultureInfo("en-US"), DateTimeStyles.None);

        return startDate;
    }

    internal static TimeSpan GetTime(string startOrEnd)
    {
        Console.Clear();
        Console.WriteLine($"Enter the shift {startOrEnd} time (format: HH:mm), press Enter to return to main menu:");
        var timeString = Console.ReadLine().Trim();

        if (timeString.IsNullOrEmpty()) return new TimeSpan();

        while (!ValidationEngine.ValidTime(timeString))
        {
            Console.WriteLine($"\nInvalid input. Enter the shift {startOrEnd} time in HH:mm format:");
            timeString = Console.ReadLine();

            if (timeString.IsNullOrEmpty()) return new TimeSpan();
        }

        var startTime = TimeSpan.ParseExact(timeString, "h\\:mm", new CultureInfo("en-US"), TimeSpanStyles.None);

        return startTime;
    }

    internal static int GetShiftChoice(List<string> shifts)
    {
        var shiftChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .AddChoices(shifts)
            );

        var id = int.Parse(shiftChoice.Split(".")[0]);

        return id;
    }

    internal static ShiftDTO GetUpdatedShift(ShiftDTO shiftToUpdate)
    {
        Console.Clear();
        Console.WriteLine(shiftToUpdate.ShiftStartTime.Date.ToShortDateString() + "\n");
        var shiftStartDate = AnsiConsole.Confirm("Update date of shift?") ? UserInput.GetDate("start") : shiftToUpdate.ShiftStartTime.Date;

        Console.Clear();
        Console.WriteLine(shiftToUpdate.ShiftStartTime.TimeOfDay.ToString() + "\n");
        var shiftStartTime = AnsiConsole.Confirm("Update start time of shift?") ? UserInput.GetTime("start") : TimeSpan.Parse(shiftToUpdate.ShiftStartTime.TimeOfDay.ToString());

        Console.Clear();
        Console.WriteLine(shiftToUpdate.ShiftEndTime.TimeOfDay.ToString() + "\n");
        var shiftEndTime = AnsiConsole.Confirm("Update end time of shift?") ? UserInput.GetTime("end") : TimeSpan.Parse(shiftToUpdate.ShiftEndTime.TimeOfDay.ToString());

        while (!ValidationEngine.ValidShiftEndTime(shiftStartTime, shiftEndTime))
        {
            Console.Clear();
            Console.WriteLine("\nInvalid shift. Shifts cannot be longer than 16 hours.\nPress any key to continue.");
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine(shiftToUpdate.ShiftStartTime.Date.ToShortDateString() + "\n");
            shiftStartDate = AnsiConsole.Confirm("Update date of shift?") ? UserInput.GetDate("start") : shiftToUpdate.ShiftStartTime.Date;

            Console.Clear();
            Console.WriteLine(shiftToUpdate.ShiftStartTime.TimeOfDay.ToString() + "\n");
            shiftStartTime = AnsiConsole.Confirm("Update start time of shift?") ? UserInput.GetTime("start") : TimeSpan.Parse(shiftToUpdate.ShiftStartTime.TimeOfDay.ToString());

            Console.Clear();
            Console.WriteLine(shiftToUpdate.ShiftEndTime.TimeOfDay.ToString() + "\n");
            shiftEndTime = AnsiConsole.Confirm("Update end time of shift?") ? UserInput.GetTime("end") : TimeSpan.Parse(shiftToUpdate.ShiftEndTime.TimeOfDay.ToString());
        }

        var shiftStartDateAndTime = new DateTime(shiftStartDate.Year, shiftStartDate.Month, shiftStartDate.Day,
        shiftStartTime.Hours, shiftStartTime.Minutes, 0);

        var timeDifference = Helper.GetTimeDifference(shiftStartTime, shiftEndTime);

        var shiftEndDateAndTime = shiftStartDateAndTime.AddMinutes(timeDifference.TotalMinutes);

        var updatedShift = new ShiftDTO
        {
            ShiftId = shiftToUpdate.ShiftId,
            ShiftStartTime = shiftStartDateAndTime,
            ShiftEndTime = shiftEndDateAndTime
        };

        return updatedShift;
    }

    internal static string GetName(string firstOrLast)
    {
        Console.Clear();

        var name = AnsiConsole.Ask<string>($"Enter {firstOrLast} name (press 0 to return to main menu):");

        Console.Clear();
        return name;
    }
}
