using System.Globalization;
using ShiftsLogger.UI.Controllers;
using ShiftsLogger.UI.Exceptions;
using ShiftsLogger.UI.Models.DTOs;
using ShiftsLogger.UI.Views;
using Spectre.Console;

namespace ShiftsLogger.UI.Services;

public static class ShiftService
{
    public static void ShowShifts()
    {
        try
        {
            var shifts = ShiftController.GetShifts();

            if (shifts.Any())
            {
                var shiftsForView = shifts.Select(shift =>
                    new ShiftViewDto
                    {
                        WorkerName = shift.WorkerName,
                        StartedAt = shift.StartedAt,
                        FinishedAt = shift.FinishedAt
                    }).ToList();

                ShiftView.ShowShiftsTable(shiftsForView);
            }
            else
            {
                Console.WriteLine("No logged shifts.");
            }
        }
        catch (ApiException e)
        {
            var messageParts = e.Message.Split(":");
            AnsiConsole.MarkupLineInterpolated($"[red]{messageParts[0]}[/]{messageParts[1]}");
        }
    }

    public static void ShowShiftDetails()
    {
        try
        {
            var shiftId = GetShiftIdInput();

            if (shiftId != null)
            {
                var shift = ShiftController.GetShiftById((long)shiftId);
                var shiftForView = new ShiftViewDetailsDto
                {
                    WorkerName = shift.WorkerName,
                    StartedAt = shift.StartedAt,
                    FinishedAt = shift.FinishedAt,
                    Duration = shift.Duration
                };

                ShiftView.ShowShiftDetails(shiftForView);
            }
            else
            {
                Console.WriteLine("No logged shifts.");
            }
        }
        catch (ApiException e)
        {
            var messageParts = e.Message.Split(":");
            AnsiConsole.MarkupLineInterpolated($"[red]{messageParts[0]}[/]{messageParts[1]}");
        }
    }

    public static void AddShift()
    {
        try
        {
            var shift = GetShiftInput();

            ShiftController.AddShift(shift);
            AnsiConsole.MarkupLine("[green]Shift was added.[/]");
        }
        catch (ApiException e)
        {
            var messageParts = e.Message.Split(":");
            AnsiConsole.MarkupLineInterpolated($"[red]{messageParts[0]}[/]{messageParts[1]}");
        }
    }

    private static ShiftDto GetShiftInput()
    {
        var workerName = AnsiConsole.Ask<string>("Worker:");

        var dateFormat = CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern;
        var timeFormat = CultureInfo.InvariantCulture.DateTimeFormat.ShortTimePattern;

        var startDate = AnsiConsole.Ask<DateTime>($"Start Date (format: {dateFormat}):");
        var startTime = AnsiConsole.Ask<TimeSpan>($"Start Time (format: {timeFormat}): ");

        var finishDate = AnsiConsole.Confirm("Use the same date as start date for finish date?")
            ? startDate
            : AnsiConsole.Ask<DateTime>($"Finish Date (format: {dateFormat}):");
        var finishTime = AnsiConsole.Ask<TimeSpan>($"Finish Time (format: {timeFormat}): ");

        return new ShiftDto
        {
            WorkerName = workerName,
            StartedAt = startDate.Add(startTime),
            FinishedAt = finishDate.Add(finishTime)
        };
    }

    private static long? GetShiftIdInput()
    {
        var shifts = ShiftController.GetShifts();
        if (!shifts.Any()) return null;

        var shiftsOptions = shifts.Select(shift =>
            new ShiftViewDetailsDto
            {
                WorkerName = shift.WorkerName,
                StartedAt = shift.StartedAt,
                FinishedAt = shift.FinishedAt,
                Duration = shift.Duration
            }).ToList();

        var selected = AnsiConsole.Prompt(new SelectionPrompt<ShiftViewDetailsDto>()
            .Title("Choose shift")
            .AddChoices(shiftsOptions));

        var id = shifts.Find(shift =>
            shift.WorkerName == selected.WorkerName &&
            shift.StartedAt == selected.StartedAt &&
            shift.FinishedAt == selected.FinishedAt)!.Id;

        return id;
    }
}