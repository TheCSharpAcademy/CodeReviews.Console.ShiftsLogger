using Shiftlogger.UI.DTOs;
using Spectre.Console;
using Shiftlogger.UI.Validators;
using Shiftlogger.UI.Constants;
using System.Globalization;

namespace Shiftlogger.UI;

public class UserInput
{
    internal static WorkerNewDto? GetNewWorker()
    {
        AnsiConsole.Clear();
        WorkerNewDto worker = new WorkerNewDto();
        worker.name = GetStringInput(Messages.NameMessage);
        if (string.IsNullOrEmpty(worker.name)) return null;

        worker.email = GetEmailInput(Messages.EmailMessage);
        if (string.IsNullOrEmpty(worker.email)) return null;

        worker.phoneNumber = GetPhoneInput(Messages.PhoneNumberMessage);
        if (string.IsNullOrEmpty(worker.phoneNumber)) return null;

        return worker;
    }

    internal static bool UpdateWorker(WorkerRequestDto worker)
    {
        worker.name = AnsiConsole.Confirm($"Do you want to Update Name([maroon]{worker.name}[/])") ? GetStringInput(Messages.NameMessage) : worker.name;
        if (string.IsNullOrEmpty(worker.name)) return false;

        worker.email = AnsiConsole.Confirm($"Do you want to Update Email([maroon]{worker.email}[/])") ? GetEmailInput(Messages.EmailMessage) : worker.email;
        if (string.IsNullOrEmpty(worker.email)) return false;

        worker.phoneNumber = AnsiConsole.Confirm($"Do you want to Update Phone Number([maroon]{worker.phoneNumber}[/])") ? GetPhoneInput(Messages.PhoneNumberMessage) : worker.phoneNumber;
        if (string.IsNullOrEmpty(worker.phoneNumber)) return false;

        return true;
    }


    internal static ShiftNewDto? GetNewShift()
    {
        ShiftNewDto shift = new ShiftNewDto();
        while (!shift.startDateTime.HasValue || !shift.endDateTime.HasValue || !(DateTime.Compare(shift.endDateTime.Value, shift.startDateTime.Value) > 0))
        {
            Console.WriteLine("End date and time must be after the start date and time.");

            shift.startDateTime = GetDateInput("Enter Start date and time in [bold green](yyyy-MM-dd HH:mm:ss)[/] format.", "yyyy-MM-dd HH:mm:ss");
            if (shift.startDateTime == null) return null;
            shift.endDateTime = GetDateInput("Enter End date and time in [bold green](yyyy-MM-dd HH:mm:ss)[/] format.", "yyyy-MM-dd HH:mm:ss");
            if (shift.endDateTime == null) return null;
        }
        return shift;

    }

    internal static bool UpdateShift(ShiftDto shift, List<WorkerRequestDto> workers)
    {
        if (AnsiConsole.Confirm($"Do you want to Update Shift Worker?)"))
        {
            VisualizationEngine.DisplayWorkers(workers, "All Workers");
            var workerId = GetIntInput();
            if (workerId == 0) return false;
            shift.workerId = workerId;
        }
        while (true)
        {
            Console.WriteLine("End date and time must be after the start date and time.\n");
            shift.startDateTime = AnsiConsole.Confirm($"Do you want to Update Start DateTime([maroon]{shift.startDateTime}[/])") ? GetDateInput("Enter Start date and time in [bold green](yyyy-MM-dd HH:mm:ss)[/] format.", "yyyy-MM-dd HH:mm:ss") : shift.startDateTime;
            if (shift.startDateTime == null) return false;

            shift.endDateTime = AnsiConsole.Confirm($"Do you want to Update End DateTime([maroon]{shift.endDateTime}[/])") ? GetDateInput("Enter Start date and time in [bold green](yyyy-MM-dd HH:mm:ss)[/] format.", "yyyy-MM-dd HH:mm:ss") : shift.endDateTime;
            if (shift.endDateTime == null) return false;

            if (DateTime.Compare(shift.endDateTime.Value, shift.startDateTime.Value) > 0) return true;
        }
    }

    public static string GetStringInput(string message)
    {
        string input = AnsiConsole.Ask<string>(message).Trim();
        if (input == "0") return "";
        while (!ValidatorHelper.IsValidName(input))
        {
            input = AnsiConsole.Ask<string>($"Invalid name [maroon]{input}[/] entered. {message}").Trim();
            if (input == "0") return "";
        }
        return input;
    }

    internal static string GetEmailInput(string message)
    {
        string input = AnsiConsole.Ask<string>(message).Trim();
        if (input == "0") return "";
        while (!ValidatorHelper.IsValidEmail(input))
        {
            input = AnsiConsole.Ask<string>($"Invalid email [maroon]{input}[/] entered. {message}").Trim();
            if (input == "0") return "";
        }
        return input;
    }

    internal static DateTime? GetDateInput(string message, string format)
    {
        string? userInput = AnsiConsole.Ask<string?>($"{message} Or Enter 0 to cancel:");
        while (!ValidatorHelper.IsValidDateInput(userInput, format))
        {
            userInput = AnsiConsole.Ask<string?>($"[bold]Invalid input [red]({userInput})[/][/].\n{message} Or Enter 0 to cancel:");
        }
        return userInput == "0" ? null : DateTime.ParseExact(userInput, format, new CultureInfo("en-US"));
    }

    internal static string GetPhoneInput(string message)
    {
        string input = AnsiConsole.Ask<string>(message).Trim();
        if (input == "0") return "";
        while (!ValidatorHelper.IsValidPhoneNumber(input))
        {
            input = AnsiConsole.Ask<string>($"Invalid phone number [maroon]{input}[/] entered. {message}").Trim();
            if (input == "0") return "";
        }
        return input;
    }

    internal static int GetIntInput()
    {
        int id = AnsiConsole.Ask<int>("Enter an Id from the table Or Enter 0 to Cancel: ");
        return id;
    }

}