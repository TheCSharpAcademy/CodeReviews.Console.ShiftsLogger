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

    string choice = SelectionMenus.MainMenu();
    await HandleMainMenuChoice(choice);

  }

  private async Task HandleMainMenuChoice(string choice)
  {
    while (true)
    {
      switch (choice)
      {
        case "View employees":
          await _employeeHandler.HandleEmployeeChoice();
          break;
        case "View shifts":
          await _shiftHandler.HandleShiftChoice();
          break;
        case "Exit":
          AnsiConsole.WriteLine("Goodbye!");
          Environment.Exit(0);
          break;
        default:
          break;
      }
    }
  }
}