using ShiftLogger.Console;
using ShiftLoggerConsoleUI;
using Spectre.Console;


bool isAppRunning = true;
var apiClient = new ShiftApiClient("https://localhost:5050/");

while (isAppRunning)
{
  var option = AnsiConsole.Prompt(
    new SelectionPrompt<MenuOptions>()
    .Title("\nWhat would you like to do?")
    .AddChoices(
        MenuOptions.AddShift,
        MenuOptions.DeleteShift,
        MenuOptions.UpdateShift,
        MenuOptions.GetShift,
        MenuOptions.GetAllShifts,
        MenuOptions.Quit));

  switch (option)
  {
    case MenuOptions.GetShift:
      break;
    case MenuOptions.AddShift:
      break;
    case MenuOptions.DeleteShift:
      break;
    case MenuOptions.UpdateShift:
      break;
    case MenuOptions.GetAllShifts:
      await apiClient.GetAllShiftsAsync();
      break;
    case MenuOptions.Quit:
      break;
    default:
      break;
  }
}