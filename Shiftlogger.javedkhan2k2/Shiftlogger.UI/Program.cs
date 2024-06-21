using System.Security.Cryptography.X509Certificates;
using Shiftlogger.UI;
using Shiftlogger.UI.Controllers;
using Spectre.Console;

Menu menu = new Menu();
WorkerController workerController = new WorkerController();
ShiftController shiftController = new ShiftController();

Console.Clear();
bool runApplication = true;
while (runApplication)
{
    AnsiConsole.Clear();
    var choice = menu.GetMainMenu();
    switch (choice)
    {
        case "Workers":
            await ViewWorkerMenu();
            break;
        case "Shifts":
            await ViewShiftMenu();
            break;
        case "Exit":
            runApplication = false;
            break;
        default:
            break;
    }
}

async Task ViewShiftMenu()
{
    while (true)
    {
        AnsiConsole.Clear();
        var choice = menu.GetShiftMenu();
        switch (choice)
        {
            case "View All Shifts":
                await shiftController.DisplayAllShifts();
                break;
            case "Add Shift":
                await shiftController.AddShift();
                break;
            case "Update Shift":
                await shiftController.UpdateShift();
                break;
            case "Delete Shift":
                await shiftController.DeleteShift();
                break;
            case "[maroon]Go Back[/]":
                return;
            default:
                break;
        }
    }
}

async Task ViewWorkerMenu()
{
    while (true)
    {
        AnsiConsole.Clear();
        var choice = menu.GetWorkerMenu();
        switch (choice)
        {
            case "View All Workers":
                await workerController.DisplayAllWorkers();
                break;
            case "Add Worker":
                await workerController.AddWorker();
                break;
            case "Update Worker":
                await workerController.UpdateWorker();
                break;
            case "Delete Worker":
                await workerController.DeleteWorker();
                break;
            case "[maroon]Go Back[/]":
                return;
            default:
                break;
        }
    }
}