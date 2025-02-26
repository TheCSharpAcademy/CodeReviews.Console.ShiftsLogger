namespace ShiftLoggerUi;

internal class Enums
{
    internal enum MainMenuOptions
    {
        ManageShifts,
        ManageWorkers,
        ManageDepartments,
        Quit
    }

    internal enum ShiftMenu
    {
        AddShift,
        ViewAllShifts,
        ViewShiftsByWorker,
        ViewShiftsByDepartment,
        UpdateShift,
        DeleteShift,
        GoBack
    }

    internal enum WorkerMenu
    {
        AddWorker,
        ViewAllWorkers,
        UpdateWorker,
        DeleteWorker,
        GoBack
    }

    internal enum DepartmentMenu
    {
        AddDepartment,
        ViewAllDepartments,
        UpdateDepartment,
        DeleteDepartment,
        GoBack
    }
}
