using ShiftsLogger.Services;

namespace ShiftsLogger.Controllers;

public class WorkerShiftController {
    private readonly ApiService _api;
    public static readonly List<string> Options = ["Log a worker's shift", "Edit a worker's shift", "View a worker's worked shifts", "View all worker shifts", "Delete a logged shift"];
    private readonly Dictionary<string, Action> _optionHandlers;
    public WorkerShiftController(ApiService api)
    {
        _api = api; 
        _optionHandlers = new Dictionary<string, Action>{
            { Options[0], LogWorkerShift },
            { Options[1], EditWorkerShift },
            { Options[2], ViewSingleWorkersShifts },
            { Options[3], ViewAllWorkerShifts },
            { Options[4], DeleteWorkerShift }
        };
    }

    public void HandleChoice(string choice)
    {
        _optionHandlers.TryGetValue(choice, out var action);
        action!();
    }

    public void LogWorkerShift()
    {}

    public void EditWorkerShift()
    {}

    public void ViewSingleWorkersShifts()
    {}

    public void ViewAllWorkerShifts()
    {}

    public void DeleteWorkerShift()
    {}
    
}