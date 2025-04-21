

public class ManageWorkerMenuController : MenuControllerBase
{
    WorkerService _workerService = new();
    internal override void OnMake()
    {
        base.OnMake();
        _workerService.ConnectApi();
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
        var workers = await _workerService.GetAllWorkersAsync();
        int availableId = 1;

        foreach(Worker worker in workers)
        {
            // Searches for the next worker id that is not 'in series' 
            // ie: Ids(0, 1, 2, 4, 5) has 3 missing and will give 3
            //     Ids(0, 1, 2, 3, 4) will give 5.
            if (workers.Find(workerId => workerId.WorkerId == worker.WorkerId + 1) == null)
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

        await _workerService.CreateWorkerAsync(newWorker);
    }

    private async Task ReadWorkerAsync()
    {
        throw new NotImplementedException();
    }

    private async Task ReadWorkerByIdAsync()
    {
        throw new NotImplementedException();
    }

    private async Task UpdateWorkerAsync()
    {
        throw new NotImplementedException();
    }

    private async Task DeleteWorkerAsync()
    {
        throw new NotImplementedException();
    }
}