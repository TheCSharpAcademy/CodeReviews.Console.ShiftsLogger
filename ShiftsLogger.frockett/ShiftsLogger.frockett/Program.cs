// See https://aka.ms/new-console-template for more information

using ShiftsLogger.frockett.UI;
using ShiftsLogger.frockett.UI.Helpers;
using ShiftsLogger.frockett.UI.UI;
using System.Reflection.Metadata;


HttpClient client = new HttpClient()
{
    BaseAddress = new Uri("https://localhost:7127/api/")
};

InputValidation inputValidation = new InputValidation();
UserInput userInput = new UserInput(inputValidation);
TableEngine tableEngine = new TableEngine();
ApiService apiService = new ApiService(client);
Menu menu = new Menu(apiService, tableEngine, userInput);
await menu.MainMenuHandler();

