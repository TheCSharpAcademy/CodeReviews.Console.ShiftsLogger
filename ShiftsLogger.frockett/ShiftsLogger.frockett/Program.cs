// See https://aka.ms/new-console-template for more information

using ShiftsLogger.frockett.UI;
using ShiftsLogger.frockett.UI.UI;


HttpClient client = new HttpClient()
{
    BaseAddress = new Uri("https://localhost:7127/api/")
};

UserInput userInput = new UserInput();
TableEngine tableEngine = new TableEngine();
ApiService apiService = new ApiService(client);
Menu menu = new Menu(apiService, tableEngine, userInput);
await menu.MainMenuHandler();

