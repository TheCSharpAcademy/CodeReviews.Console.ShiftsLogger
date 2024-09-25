namespace ShiftLoggerUI;

public enum ShiftMenuOptions
{
    [Title("Quit")]
    Quit,
    [Title("Create Shift")]
    CreateShift,
    [Title("Update Shift")]
    UpdateShift,
    [Title("Delete Shift")]
    DeleteShift,
    [Title("Show Shifts")]
    ShowShifts,
    [Title("Show Shifts by Employee")]
    ShowShiftsByEmployee,
    [Title("Manage Employees")]
    ManageEmployees,
}

public enum EmployeeMenuOptions
{
    [Title("Back")]
    Back,
    [Title("Create Employee")]
    CreateEmployee,
    [Title("Update Employee")]
    UpdateEmployee,
    [Title("Delete Employee")]
    DeleteEmployee,
    [Title("Show Employees")]
    ShowEmployees,
}