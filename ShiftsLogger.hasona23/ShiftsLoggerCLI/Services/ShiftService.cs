using ShiftsLoggerCLI.Enums;
using ShiftsLoggerCLI.Models;
using Spectre.Console;

namespace ShiftsLoggerCLI.Services;

public class ShiftService(InputHandler inputHandler)
{
    List<WorkerRead> Workers { get;  set; } = [];
    private void GetShifts()
    {
        int workerId = inputHandler.ChooseWorkerFromSelection(Workers).Id;
        var shifts = ResponseManager.GetShiftsByWorkerId(workerId).Result;
        if (shifts.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No shifts found![/]");
            MenuBuilder.EnterButtonPause();
            return;
        }
        VisualisationEngine.DisplayShifts(shifts);
    }

    private void GetAllShifts()
    {
        var shifts = ResponseManager.GetAllShifts().Result;
        if (shifts.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No shifts found![/]");
            MenuBuilder.EnterButtonPause();
            return;
        }
        VisualisationEngine.DisplayShifts(shifts);
    }

    private void AddShift()
    {
        ShiftCreate shift = inputHandler.CreateShift(Workers);
        ResponseManager.AddShift(shift).Wait();
    }

    private void DeleteShift()
    {
        int workerId = inputHandler.ChooseWorkerFromSelection(Workers).Id;
        var shifts = ResponseManager.GetShiftsByWorkerId(workerId).Result;
        if (shifts.Count() == 0)
        {
            AnsiConsole.MarkupLine("[red]Worker doesnt have shifts.[/]"); 
            MenuBuilder.EnterButtonPause();
            return;
        }
        int shiftId = inputHandler.ChooseShiftFromSelection(shifts).Id;
        ResponseManager.DeleteShift(shiftId).Wait();
    }

    private void UpdateShift()
    {
        int workerId = inputHandler.ChooseWorkerFromSelection(Workers).Id;
        var shifts = ResponseManager.GetShiftsByWorkerId(workerId).Result;
        if (shifts.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Worker doesnt have shifts.[/]"); 
            MenuBuilder.EnterButtonPause();
            return ;
        }
        var newShift = inputHandler.GetShiftUpdate(shifts);
        ResponseManager.UpdateShift(newShift).Wait();
    }
    public void HandleShifts()
    {
        Workers = ResponseManager.GetAllWorkers().Result;
        switch (MenuBuilder.DisplayShiftsWorkerOptions(false))
        {
            case Crud.Add:
                AddShift();
                break;
            case Crud.Update:
                UpdateShift();
                break;
            case Crud.Delete:
                DeleteShift();
                break;
            case Crud.ReadAll:
                GetAllShifts();
                break;
            case Crud.Read:
                GetShifts();
                break;
            case Crud.Return:
                break;
        }
    }
    
   
}