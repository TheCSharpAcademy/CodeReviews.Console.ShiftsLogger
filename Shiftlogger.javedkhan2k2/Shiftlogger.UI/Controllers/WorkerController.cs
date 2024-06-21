using Shiftlogger.UI.DTOs;
using Shiftlogger.UI.Services;

namespace Shiftlogger.UI.Controllers;

internal class WorkerController
{
    private readonly WorkerService _workerService;

    public WorkerController()
    {
        _workerService = new WorkerService();
    }

    internal async Task DisplayAllWorkers()
    {
        var workers = await _workerService.GetWorkers();
        if (workers != null)
        {
            VisualizationEngine.DisplayWorkers(workers, "Showing All Workers");
        }
        VisualizationEngine.DisplayContinueMessage();
    }

    internal async Task AddWorker()
    {
        var worker = UserInput.GetNewWorker();
        if (worker == null)
        {
            VisualizationEngine.DisplayCancelOperation();
            return;
        }
        var addedWorker = await _workerService.AddWorker(worker);
        if (addedWorker == null)
        {
            VisualizationEngine.DisplayFailureMessage("Worker is not added to database.");
        }
        else
        {
            VisualizationEngine.DisplaySuccessMessage("Worker is added to database successfully.");
        }
    }

    internal async Task UpdateWorker()
    {
        var workers = await GetAllWorkers();
        VisualizationEngine.DisplayWorkers(workers, "All Workers");
        var workerId = UserInput.GetIntInput();
        if(workerId == 0)
        {
            VisualizationEngine.DisplayCancelOperation();
            return;
        }
        var worker = await GetWorkerById(workerId);
        
        if(worker == null)
        {
            VisualizationEngine.DisplayFailureMessage($"Worker with id: [green]{workerId}[/] not found.");
            return;
        }

        if(!UserInput.UpdateWorker(worker))
        {
            VisualizationEngine.DisplayCancelOperation();
            return;
        }
        if(!await _workerService.PutWorker(workerId, worker))
        {
            VisualizationEngine.DisplayFailureMessage("Worker is not updated.");
        }
        else
        {
            VisualizationEngine.DisplaySuccessMessage("Worker is updated.");
        }
    }

    internal async Task DeleteWorker()
    {
        var workers = await GetAllWorkers();
        VisualizationEngine.DisplayWorkers(workers, "All Workers");
        var workerId = UserInput.GetIntInput();
        if(workerId == 0)
        {
            VisualizationEngine.DisplayCancelOperation();
            return;
        }
        var worker = await GetWorkerById(workerId);
        
        if(worker == null)
        {
            VisualizationEngine.DisplayFailureMessage($"Worker with id: [green]{workerId}[/] not found.");
            return;
        }
        if(!await _workerService.DeleteWorker(workerId))
        {
            VisualizationEngine.DisplayFailureMessage("Worker is not deleted.");
        }
        else
        {
            VisualizationEngine.DisplaySuccessMessage("Worker is deleted.");
        }
    }

    internal async Task<List<WorkerRequestDto>> GetAllWorkers() => await _workerService.GetWorkers();

    internal async Task<WorkerRequestDto> GetWorkerById(int id) => await _workerService.GetWorkerById(id);

}