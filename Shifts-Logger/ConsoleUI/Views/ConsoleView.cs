using ConsoleUI.Models;
using ConsoleUI.Services;
using Spectre.Console;

namespace ConsoleUI.Views;

internal class ConsoleView
{
    private readonly WorkerShiftService _workerShiftService;
    public ConsoleView(WorkerShiftService workerShiftService)
    {
        _workerShiftService = workerShiftService;
    }
    internal async Task DisplayMainMenu()
    {
        string mainMenuChoice = "";
        do
        {
            string managerCode = "";
            int selectedId = 0;
            bool success = false;

            Console.Clear();
            mainMenuChoice = await AnsiConsole.PromptAsync(new SelectionPrompt<string>()
                                       .Title("Welcome! Choose below:")
                                       .AddChoices(new List<string>()
                                       {
                                        "View Shifts",
                                        "View Workers",
                                        "Create New Worker",
                                        "Create New Shift",
                                        "Update Worker",
                                        "Update Shift",
                                        "Delete Worker",
                                        "Delete Shift",
                                        "Quit"
                                       })
                                       .PageSize(10));
            switch (mainMenuChoice)
            {
                case "View Shifts":
                    await DisplayAllShiftsById();
                    // view all shifts
                    break;

                case "View Workers":
                    await DisplayAllWorkers();
                    // view all workers
                    break;

                case "Create New Worker":
                    Worker createWorker= await GetInputsForWorker(false);
                    success = await _workerShiftService.CreateNewWorker(createWorker);
                    AnsiConsole.MarkupLine(success ? "[green]Success![/]" : "[maroon]Failure :([/]");
                    // create - input form
                    break;

                case "Create New Shift":
                    Shift createShift = await GetInputsForShift(false);
                    success = await _workerShiftService.CreateNewShift(createShift);
                    AnsiConsole.MarkupLine(success ? "[green]Success![/]" : "[maroon]Failure :([/]");
                    // create - input form
                    break;

                case "Update Worker":
                    selectedId = await SelectWorkerById();
                    Worker updateWorker = await GetInputsForWorker(true, selectedId);
                    managerCode = await GetInputManagerCode();

                    success = await _workerShiftService.UpdateWorker(updateWorker, managerCode);
                    AnsiConsole.MarkupLine(success ? "[green]Success![/]" : "[maroon]Failure :([/]");
                    // view all workers
                    break;

                case "Update Shift":
                    Shift updateShift = await GetInputsForShift(true);
                    managerCode = await GetInputManagerCode();

                    success = await _workerShiftService.UpdateShift(updateShift, managerCode);
                    AnsiConsole.MarkupLine(success ? "[green]Success![/]" : "[maroon]Failure :([/]");
                    // view all workers, view shifts per worker
                    // or just view all shifts by id
                    break;

                case "Delete Worker":
                    selectedId = await SelectWorkerById();
                    managerCode = await GetInputManagerCode();
                    success = await _workerShiftService.DeleteWorker(new Worker { Id = selectedId }, managerCode);
                    AnsiConsole.MarkupLine(success ? "[green]Success![/]" : "[maroon]Failure :([/]");
                    // view all workers with id
                    break;

                case "Delete Shift":
                    selectedId = await SelectShiftById();
                    managerCode = await GetInputManagerCode();
                    success = await _workerShiftService.DeleteShift(new Shift { Id = selectedId }, managerCode);
                    AnsiConsole.MarkupLine(success ? "[green]Success![/]" : "[maroon]Failure :([/]");
                    // view all shifts per worker, view all shifts by id
                    break;

                case "Quit":
                    break;

                default:
                    AnsiConsole.MarkupLine("[maroon]An unexpected error occurred. Please try again later.[/]");
                    break;
            }

            AnsiConsole.MarkupLine("\nPress the [yellow]Enter[/] key to continue.");
            Console.ReadLine();
        } while (mainMenuChoice != "Quit");
    }

    private async Task DisplayAllShiftsById()
    {
        string shiftChoice = "";
        List<Shift> allShifts = await _workerShiftService.GetAllShifts();
        List<String> shiftDetails = allShifts
                    .Select(shift => $"Shift ID: {shift.Id} | Worker ID: {shift.WorkerId} | Start: {shift.StartTime} | End: {shift.EndTime}")
                    .ToList();

        shiftDetails.Add("Go Back");
        do
        {
            shiftChoice = await AnsiConsole.PromptAsync(new SelectionPrompt<string>()
                                            .Title("These are all the shifts:")
                                            .AddChoices(shiftDetails)
                                            .PageSize(10));
            Console.WriteLine(shiftChoice);
        } while (shiftChoice != "Go Back");
    }

