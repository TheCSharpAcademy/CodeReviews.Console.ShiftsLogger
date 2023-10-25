using System.Globalization;
using ShiftsLogger.UI.Controllers;
using ShiftsLogger.UI.Exceptions;
using ShiftsLogger.UI.Models.DTOs;
using ShiftsLogger.UI.Views;
using Spectre.Console;

namespace ShiftsLogger.UI.Services;

public static class ShiftService
{
    private static readonly string DateFormat = CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern;
    private static readonly string TimeFormat = CultureInfo.InvariantCulture.DateTimeFormat.ShortTimePattern;

    public static void ShowShifts()
    {
        try
        {
            var shifts = ShiftController.GetShifts().Result;

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
                var shift = ShiftController.GetShiftById((long)shiftId).Result;
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
            AnsiConsole.MarkupLine("[green]Shift has been added.[/]");
        }
        catch (ApiException e)
        {
            var messageParts = e.Message.Split(":");
            AnsiConsole.MarkupLineInterpolated($"[red]{messageParts[0]}[/]{messageParts[1]}");
        }
    }

    public static void DeleteShift()
    {
        try
        {
            var shiftId = GetShiftIdInput();

            if (shiftId != null)
            {
                ShiftController.DeleteShift((long)shiftId);
                AnsiConsole.MarkupLine("[green]Shift has been deleted.[/]");
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

    public static void UpdateShift()
    {
        try
        {
            var shift = GetShiftToUpdateInput();

            if (shift != null)
            {
                ShiftController.UpdateShift(shift.Id, shift);
                AnsiConsole.MarkupLine("[green]Shift has been updated.[/]");
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

    private static ShiftDto GetShiftInput()
    {
        var workerName = AnsiConsole.Ask<string>("Worker:");

        var startDate = AnsiConsole.Ask<DateTime>($"Start date (format: {DateFormat}):");
        var startTime = AnsiConsole.Ask<TimeSpan>($"Start dime (format: {TimeFormat}): ");

        var finishDate = AnsiConsole.Confirm("Use the same date as start date for finish date?")
            ? startDate
            : AnsiConsole.Ask<DateTime>($"Finish date (format: {DateFormat}):");
        var finishTime = AnsiConsole.Ask<TimeSpan>($"Finish time (format: {TimeFormat}): ");

        return new ShiftDto
        {
            WorkerName = workerName,
            StartedAt = startDate.Add(startTime),
            FinishedAt = finishDate.Add(finishTime)
        };
    }

    private static ShiftToUpdateDto? GetShiftToUpdateInput()
    {
        var shiftId = GetShiftIdInput();
        if (shiftId == null) return null;

        var shift = ShiftController.GetShiftById((long)shiftId).Result;

        ShiftView.ShowShiftBeingUpdated(new ShiftDto
        {
            WorkerName = shift.WorkerName,
            StartedAt = shift.StartedAt,
            FinishedAt = shift.FinishedAt
        });

        shift.WorkerName = AnsiConsole.Confirm("Update worker?")
            ? AnsiConsole.Ask<string>("Worker:")
            : shift.WorkerName;

        if (AnsiConsole.Confirm("Update start date?"))
        {
            var startDate = AnsiConsole.Ask<DateTime>($"Start date (format: {DateFormat}):");
            var startTime = AnsiConsole.Ask<TimeSpan>($"Start dime (format: {TimeFormat}): ");

            shift.StartedAt = startDate.Add(startTime);
        }
        else
        {
            shift.StartedAt = shift.StartedAt;
        }

        if (AnsiConsole.Confirm("Update finish date?"))
        {
            var finishDate = AnsiConsole.Ask<DateTime>($"Finish date (format: {DateFormat}):");
            var finishTime = AnsiConsole.Ask<TimeSpan>($"Finish time (format: {TimeFormat}): ");

            shift.FinishedAt = finishDate.Add(finishTime);
        }
        else
        {
            shift.FinishedAt = shift.FinishedAt;
        }

        return new ShiftToUpdateDto
        {
            Id = shift.Id,
            WorkerName = shift.WorkerName,
            StartedAt = shift.StartedAt,
            FinishedAt = shift.FinishedAt
        };
    }

    private static long? GetShiftIdInput()
    {
        var shifts = ShiftController.GetShifts().Result;
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