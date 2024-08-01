using ShiftsLogger.Services;

namespace ShiftsLogger.Controllers;

public class WorkerController {
    private readonly ApiService _service;
    public static readonly List<string> Options = ["View workers", "Add worker", "Edit worker", "Delete worker"];
    private readonly Dictionary<string, Action> _optionHandlers;
    public WorkerController(ApiService service)
    {
        _service = service; 
        _optionHandlers = new Dictionary<string, Action>{
            { Options[0], ViewWorkers },
            { Options[1], AddWorker },
            { Options[2], EditWorker },
            { Options[3], DeleteWorker }
        };
    }

    public void HandleChoice(string choice) {
        _optionHandlers.TryGetValue(choice, out var action);
        action!(); 
    }

    private void ViewWorkers()
    {
        //get workers

        //display workers

        //confirm message
    }

    private void EditWorker()
    {
        //get workers

        // display workers

        // get id of worker to edit

        // get worker details

        // update worker
    }

    private void AddWorker()
    {
        //get worker details

        // create worker
    }

    private void DeleteWorker()
    {
        //get workers

        // display workers

        // get id of worker to delete

        // delete worker
    }
}
