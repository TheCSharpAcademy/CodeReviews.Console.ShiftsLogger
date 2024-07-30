namespace ShiftsLogger.ConsoleApp.Enums;

/// <summary>
/// Supported choices for all menu pages in the application.
/// </summary>
internal enum MenuChoice
{
    [System.ComponentModel.Description("Default")]
    Default,
    [System.ComponentModel.Description("Close application")]
    CloseApplication,
    [System.ComponentModel.Description("Close page")]
    ClosePage,
    [System.ComponentModel.Description("Log a shift")]
    CreateShift,
    [System.ComponentModel.Description("Delete a shift")]
    DeleteShift,
    [System.ComponentModel.Description("Update a shift")]
    UpdateShift,
    [System.ComponentModel.Description("View all shifts")]
    ViewShifts,
}
