using System.Text.RegularExpressions;
using Models;
using Models.Stringer;
using Spectre.Console;

namespace ShiftsLogger;

public static class UI
{
    public static void Clear() => AnsiConsole.Clear();

    public static string MenuSelection(string title, string[] options)
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(title)
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                .AddChoices(options)
        );
    }

    public static string StringResponse(string question) => AnsiConsole.Ask<string>(question + ":");

    public static string StringResponseWithFormat(string prompt, string format)
    {
        while (true)
        {
            string response = AnsiConsole.Ask<string>(prompt + $"[grey] formatted as '{format}[/]':");

            if (Regex.IsMatch(response, format))
            {
                return response;
            }

            AnsiConsole.MarkupLine($"Requires format '[yellow]{format}[/]'");
        }
    }

    public static TimeOnly TimeOnlyResponse(string prompt)
    {
        while (true)
        {
            var response = AnsiConsole.Ask<string>(prompt + ":");

            if (TimeOnly.TryParseExact(response, "HH:mm", out TimeOnly time))
            {
                return time;
            }

            AnsiConsole.MarkupLine("[red]Invalid time.[/] [grey]Format as 'HH:mm'[/]");
        }
    }

    public static TimeOnly TimeOnlyResponseWithDefault(string prompt, TimeOnly defaultTime)
    {
        while (true)
        {
            AnsiConsole.Markup(prompt + $". [grey]Format as 'HH:mm'[/]. Press 'enter' to leave as [grey]{defaultTime.ToHourMinutes()}[/]: ");
            var response = Console.ReadLine();

            if (response == "")
            {
                return defaultTime;
            }
            else if (TimeOnly.TryParseExact(response, "HH:mm", out TimeOnly time))
            {
                return time;
            }

            AnsiConsole.MarkupLine("[red]Invalid time.[/] [grey]Format as 'HH:mm'[/]");
        }
    }

    public static string StringResponseWithDefault(string question, string defaultResponse)
    {
        var response = AnsiConsole.Prompt(
            new TextPrompt<string>(question + " [grey](Press 'enter' to leave as '" + defaultResponse + "'[/]):")
                .AllowEmpty()
        );

        if (response == null || response == "")
        {
            return defaultResponse;
        }
        else
        {
            return response;
        }
    }

    public static int IntResponse(string question)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<int>(question + ":")
                .ValidationErrorMessage("[red]That's not a valid number[/]")
                .Validate(id =>
                {
                    return id switch
                    {
                        < 0 => ValidationResult.Error("[red]Id's must be a number greater than or equal to 0[/]"),
                        _ => ValidationResult.Success(),
                    };
                })
        );
    }

    public static string TimeResponse(string question)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>(question + " formatted like [yellow]23:59 dd-MM-yy[/]:")
                .PromptStyle("green")
                .ValidationErrorMessage("format times like [red]23:59 dd-MM-yy[/]")
                .Validate(time =>
                {
                    return DateTime.TryParseExact(time, "HH:mm dd-MM-yy", null, System.Globalization.DateTimeStyles.None, out DateTime dateTime);
                })
        );
    }

    public static DateOnly DateOnlyResponseWithDefault(string prompt, DateOnly defaultDate)
    {
        while (true)
        {
            AnsiConsole.Markup(prompt + $". [grey]Format as 'dd-MM-yyyy'[/]. Press 'enter' to leave as [grey]{defaultDate.ToDayMonthYear()}[/]: ");
            var response = Console.ReadLine();

            if (response == "")
            {
                return defaultDate;
            }
            else if (DateOnly.TryParseExact(response, "dd-MM-yyyy", out DateOnly date))
            {
                return date;
            }

            AnsiConsole.MarkupLine("[red]Invalid date.[/] [grey]Format as 'dd-MM-yyyy'[/]");
        }
    }

    public static void ConfirmationMessage(string message)
    {
        if (message != "")
        {
            AnsiConsole.Write(". ");
        }
        AnsiConsole.Console.MarkupLine(message + "Press 'enter' to continue");
        Console.ReadLine();
        AnsiConsole.Clear();
    }

    public static void InvalidationMessage(string message) => AnsiConsole.MarkupLine("[red]" + message + "[/]");

    public static void DisplayWorkers(IEnumerable<GetWorkerDto> workers)
    {
        var table = new Table();

        string[] columns = ["ID", "First Name", "Last Name", "Position"];
        table.AddColumns(columns);

        foreach (var worker in workers)
        {
            table.AddRow(
                worker.Id.ToString(),
                worker.FirstName,
                worker.LastName,
                worker.Position
            );
        }
        AnsiConsole.Write(table);
    }

    public static void DisplayShifts(IEnumerable<GetShiftDto> shifts)
    {
        var table = new Table();

        string[] columns = ["ID", "Name", "Start Time", "End Time"];
        table.AddColumns(columns);

        foreach (var shift in shifts)
        {
            table.AddRow(
                shift.Id.ToString(),
                shift.Name,
                shift.StartTime.ToShortTimeString(),
                shift.EndTime.ToShortTimeString()
            );
        }
        AnsiConsole.Write(table);
    }

    public static void DisplayWorkerShifts(IEnumerable<GetWorkerShiftDto> workerShifts)
    {
        var table = new Table();

        string[] columns = ["ID", "Worker ID", "Shift ID", "Shift Date", "Shift Name", "Worker Name"];
        table.AddColumns(columns);

        foreach (var workerShift in workerShifts)
        {
            table.AddRow(
                workerShift.Id.ToString(),
                workerShift.WorkerId.ToString(),
                workerShift.ShiftId.ToString(),
                workerShift.ShiftDate.ToDayMonthYear(),
                workerShift.Shift.Name,
                workerShift.Worker.FirstName + " " + workerShift.Worker.LastName
            );
        }
        AnsiConsole.Write(table);
    }
}
