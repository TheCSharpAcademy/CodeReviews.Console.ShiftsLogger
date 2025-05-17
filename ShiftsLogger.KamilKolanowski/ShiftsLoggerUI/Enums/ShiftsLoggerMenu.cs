namespace ShiftsLogger.KamilKolanowski.Enums;

internal class ShiftsLoggerMenu
{
    internal enum MainMenu
    {
        Worker,
        Shift,
        Exit,
    }

    internal enum WorkerMenu
    {
        AddWorker,
        EditWorker,
        DeleteWorker,
        ViewWorkers,
        GoBack,
    }

    internal static Dictionary<WorkerMenu, string> WorkerMenuType { get; } =
        new()
        {
            { WorkerMenu.AddWorker, "Add Worker" },
            { WorkerMenu.EditWorker, "Edit Worker" },
            { WorkerMenu.DeleteWorker, "Delete Worker" },
            { WorkerMenu.ViewWorkers, "View Workers" },
            { WorkerMenu.GoBack, "Go Back" },
        };

    internal enum ShiftMenu
    {
        AddShift,
        EditShift,
        DeleteShift,
        ViewShifts,
        GoBack,
    }

    internal static Dictionary<ShiftMenu, string> ShiftMenuType { get; } =
        new()
        {
            { ShiftMenu.AddShift, "Add Shift" },
            { ShiftMenu.EditShift, "Edit Shift" },
            { ShiftMenu.DeleteShift, "Delete Shift" },
            { ShiftMenu.ViewShifts, "View Shifts" },
            { ShiftMenu.GoBack, "Go Back" },
        };
}
