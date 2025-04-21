using Spectre.Console;

public class ManageWorkerMenuController : MenuControllerBase
{
    WorkerService _workerService = new();

    List<Worker> _workers;
    internal override async Task OnMakeAsync()
    {
        _workerService.ConnectApi();
        _workers = [.. (await _workerService.GetAllWorkersAsync()).OrderBy(worker => worker.WorkerId)];
    }
    internal override async Task<bool> HandleMenuSelectionAsync()
    {
        MenuEnums.Worker input = DisplayMenu.WorkersMenu();
        switch (input)
        {
            case MenuEnums.Worker.CREATEWORKER:
                await CreateWorkerAsync();
                break;
            case MenuEnums.Worker.READWORKER:
                await ReadWorkerAsync();
                break;
            case MenuEnums.Worker.READWORKERBYID:
                await ReadWorkerByIdAsync();
                break;
            case MenuEnums.Worker.UPDATEWORKER:
                await UpdateWorkerAsync();
                break;
            case MenuEnums.Worker.DELETEWORKER:
                await DeleteWorkerAsync();
                break;
            case MenuEnums.Worker.BACK:
                return true;
        }
        return false;
    }

    private async Task CreateWorkerAsync()
    {
        // Getting the most avaliable Id
        int availableId = 1;

        foreach(Worker worker in _workers)
        {
            // Searches for the next worker id that is not 'in series' 
            // ie: Ids(0, 1, 2, 4, 5) has 3 missing and will give 3
            //     Ids(0, 1, 2, 3, 4) will give 5.
            if (_workers.Find(workerId => workerId.WorkerId == worker.WorkerId + 1) == null)
            {
                availableId = worker.WorkerId + 1;
                break;
            }
        }

        Worker newWorkerWithId = new()
        {
            WorkerId = availableId,
        };
        WorkerDto newWorker = GetData.GetWorker(newWorkerWithId);

        // Error checks
        if (_workers.Find(workerId => workerId.WorkerId == newWorker.WorkerId) != null)
            throw new Exception("Overlapping worker id");

        await _workerService.CreateWorkerAsync(newWorker);
    }

    private async Task ReadWorkerAsync()
    {
        List<Worker> workers = await _workerService.GetAllWorkersAsync();
        DisplayTable.Worker(workers);
    }

    private async Task ReadWorkerByIdAsync()
    {
        int userEnteredId = GetData.FindWorker(_workers);
        Worker? worker = await _workerService.GetWorkerByIdAsync(userEnteredId) 
            ?? throw new Exception("Could not find data");
        
        DisplayTable.Worker([worker]);
    }

    private async Task UpdateWorkerAsync()
    {
        Worker workerToUpdate = GetData.GetWorker(_workers);
        WorkerDto updatedWorker = GetData.GetWorker(workerToUpdate);
        await _workerService.UpdateWorkerAsync(updatedWorker);
    }

    private async Task DeleteWorkerAsync()
    {
        throw new NotImplementedException();
    }
}