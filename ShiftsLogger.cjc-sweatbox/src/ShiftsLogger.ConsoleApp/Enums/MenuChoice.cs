using System.ComponentModel;

namespace ShiftsLogger.ConsoleApp.Enums;

/// <summary>
/// Supported choices for all menu pages in the application.
/// </summary>
internal enum MenuChoice
{
    [Description("Default")]
    Default,
    [Description("Close application")]
    CloseApplication,
    [Description("Close page")]
    ClosePage,
    [Description("Log a shift")]
    CreateShift,
    [Description("Delete a shift")]
    DeleteShift,
    [Description("Update a shift")]
    UpdateShift,
    [Description("View all shifts")]
    ViewShifts,
}
