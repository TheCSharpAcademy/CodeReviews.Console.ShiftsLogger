using Client.Services;
using Client.Views;
using Spectre.Console;

namespace Client;

public class AppSession
{
  private readonly HttpClient _http;
  private readonly EmployeeHandler _employeeHandler;
  private readonly ShiftHandler _shiftHandler;
  public AppSession(HttpClient http)
  {
    _http = http;
    _employeeHandler = new(_http);
    _shiftHandler = new(_http);
  }
  public async Task OnStart()
  {
    string choice = SelectionMenus.MainMenu();
    await HandleMainMenuChoice(choice);
  }

  private async Task HandleMainMenuChoice(string choice)
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