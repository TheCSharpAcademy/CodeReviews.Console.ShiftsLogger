using Models;
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
        var workers = _service.GetAllWorkers();

        if (workers == null) {
            UI.ConfirmationMessage("No workers to view");
            return;
        }

        UI.DisplayWorkers(workers);

        UI.ConfirmationMessage("");
    }

    private void EditWorker()
    {
        var workers = _service.GetAllWorkers();

        if (workers == null)
        {
            UI.ConfirmationMessage("No workers to edit");
            return;
        }

        UI.DisplayWorkers(workers);
        GetWorkerDto worker = SelectWorkerFromList(workers, "edit");

        var firstName = UI.StringResponseWithDefault($"Enter the [green]worker's first name[/]", worker.FirstName);
        var lastName = UI.StringResponseWithDefault($"Enter the [green]worker's last name[/]", worker.LastName);
        var position = UI.StringResponseWithDefault($"Enter the [green]worker's position name[/]", worker.Position);

        var dto = new PutWorkerDto
        {
            FirstName = firstName,
            LastName = lastName,
            Position = position
        };

        try
        {
            _service.UpdateWorker(dto, worker.Id);
            UI.ConfirmationMessage("Worker updated");
        }
        catch (Exception e)
        {
            UI.ConfirmationMessage("Something went wrong: " + e.Message);
        }
    }

    public static GetWorkerDto SelectWorkerFromList(List<GetWorkerDto> workers, string action)
    {
        GetWorkerDto? worker = null;
        while (worker == null)
        {
            var id = UI.IntResponse($"Enter the [green]id[/] of the worker you want to {action}");
            worker = workers.FirstOrDefault(w => w.Id == id);

            if (worker == null)
            {
                UI.InvalidationMessage("No worker with that id");
            }
        }

        return worker;
    }

    private void AddWorker()
    {
        var firstName = UI.StringResponse("Enter the worker's [green]first name[/]:");
        var lastName = UI.StringResponse("Enter the worker's [green]last name[/]:");
        var position = UI.StringResponse("Enter the worker's [green]position[/]:");
        
        var dto = new PostWorkerDto{
            FirstName = firstName,
            LastName = lastName,
            Position = position
        };

        try {
            _service.CreateWorker(dto);
            UI.ConfirmationMessage("Worker created");
        } catch(Exception e)
        {
            UI.ConfirmationMessage("Something went wrong: " + e.Message);
        }
    }

    private void DeleteWorker()
    {
        var workers = _service.GetAllWorkers();

        if (workers == null) {
            UI.ConfirmationMessage("No workers to delete");
            return;
        }

        UI.DisplayWorkers(workers);

        var worker = SelectWorkerFromList(workers, "delete");

        try {
            _service.DeleteWorker(worker.Id);
            UI.ConfirmationMessage("Worker deleted");
        } catch(Exception e) {
            UI.ConfirmationMessage("Something went wrong: " + e.Message);
        }
    }
}
