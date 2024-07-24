using WorkerShiftsUI.Services;
using WorkerShiftsUI.Views;

var apiService = new ApiService();
IWorkerService workerService = new WorkerService(apiService);
IWorkersView workerView = new WorkersView(workerService);
var mainMenu = new MainMenu(workerView);

await mainMenu.ShowMainMenu();