using ShiftsLoggerCLI.Enums;

namespace ShiftsLoggerCLI.Handlers;

public class ShiftsHandler(InputHandler inputHandler)
{
    private void GetShifts()
    {
        int workerId = inputHandler.ChooseWorkerFromSelection(ResponseManager.GetAllWorkers().Result).Id;
        var shifts = ResponseManager.GetShifts(workerId).Result;
        VisualisationEngine.DisplayShifts(shifts);
    }

    private void GetAllShifts()
    {
        var shifts = ResponseManager.GetAllShifts().Result;
        VisualisationEngine.DisplayShifts(shifts);
    }

    private void AddShift()
    {
        
    }

    private void DeleteShift()
    {
        
    }

    private void UpdateShift()
    {
        
    }
    public void HandleShifts()
    {
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
        }
    }
    
   
}