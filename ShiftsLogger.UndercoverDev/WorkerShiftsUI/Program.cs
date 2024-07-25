using WorkerShiftsUI.Services;
using WorkerShiftsUI.Views;

var apiService = new ApiService();
IWorkerService workerService = new WorkerService(apiService);
IWorkersView workerView = new WorkersView(workerService);
IShiftService shiftService = new ShiftService(apiService);
IShiftView shiftView = new ShiftsView(shiftService);
var mainMenu = new MainMenu(workerView, shiftView);

Console.Clear();
await mainMenu.ShowMainMenu();