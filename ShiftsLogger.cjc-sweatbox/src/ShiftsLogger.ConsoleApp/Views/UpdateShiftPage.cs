using ShiftsLogger.ConsoleApp.Constants;
using ShiftsLogger.ConsoleApp.Engines;
using ShiftsLogger.ConsoleApp.Models;
using ShiftsLogger.ConsoleApp.Services;
using Spectre.Console;

namespace ShiftsLogger.ConsoleApp.Views;

/// <summary>
/// Page which allows users to update a shift entry.
/// </summary>
internal class UpdateShiftPage : BasePage
{
    #region Constants

    private const string PageTitle = "Update Shift";

    #endregion
    #region Methods

    internal static UpdateShiftRequest? Show(ShiftDto shift)
    {
        WriteHeader(PageTitle);

        // Show user the what is being updated.
        var table = TableEngine.GetShiftTable(shift);
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();

        string dateTimeFormat = StringFormat.DateTime;

        var startTime = UserInputService.GetShiftStartDateTime(
            $"Enter the start date and time, format [blue]{dateTimeFormat}[/], or [blue]0[/] to cancel: ",
            dateTimeFormat);
        if (!startTime.HasValue)
        {
            return null;
        }

        var endTime = UserInputService.GetShiftEndDateTime(
            $"Enter the end date and time, format [blue]{dateTimeFormat}[/], or [blue]0[/] to cancel: ",
            dateTimeFormat,
            startTime.Value);
        if (!endTime.HasValue)
        {
            return null;
        }

        return new UpdateShiftRequest
        {
            Id = shift.Id,
            StartTime = startTime.Value,
            EndTime = endTime.Value,
        };
    }

    #endregion
}

