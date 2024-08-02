namespace ShiftsLogger.Controllers;

public class MainMenuController {

    private readonly WorkerController _workerController;
    private readonly ShiftController _shiftController;
    private readonly WorkerShiftController _workerShiftController;
    public static readonly List<string> Options = ["Manage workers", "Manage shifts", "Manage worker shifts"];
    private readonly Dictionary<string, Action> _optionHandlers;
    public MainMenuController(WorkerController workerController,
                            ShiftController shiftController,
                            WorkerShiftController workerShiftController)
    {
        _workerController = workerController;
        _shiftController = shiftController;
        _workerShiftController = workerShiftController;
        _optionHandlers = new Dictionary<string, Action>{
             { Options[0], ManageWorkersMenu },
             { Options[1], ManageShiftsMenu },
             { Options[2], ManageWorkerShiftsMenu }
        };
    }

    public void HandleChoice(string choice)
    {
        _optionHandlers.TryGetValue(choice, out var action);
        action!();
    }
    
    private void ManageWorkersMenu()
    {
        while (true) {
            string backToMainMenu = "Back to main menu";
            var option = UI.MenuSelection("[green]Manage workers[/] menu", [
                backToMainMenu,
                ..WorkerController.Options
            ]);

            if (option == backToMainMenu) {
                break;
            }

            _workerController.HandleChoice(option);
        }
    }

    private void ManageShiftsMenu()
    {
        while (true) {
            string backToMainMenu = "Back to main menu";
            var option = UI.MenuSelection("[green]Manage shift[/] menu", [
                backToMainMenu,
                ..ShiftController.Options
            ]);

            if (option == backToMainMenu) {
                break;
            }

            _shiftController.HandleChoice(option);
        }
    }

    private void ManageWorkerShiftsMenu()
    {
        while (true) {
            string backToMainMenu = "Back to main menu";
            var option = UI.MenuSelection("[green]Manage worker shifts[/] menu", [
                backToMainMenu,
                ..WorkerShiftController.Options
            ]);

            if (option == backToMainMenu) {
                break;
            }

            _workerShiftController.HandleChoice(option);
        }
    }
}