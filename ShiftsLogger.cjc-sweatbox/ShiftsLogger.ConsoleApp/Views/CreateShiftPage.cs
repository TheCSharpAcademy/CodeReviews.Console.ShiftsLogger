using ShiftsLogger.ConsoleApp.Constants;
using ShiftsLogger.ConsoleApp.Models;
using ShiftsLogger.ConsoleApp.Services;

namespace ShiftsLogger.ConsoleApp.Views;

/// <summary>
/// Page which allows users to create a shift entry.
/// </summary>
internal class CreateShiftPage : BasePage
{
    #region Constants

    private const string PageTitle = "Create Shift";

    #endregion
    #region Methods

    internal static CreateShiftRequest? Show()
    {
        WriteHeader(PageTitle);

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

        return new CreateShiftRequest
        {
            StartTime = startTime.Value,
            EndTime = endTime.Value,
        };
    }

    #endregion
}

