namespace UI;
internal class Enums
{
    public enum MainMenuOptions
    {
        Shifts,
        Workers,
        Quit
    }

    public enum CurrentShiftOptions
    {
        CurrentShiftInformation,
        Start,
        End,
        Back
    }

    public enum ShiftsMenuOptions
    {
        ViewAllShifts,
        CurrentShift,
        EditShift,
        DeleteShift,
        Back
    }

    public enum EditShiftMenu 
    { 
        Worker,
        StartTime,
        EndTime,
        Back,
    }

    public enum EditWorkerMenu
    {
        Name,
        Department,
        Back
    }

    public enum WorkersMenuOptions
    {
        AddWorker,
        ViewAllWorkers,
        ViewAllShiftsByWorker,
        EditWorker,
        DeleteWorker,
        Back
    }
}
