using ShiftsLogger.Services;

namespace ShiftsLogger.Controllers;

public class ShiftController {
    private readonly ApiService _service;
    public static readonly List<string> Options = ["View shifts", "Add shift", "Edit shift", "Delete shift"];
    private readonly Dictionary<string, Action> _optionHandlers;


    public ShiftController(ApiService service)
    {
        _service = service; 
        _optionHandlers = new Dictionary<string, Action>{
            { Options[0], ViewShifts },
            { Options[1], AddShift },
            { Options[2], EditShift },
            { Options[3], DeleteShift }
        };
    }

    public void HandleChoice(string choice) {
        _optionHandlers.TryGetValue(choice, out var action);
        action!();
    }

   private void ViewShifts()
   {} 

   private void AddShift()
   {}

   private void EditShift()
   {}

   private void DeleteShift()
   {}
}