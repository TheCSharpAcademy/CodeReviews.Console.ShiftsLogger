using Client.Services;
using Client.Views;
using Spectre.Console;

namespace Client;

public class AppSession
{
  private readonly EmployeeHandler _employeeHandler;
  private readonly ShiftHandler _shiftHandler;
  public AppSession(EmployeeHandler employeeHandler, ShiftHandler shiftHandler)
  {

    _employeeHandler = employeeHandler;
    _shiftHandler = shiftHandler;
  }
  public async Task OnStart()
  {
    bool running = true;
    while (running)
    {
      Console.Clear();
      AnsiConsole.WriteLine(@"
 _______  __   __  ___   _______  _______    ___      _______  _______  _______  _______  ______   
|       ||  | |  ||   | |       ||       |  |   |    |       ||       ||       ||       ||    _ |  
|  _____||  |_|  ||   | |    ___||_     _|  |   |    |   _   ||    ___||    ___||    ___||   | ||  
| |_____ |       ||   | |   |___   |   |    |   |    |  | |  ||   | __ |   | __ |   |___ |   |_||_ 
|_____  ||       ||   | |    ___|  |   |    |   |___ |  |_|  ||   ||  ||   ||  ||    ___||    __  |
 _____| ||   _   ||   | |   |      |   |    |       ||       ||   |_| ||   |_| ||   |___ |   |  | |
|_______||__| |__||___| |___|      |___|    |_______||_______||_______||_______||_______||___|  |_|
");
      string choice = SelectionMenus.MainMenu();
      running = await HandleMainMenuChoice(choice);
    }
  }

  private async Task<bool> HandleMainMenuChoice(string choice)
  {
      switch (choice)
      {
        case "Employees":
          await _employeeHandler.HandleEmployeeChoice();
          return true;
        case "Shifts":
          await _shiftHandler.HandleShiftChoice();
          return true;
        case "Exit":
          AnsiConsole.WriteLine("Goodbye!");
          Environment.Exit(0);
          return false;
        default:
          return true;
    }
  }
}