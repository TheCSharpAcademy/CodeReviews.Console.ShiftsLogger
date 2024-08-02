using Models;
using ShiftsLogger.Services;

namespace ShiftsLogger.Controllers;

public class WorkerShiftController {
    private readonly ApiService _service;
    public static readonly List<string> Options = ["Log a worker's shift", "Edit a worker's logged shift", "View a worker's worked shifts", "View all workers worked shifts", "Delete a logged shift"];
    private readonly Dictionary<string, Action> _optionHandlers;
    public WorkerShiftController(ApiService service)
    {
        _service = service; 
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
    {
        var workers = _service.GetAllWorkers();
        if (workers == null) {
            UI.ConfirmationMessage("No workers to log a shift for");
            return;
        }
        UI.DisplayWorkers(workers);

        var worker = WorkerController.SelectWorkerFromList(workers, "select");

        UI.Clear();

        var shifts = _service.GetAllShifts();
        if (shifts == null)
        {
            UI.ConfirmationMessage("No shifts to assign to worker");
            return;
        }

        UI.DisplayShifts(shifts);
        var shift = ShiftController.SelectShiftFromList(shifts, "select");

        var shiftDate = UI.DateOnlyResponseWithDefault("Enter the [green]shift date[/] the worker worked on. ", DateOnly.FromDateTime(DateTime.Today));

        var workerShift = new PostWorkerShiftDto{
            WorkerId = worker.Id,
            ShiftId = shift.Id,
            ShiftDate = shiftDate
        };

        try {
            _service.CreateWorkerShift(workerShift);
            UI.ConfirmationMessage("Logged worker shift");
        }catch(Exception e) {
            UI.ConfirmationMessage(e.Message);
        }
    }

    public void EditWorkerShift()
    {
       var workerShifts = _service.GetAllWorkerShifts();

       if (workerShifts == null)
       {
        UI.ConfirmationMessage("No worker shifts to edit");
        return;
       } 

       UI.DisplayWorkerShifts(workerShifts);

       var workerShift = SelectWorkerShiftFromList(workerShifts, "edit");

       var shiftDate = UI.DateOnlyResponseWithDefault("Enter the [green]shift date[/]", workerShift.ShiftDate);
    
        var workers = _service.GetAllWorkers();
        if (workers == null)
        {
            UI.ConfirmationMessage("No workers to add to worker shift");
            return;
        }
        UI.DisplayWorkers(workers);

        var worker = WorkerController.SelectWorkerFromList(workers, "assign to worker shift");

        var shifts = _service.GetAllShifts();
        if (shifts == null)
        {
            UI.ConfirmationMessage("No shifts to add to worker shift");
            return;
        }
        UI.DisplayShifts(shifts);

        var shift = ShiftController.SelectShiftFromList(shifts, "assign to worker shift");

        var dto = new PutWorkerShiftDto{
            WorkerId = worker.Id,
            ShiftId = shift.Id,
            ShiftDate = shiftDate,
        };
    
        try {
            _service.UpdateWorkerShift(workerShift.Id, dto);
            UI.ConfirmationMessage("Updated worker shift. ");
        } catch(Exception e)
        {
            UI.ConfirmationMessage(e.Message);
        }
    }

    public static GetWorkerShiftDto SelectWorkerShiftFromList(List<GetWorkerShiftDto> workerShifts, string action)
    {
        GetWorkerShiftDto? workerShift = null;
        while (workerShift == null)
        {
            var id = UI.IntResponse($"Enter the [green]id[/] of the worker shift you want to {action}");
            workerShift = workerShifts.FirstOrDefault(ws => ws.Id == id);

            if (workerShift == null)
            {
                UI.InvalidationMessage("No worker with that id");
            }
        }

        return workerShift;
    }

    public void ViewSingleWorkersShifts()
    {
        var workerShifts = _service.GetAllWorkerShifts();

        if (workerShifts == null)
        {
            UI.ConfirmationMessage("No worker shitfts to view");
            return;
        }

        var workers = _service.GetAllWorkers();
        if (workers == null)
        {
            UI.ConfirmationMessage("No workers to view worker shifts for. ");
            return;
        }

        UI.Clear();
        UI.DisplayWorkers(workers);

        var worker = WorkerController.SelectWorkerFromList(workers, "view their worker shifts");

        var filteredWorkerShifts = workerShifts.Where(ws => ws.WorkerId == worker.Id);

        if (filteredWorkerShifts.Count() == 0) {
            UI.ConfirmationMessage("No work shifts to list for that worker");
            return;
        }

        UI.Clear();
        UI.DisplayWorkerShifts(filteredWorkerShifts);
        UI.ConfirmationMessage("");
    }

    public void ViewAllWorkerShifts()
    {
        var workerShifts = _service.GetAllWorkerShifts();

        if (workerShifts == null)
        {
            UI.ConfirmationMessage("No worker shitfts to view");
            return;
        }

        UI.DisplayWorkerShifts(workerShifts);
        UI.ConfirmationMessage("");
    }

    public void DeleteWorkerShift()
    {
        var workerShifts = _service.GetAllWorkerShifts();

        if (workerShifts == null)
        {
            UI.ConfirmationMessage("No worker shifts to view");
            return;
        }

        UI.DisplayWorkerShifts(workerShifts);
        var workerShift = SelectWorkerShiftFromList(workerShifts, "delete");

        try {
            _service.DeleteWorkerShift(workerShift.Id);
            UI.ConfirmationMessage("Worker shift deleted. ");
        }catch(Exception e)
        {
            UI.ConfirmationMessage(e.Message);
        }
    }
    
}