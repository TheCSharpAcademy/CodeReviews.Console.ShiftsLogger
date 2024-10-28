using ShiftsLoggerCLI.Enums;
using ShiftsLoggerCLI.Models;
using Spectre.Console;

namespace ShiftsLoggerCLI.Handlers;

public class WorkerHandler(InputHandler inputHandler)
{
    private readonly InputHandler _inputHandler = inputHandler;

  
    private async void AddWorker()
    {
        WorkerCreate worker = _inputHandler.CreateWorker();
        ResponseManager.AddWorker(worker).Wait();
    }

    private void DeleteWorker()
    {
        int id = _inputHandler.ChooseWorkerFromSelection(ResponseManager.GetAllWorkers().Result).Id;
        ResponseManager.DeleteWorker(id).Wait();
    }

    private void UpdateWorker()
    {
        int id = _inputHandler.ChooseWorkerFromSelection(ResponseManager.GetAllWorkers().Result).Id;
        var newWorker = _inputHandler.GetWorkerUpdate(id);
        ResponseManager.UpdateWorker(newWorker).Wait();
        
    }

    private  void GetWorkerDetails()
    {
        int id = _inputHandler.ChooseWorkerFromSelection(ResponseManager.GetAllWorkers().Result).Id;
        
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
        }
        
        
    }
}