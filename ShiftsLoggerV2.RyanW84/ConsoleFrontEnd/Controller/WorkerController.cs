using ConsoleFrontEnd.MenuSystem;
using ConsoleFrontEnd.Models.Dtos;
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
            if (createdWorker.ResponseCode is not System.Net.HttpStatusCode.OK || createdWorker.ResponseCode is not System.Net.HttpStatusCode.Created)
            {
				Console.WriteLine($"Error {createdWorker.ResponseCode}");
                AnsiConsole.MarkupLine("[red]Error: Failed to create worker.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[green]Worker created successfully![/]");
                AnsiConsole.MarkupLine($"[green]Worker ID: {createdWorker.Data.WorkerId}[/]");
            }
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
            if (worker.Data is null)
            {
                AnsiConsole.Markup("[red] No Workers returned[/]");
                return;
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
            var workerExists = existingWorker != null && existingWorker.Data.Count > 0;
            if (!workerExists)
            {
                AnsiConsole.MarkupLine("[red]Error: Worker not found.[/]");
                UpdateWorker();
                return;
            }

            var updatedWorker = userInterface.UpdateWorkerUi(existingWorker.Data);

            var updatedWorkerResponse = await workerService.UpdateWorker(workerId, updatedWorker);
            if (updatedWorkerResponse == null || updatedWorkerResponse.Data == null)
            {
                AnsiConsole.MarkupLine("[red]Error: Failed to update worker.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[green]Worker updated successfully![/]");
                AnsiConsole.MarkupLine(
                    $"[green]Worker ID: {updatedWorkerResponse.Data.WorkerId}[/]"
                );
            }
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
            if (deletedWorker is null)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
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
