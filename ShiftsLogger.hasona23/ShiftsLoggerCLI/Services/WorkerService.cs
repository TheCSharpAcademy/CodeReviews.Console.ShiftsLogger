using ShiftsLoggerCLI.Enums;
using ShiftsLoggerCLI.Models;
using Spectre.Console;

namespace ShiftsLoggerCLI.Services;

public class WorkerService(InputHandler inputHandler)
{
    private  void AddWorker()
    {
        WorkerCreate worker = inputHandler.CreateWorker();
        ResponseManager.AddWorker(worker).Wait();
    }

    private void DeleteWorker()
    {
        var workers = ResponseManager.GetAllWorkers().Result;
        if (workers.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No Workers Found![/]");
            MenuBuilder.EnterButtonPause();
            return;
        }
        int id = inputHandler.ChooseWorkerFromSelection(workers).Id;
        ResponseManager.DeleteWorker(id).Wait();
    }

    private void UpdateWorker()
    {
        var workers = ResponseManager.GetAllWorkers().Result;
        if (workers.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No Workers Found![/]");
            MenuBuilder.EnterButtonPause();
            return;
        }
        int id = inputHandler.ChooseWorkerFromSelection(workers).Id;
        var newWorker = inputHandler.GetWorkerUpdate(id);
        ResponseManager.UpdateWorker(newWorker).Wait();
        
    }

    private  void GetWorkerDetails()
    {
        int id = inputHandler.ChooseWorkerFromSelection(ResponseManager.GetAllWorkers().Result).Id;
        
        WorkerRead? worker =  ResponseManager.GetWorker(id).Result;
        if (worker == null)
        {
            AnsiConsole.MarkupLine($"Couldn't find worker with id: {id}\npress enter to continue...");
            Console.ReadLine();
            return;
        }
        List<ShiftRead> workerShifts = ResponseManager.GetShiftsByWorkerId(worker.Id).Result;
        VisualisationEngine.DisplayWorker(worker,workerShifts);
    }

    private void GetAllWorkers()
    {
        var workers =  ResponseManager.GetAllWorkers().Result;
        if (workers.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No Workers Found[/]");
            MenuBuilder.EnterButtonPause();
            return;
        }
        var task = new Task(()=>PositionManager.SetPositions(workers));
        task.Start();
        VisualisationEngine.DisplayWorkers(workers);
        task.Wait();
    }
    public void HandleWorkers()
    {

        switch (MenuBuilder.DisplayShiftsWorkerOptions(true))
        {
            case Crud.Add:
                AddWorker();
                break;
            case Crud.Update:
                UpdateWorker();
                break;
            case Crud.Delete:
                DeleteWorker();
                break;
            case Crud.Read:
                GetWorkerDetails();
                break;
            case Crud.ReadAll:
                GetAllWorkers();
                break;
            case Crud.Return:
                break;
        }
        
        
    }
}