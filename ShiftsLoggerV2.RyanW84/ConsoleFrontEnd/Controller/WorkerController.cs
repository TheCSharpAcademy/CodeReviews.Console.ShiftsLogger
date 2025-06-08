using System.Reflection.Metadata.Ecma335;
using ConsoleFrontEnd.Models.FilterOptions;
using ConsoleFrontEnd.Services;
using Spectre.Console;

namespace ConsoleFrontEnd.Controller;

internal class WorkerController
{
    internal readonly MenuSystem.UserInterface userInterface = new();
    internal readonly WorkerService workerService = new();
    internal WorkerFilterOptions workerFilterOptions = new() { Name = null };

    public async Task CreateWorker()
    {
        try
        {
            Console.Clear();
            AnsiConsole.Write(
                new Rule("[bold yellow]Create Worker[/]").RuleStyle("yellow").Centered()
            );
            var worker = userInterface.CreateWorkerUi();
            var createdWorker = await workerService.CreateWorker(worker);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Exception: {ex.Message}[/]");
        }
    }

    public async Task GetAllWorkers()
    {
        try
        {
            Console.Clear();
            AnsiConsole.Write(
                new Rule("[bold yellow]View All Workers[/]").RuleStyle("yellow").Centered()
            );

            var filterOptions = userInterface.FilterWorkersUi();

            workerFilterOptions = filterOptions;
            var workers = await workerService.GetAllWorkers(workerFilterOptions);
            userInterface.DisplayWorkersTable(workers.Data);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Exception: {ex.Message}[/]");
        }
    }

    public async Task GetWorkerById()
    {
        try
        {
            Console.Clear();
            AnsiConsole.Write(
                new Rule("[bold yellow]View Worker by ID[/]").RuleStyle("yellow").Centered()
            );
            var workerId = userInterface.GetWorkerByIdUi();
            var worker = await workerService.GetWorkerById(workerId);

            while (worker.ResponseCode is System.Net.HttpStatusCode.NotFound)
            {
                var exitSelection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Try again or exit?")
                        .AddChoices(new[] { "Try Again", "Exit" })
                );
                if (exitSelection is "Exit")
                {
                    Console.Clear();
                    return;
                }
                else if (exitSelection is "Try Again")
                {
                    Console.Clear();
                    workerId = userInterface.GetWorkerByIdUi();
                    worker = await workerService.GetWorkerById(workerId);
                }
            }

            userInterface.DisplayWorkersTable(worker.Data);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex}");
        }
    }

    public async Task UpdateWorker()
    {
        try
        {
            Console.Clear();
            AnsiConsole.Write(
                new Rule("[bold yellow]Update Worker[/]").RuleStyle("yellow").Centered()
            );

            var workerId = userInterface.GetWorkerByIdUi();

            var existingWorker = await workerService.GetWorkerById(workerId);

            while (existingWorker.ResponseCode is System.Net.HttpStatusCode.NotFound)
            {
                var exitSelection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Try again or exit?")
                        .AddChoices(new[] { "Try Again", "Exit" })
                );
                if (exitSelection is "Exit")
                {
                    Console.Clear();
                    return;
                }
                else if (exitSelection is "Try Again")
                {
                    Console.Clear();
                    workerId = userInterface.GetWorkerByIdUi();
                    existingWorker = await workerService.GetWorkerById(workerId);
                }
            }

            var updatedWorker = userInterface.UpdateWorkerUi(existingWorker.Data);

            var updatedWorkerResponse = await workerService.UpdateWorker(workerId, updatedWorker);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Exception: {ex.Message}[/]");
        }
    }

    public async Task DeleteWorker()
    {
        try
        {
            Console.Clear();
            AnsiConsole.Write(
                new Rule("[bold yellow]Delete Worker[/]").RuleStyle("yellow").Centered()
            );
            var workerId = userInterface.GetWorkerByIdUi();
            var deletedWorker = await workerService.DeleteWorker(workerId);
            if (deletedWorker.ResponseCode is System.Net.HttpStatusCode.NotFound)
            {
                var exitSelection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Try again or exit?")
                        .AddChoices(new[] { "Try Again", "Exit" })
                );
                if (exitSelection == "Exit")
                {
                    Console.Clear();
                    return;
                }
                else
                {
                    Console.Clear();
                    await DeleteWorker(); // Retry
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Try Pass failed in Worker Controller: Delete Worker {ex}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
