using ConsoleUI.Services;
using ConsoleUI.Views;

HttpClient httpClient = new HttpClient();
WorkerShiftService workerShiftService = new WorkerShiftService(httpClient);
ConsoleView consoleView = new ConsoleView(workerShiftService);

await consoleView.DisplayMainMenu();