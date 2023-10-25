namespace ShiftsLogger.UI.Enums;

public enum ShiftsMenuOptions
{
    [EnumExtensions.DisplayText("View All Shifts")]
    ViewAllShifts,

    [EnumExtensions.DisplayText("View Shift Details")]
    ViewShiftDetails,

    [EnumExtensions.DisplayText("Add Shift")]
    AddShift,

    [EnumExtensions.DisplayText("Delete Shift")]
    DeleteShift,

    [EnumExtensions.DisplayText("Update Shift")]
    UpdateShift,

    [EnumExtensions.DisplayText("Go Back")]
    GoBack
}