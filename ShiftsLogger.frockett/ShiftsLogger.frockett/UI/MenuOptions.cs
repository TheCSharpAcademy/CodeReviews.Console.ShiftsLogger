using System.ComponentModel;

namespace ShiftsLogger.frockett.UI.UI;
public enum MainMenuOptions
{
    [Description("View All Shifts")]
    ViewShifts,
    [Description("View Employee's Shifts")]
    ViewEmployeeShifts,
    [Description("Add a Shift")]
    AddShift,
    [Description("Delete a Shift")]
    DeleteShift,
    [Description("Update a Shift")]
    UpdateShift,
    [Description("Add Employee")]
    AddEmployee,
    [Description("Delete Employee")]
    DeleteEmployee,
    [Description("Update Employee")]
    UpdateEmployee,
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
