using ShiftsLogger.KamilKolanowski.Services;
using Spectre.Console;

namespace ShiftsLogger.KamilKolanowski.Controllers;

internal class WorkerController
{
    private readonly WorkerService _workerService = new();

    internal async Task Operate()
    {
        var workers = await _workerService.GetWorkersAsync();
        await _workerService.CreateTable(workers);
    }
}
