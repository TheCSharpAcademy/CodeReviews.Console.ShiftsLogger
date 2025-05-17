using ShiftUI.UIControllers;
using ShiftUI.UIModels;
using Spectre.Console;
using System.Globalization;

namespace ShiftUI;

class UserInputs
{
    internal static DateTime GetTime()
    {
        bool valid = false;
        DateTime returnedTime = new();
        DateTime result;
        string day;
        string time;
        while (!valid)
        {
            day = AnsiConsole.Prompt(new TextPrompt<string>("Enter a date(Format = yyyy.MM.dd): ").Validate(x => DateTime.TryParseExact(x, "yyyy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result)));
            time = AnsiConsole.Prompt(new TextPrompt<string>("Enter a time(Fromat = HH:mm): ").Validate(x => DateTime.TryParseExact(x, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out result)));
            valid = DateTime.TryParse($"{day} {time}", out returnedTime);
            valid = returnedTime < DateTime.Now ? true : false;
            if (!valid)
            {
                AnsiConsole.WriteLine($"Date and Time must be before actual time {DateTime.Now.ToString("yyyy.MM.dd hh:mm")}");
            }
        }
        return returnedTime;
    }

    internal static UIUser? GetUser()
    {
        List<UIUser>? users = UIUserController.GetAllUsers();
        SelectionPrompt<UIUser> prompt = new();
        prompt.Title("Choose an user below:");
        prompt.AddChoices(users);
        prompt.AddChoice(new UIUser { userId = 0, firstName = "CANCEL" });
        prompt.UseConverter(x => $"First Name: {x.firstName} - Last Name: {x.lastName}");
        UIUser user = AnsiConsole.Prompt(prompt);
        return user.userId == 0 ? null : user;
    }

    internal static string GetName(string message)
    {
        string name = AnsiConsole.Prompt(new TextPrompt<string>(message).Validate(x => x.Any(y => char.IsAsciiLetter(y))));
        return name;
    }

    internal static UIShift GetShift()
    {
        List<UIShift>? shifts = UIShiftController.GetAllShifts();
        List<UIUser>? users = UIUserController.GetAllUsers();
        foreach (UIShift shift in shifts)
        {
            shift.user = users.Single(x => x.userId == shift.userId);
        }
        SelectionPrompt<UIShift> prompt = new();
        prompt.Title("Select a shift below:");
        prompt.AddChoices(shifts);
        prompt.AddChoice(new UIShift { id = 0, user = new UIUser { firstName = "CANCEL" } });
        prompt.UseConverter(x => $"{x.id} - Start Time: {x.startTime} - End Time: {x.endTime} - User: {x.user.firstName} {x.user.lastName}");
        UIShift returnedShift = AnsiConsole.Prompt(prompt);
        return returnedShift;
    }

    internal static bool Validation(string message)
    {
        bool validation = AnsiConsole.Confirm(message, false);
        return validation;
    }
}