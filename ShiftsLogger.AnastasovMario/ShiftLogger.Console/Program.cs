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
        MenuOptions.GetAllShifts,
        MenuOptions.Quit));

  switch (option)
  {
    case MenuOptions.AddShift:
      await apiClient.AddShiftAsync();
      break;
    case MenuOptions.DeleteShift:
      await apiClient.DeleteShiftAsync();
      break;
    case MenuOptions.UpdateShift:
      await apiClient.UpdateShiftAsync();
      break;
    case MenuOptions.GetAllShifts:
      await apiClient.GetAllShiftsAsync();
      break;
    case MenuOptions.Quit:
      isAppRunning = false;
      Console.WriteLine("\nGoodbye\n");
      Environment.Exit(0);
      break;
    default:
      break;
  }
}