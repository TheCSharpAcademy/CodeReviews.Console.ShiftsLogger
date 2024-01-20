using System.ComponentModel;

namespace ShiftsLoggerUI.UI;

public enum MainMenuOptions
{
    [Description("View Shifts")]
    ViewShifts,
    [Description("Add a Shift")]
    AddShift,
    [Description("Delete a Shift")]
    DeleteShift,
    [Description("Update a Shift")]
    UpdateShift,
    [Description("Exit")]
    Exit
}

public enum UpdateMenuOptions
{
    [Description("Employee ID")]
    EmployeeId,
    [Description("Date")]
    Date,
    [Description("Start Time")]
    StartTime,
    [Description("End Time")]
    EndTime,
    [Description("Comment")]
    Comment,
    [Description("Save")]
    Save
}