    private async Task DisplayAllWorkers()
    {
        string workerChoice = "";
        List<Worker> allWorkers = await _workerShiftService.GetAllWorkers();
        List<String> workerDetails = allWorkers
                        .Select(w => $"Worker ID: {w.Id} | Name: {w.Name}")
                        .ToList();
        workerDetails.Add("Go Back");
        do
        {
            workerChoice = await AnsiConsole.PromptAsync(new SelectionPrompt<string>()
                                .Title("These are all the workers:")
                                .AddChoices(workerDetails)
                                .PageSize(10));
        } while (workerChoice != "Go Back");
    }

    private async Task<int> SelectWorkerById()
    {
        string workerChoice = "";
        int workerId = 0;
        List<Worker> allWorkers = await _workerShiftService.GetAllWorkers();
        List<string> workerIds = allWorkers.Select(w => w.Id.ToString()).ToList();

        workerChoice = await AnsiConsole.PromptAsync(new SelectionPrompt<string>()
                                .Title("Select your worker by id:")
                                .AddChoices(workerIds)
                                .PageSize(10));
        if (!String.IsNullOrEmpty(workerChoice))
        {
            workerId = int.Parse(workerChoice);
        }
        return workerId;
    }

    private async Task<int> SelectShiftById()
    {
        string shiftChoice = "";
        int shiftId = 0;
        List<Shift> allShifts = await _workerShiftService.GetAllShifts();
        List<string> shiftIds = allShifts.Select(w => w.Id.ToString()).ToList();
        shiftChoice = await AnsiConsole.PromptAsync(new SelectionPrompt<string>()
                                .Title("Select your shift by id:")
                                .AddChoices(shiftIds)
                                .PageSize(10));
        if (!String.IsNullOrEmpty(shiftChoice))
        {
            shiftId = int.Parse(shiftChoice);
        }
        return shiftId;
    }

    private async Task<int> SelectShiftsByWorkerId(int workerId)
    {

        string shiftChoice = "";
        int shiftId = 0;
        List<Shift> workerShifts = await _workerShiftService.GetAllShifts();
        workerShifts = workerShifts.Where(s  => s.WorkerId == workerId).ToList();

        List<string> shiftIds = workerShifts.Select(w => w.Id.ToString()).ToList();
        shiftChoice = await AnsiConsole.PromptAsync(new SelectionPrompt<string>()
                                .Title("Choose which shift to view:")
                                .AddChoices(shiftIds)
                                .PageSize(10));
        if (!String.IsNullOrEmpty(shiftChoice))
        {
            shiftId = int.Parse(shiftChoice);
        }
        return shiftId;
    }

    private static async Task<Worker> GetInputsForWorker(bool isUpdating, int workerId=-1)
    {
        string name = await AnsiConsole.PromptAsync(new TextPrompt<string>("What's the worker's name?"));
        if (isUpdating)
        {
            return new Worker { Id = workerId, Name = name };  
        }
        return new Worker { Name = name };
    }

    private async Task<Shift> GetInputsForShift(bool isUpdating)
    {
        int shiftId = -1;

        int workerId = await SelectWorkerById();
        if (isUpdating)
        {
            shiftId = await SelectShiftsByWorkerId(workerId);
        }

        DateTime startTime = await AnsiConsole.PromptAsync(
                                new TextPrompt<DateTime>("Please enter the start date and time of the shift as yyyy/MM/dd HH:mm:ss")
                                .Validate(dt => dt > DateTime.MinValue
                                    ? ValidationResult.Success()
                                    : ValidationResult.Error("[red]I didn't understand that number.[/]")));
        DateTime endTime = await AnsiConsole.PromptAsync(
                                new TextPrompt<DateTime>("Please enter the end date and time of the shift as yyyy/MM/dd HH:mm:ss")
                                .Validate(dt => dt > DateTime.MinValue
                                    ? ValidationResult.Success()
                                    : ValidationResult.Error("[red]I didn't understand that number.[/]")));

        if (isUpdating)
        {
            return new Shift { Id = shiftId, WorkerId = workerId, StartTime = startTime, EndTime = endTime };
        }
        return new Shift { WorkerId = workerId, StartTime = startTime, EndTime = endTime };
    }

    private static async Task<string> GetInputManagerCode()
    {
        string code = await AnsiConsole.AskAsync<string>("What is the manager code?");
        return code;
    }
}
