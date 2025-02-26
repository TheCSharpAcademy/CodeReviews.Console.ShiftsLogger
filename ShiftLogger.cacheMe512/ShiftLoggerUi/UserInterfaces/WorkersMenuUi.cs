using ShiftLoggerUi.Services;
using Spectre.Console;
using ShiftLoggerUi.DTOs;
using static ShiftLoggerUi.Utilities;
using static ShiftLoggerUi.Enums;

namespace ShiftLoggerUi.UserInterfaces;

internal class WorkersMenuUi
{
    static internal void WorkersMenu()
    {
        var isWorkersMenuRunning = true;
        while (isWorkersMenuRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<WorkerMenu>()
                    .Title("Workers Menu")
                    .AddChoices(
                        WorkerMenu.AddWorker,
                        WorkerMenu.ViewAllWorkers,
                        WorkerMenu.UpdateWorker,
                        WorkerMenu.DeleteWorker,
                        WorkerMenu.GoBack));

            switch (option)
            {
                case WorkerMenu.AddWorker:
                    CreateWorker();
                    break;
                case WorkerMenu.ViewAllWorkers:
                    GetAllWorkers();
                    break;
                case WorkerMenu.UpdateWorker:
                    UpdateWorker();
                    break;
                case WorkerMenu.DeleteWorker:
                    DeleteWorker();
                    break;
                case WorkerMenu.GoBack:
                    isWorkersMenuRunning = false;
                    break;
            }
        }
    }

    public static void CreateWorker()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[cyan]Creating a new worker[/]");

        string firstName = UserInput.GetStringInput("Enter first name:");
        string lastName = UserInput.GetStringInput("Enter last name:");
        DateTime hireDate = UserInput.GetDateInput("Enter hire date (YYYY-MM-DD):");
        var department = UserInput.GetDepartmentOptionInput();
        if (department == null) return;

        var worker = new WorkerDto
        {
            FirstName = firstName,
            LastName = lastName,
            HireDate = hireDate,
            DepartmentId = department.DepartmentId
        };

        var workerService = new WorkerService();
        var createdWorker = workerService.CreateWorker(worker);

        if (createdWorker != null)
            AnsiConsole.MarkupLine("[green]Worker created successfully![/]");
        else
            DisplayMessage("Failed to create worker.", "red");

        DisplayMessage("Press any key to continue...");
        Console.ReadKey();
    }

    public static void GetAllWorkers()
    {
        var workerService = new WorkerService();
        var workers = workerService.GetAllWorkers();

        if (workers.Count == 0)
            DisplayMessage("No workers found.", "red");
        else
            ShowTable(workers, new[] { "Worker ID", "First Name", "Last Name", "Hire Date", "Department Name" },
                w => new[] { w.WorkerId.ToString(), w.FirstName, w.LastName, w.HireDate.ToString("yyyy-MM-dd"), w.DepartmentName ?? "N/A" });
    }

    public static void UpdateWorker()
    {
        Console.Clear();
        DisplayMessage("Select a worker to update:", "cyan");
        var selectedWorker = UserInput.GetWorkerOptionInput();
        if (selectedWorker == null) return;

        Console.Clear();
        string firstName = UserInput.GetStringInput("Enter new first name:");
        string lastName = UserInput.GetStringInput("Enter new last name:");
        DateTime hireDate = UserInput.GetDateInput("Enter new hire date (YYYY-MM-DD):");
        var department = UserInput.GetDepartmentOptionInput();
        if (department == null) return;

        var updatedWorker = new WorkerDto
        {
            WorkerId = selectedWorker.WorkerId,
            FirstName = firstName,
            LastName = lastName,
            HireDate = hireDate,
            DepartmentId = department.DepartmentId
        };

        var workerService = new WorkerService();
        if (workerService.UpdateWorker(selectedWorker.WorkerId, updatedWorker))
            DisplayMessage("Worker updated successfully!", "green");
        else
            DisplayMessage("Failed to update worker.", "red");

        DisplayMessage("Press any key to continue...");
        Console.ReadKey();
    }

    public static void DeleteWorker()
    {
        Console.Clear();
        DisplayMessage("Select a worker to delete:", "cyan");
        var selectedWorker = UserInput.GetWorkerOptionInput();
        if (selectedWorker == null) return;

        var workerService = new WorkerService();
        if (workerService.DeleteWorker(selectedWorker.WorkerId))
            DisplayMessage("Worker deleted successfully!", "green");
        else
            DisplayMessage("Failed to delete worker.", "red");

        DisplayMessage("Press any key to continue...");
        Console.ReadKey();
    }
}
