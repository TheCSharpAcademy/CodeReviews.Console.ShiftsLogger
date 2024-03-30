namespace ShiftLoggerClient.Models;

internal class Enums
{
    internal enum MainMenuOptions
    {
        ManagementFunctions,
        PunchCard,
        ExitProgram

    }

    internal enum ManagmentFunctionOptions
    {
        ManageEmployees,
        ManageShifts,
        Back
    }

    internal enum ManageEmployeeOptions
    {
        AddEmployee,
        UpdateEmployee,
        DeleteEmployee, //do I really want to delete employees?
        Back
    }

    internal enum ManageShiftsOptions
    {
        ViewEditAllShifts,
        ViewEditAllShiftsByEmployee,
        ViewOpenShifts,
        Back

    }
    internal enum ManageShiftOptions
    {
        NewShift,
        UpdateShift,
        DeleteShift,
        Back   
    }

    internal enum ContinueExitMenuOptions
    {
        Continue,
        ReEnter,
        ExitMenu
    }
    internal enum ContinueBackOptions
    {
        Continue,
        Back
    }

}
