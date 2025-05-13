namespace ShiftsLogger.KamilKolanowski.Enums;

internal class ShiftsLoggerMenu
{
    internal enum MainMenu
    {
        Worker,
        Shift
    }

    internal enum WorkerMenu
    {
        AddWorker,
        EditWorker,
        DeleteWorker,
    }

    internal static Dictionary<WorkerMenu, string> WorkerMenuType { get; } = new()
    {
        { WorkerMenu.AddWorker, "Add Worker" },
        { WorkerMenu.EditWorker, "Edit Worker" },
        { WorkerMenu.DeleteWorker, "Delete Worker" },
    };

    internal enum ShiftMenu
    {
        AddShift,
        EditShift,
        DeleteShift,
    }

    internal static Dictionary<ShiftMenu, string> ShiftMenuType { get; } = new()
    {
        { ShiftMenu.AddShift, "Add Shift" },
        { ShiftMenu.EditShift, "Edit Shift" },
        { ShiftMenu.DeleteShift, "Delete Shift" },
    };
}