using ShiftsLoggerUI.Models;
using ShiftsLoggerUI.Services;
using Spectre.Console;

namespace ShiftsLoggerUI.Controllers;

internal class WorkerController
{
    private readonly WorkerService _workerService = new();

    internal async Task Operate()
    {
        // var workers = await _workerService.GetWorkersAsync();
        // await _workerService.CreateTable(workers);

        var editedWorker = await EditWorker();
        await _workerService.UpdateWorker(editedWorker);
    }

    private async Task<WorkerDto> EditWorker()
    {
        var workerDtoToUpdate = await _workerService.GetWorkerAsync(1);

        var columnToUpdate = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose the property to edit")
                .AddChoices("FirstName", "LastName", "Email", "Role"));

        var newValue = AnsiConsole.Ask<string>($"Provide new value for {columnToUpdate} property: ");

        switch (columnToUpdate)
        {
            case "FirstName":
                workerDtoToUpdate.FirstName = newValue;
                break;
            case "LastName":
                workerDtoToUpdate.LastName = newValue;
                break;
            case "Email":
                workerDtoToUpdate.Email = newValue;
                break;
            case "Role":
                workerDtoToUpdate.Role = newValue;
                break;
        }

        return workerDtoToUpdate;
    }
}
