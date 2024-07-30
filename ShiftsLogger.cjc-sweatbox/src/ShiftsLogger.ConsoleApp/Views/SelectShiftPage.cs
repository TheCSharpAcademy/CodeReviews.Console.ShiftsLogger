using ShiftsLogger.ConsoleApp.Enums;
using ShiftsLogger.ConsoleApp.Models;
using ShiftsLogger.ConsoleApp.Services;
using ShiftsLogger.Extensions;
using Spectre.Console;

namespace ShiftsLogger.ConsoleApp.Views;

/// <summary>
/// A page to displays a list of shifts for selection.
/// </summary>
internal class SelectShiftPage : BasePage
{
    #region Constants

    private const string PageTitle = "Select Shift";

    #endregion
    #region Methods - Internal

    internal static ShiftDto? Show(IReadOnlyList<ShiftDto> shifts)
    {
        WriteHeader(PageTitle);

        var option = GetOption(shifts);

        return option.Id == Guid.Empty ? null : shifts.First(x => x.Id == option.Id);
    }

    #endregion
    #region Methods - Private

    private static SelectionChoice GetOption(IReadOnlyList<ShiftDto> shifts)
    {
        IEnumerable<SelectionChoice> pageChoices =
        [
            .. shifts.Select(x => new SelectionChoice { Id = x.Id, Name = x.ToSelectionChoice() }),
            new SelectionChoice { Name = MenuChoice.ClosePage.GetDescription() }
        ];

        return UserInputService.GetPageChoice(PromptTitle, pageChoices);
    }

    #endregion
}
