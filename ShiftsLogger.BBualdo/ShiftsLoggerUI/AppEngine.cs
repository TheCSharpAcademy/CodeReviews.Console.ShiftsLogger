using ShiftsLoggerUI.Helpers;
using ShiftsLoggerUI.Models;
using ShiftsLoggerUI.Services;
using Spectre.Console;

namespace ShiftsLoggerUI;

internal class AppEngine
{
  internal bool IsRunning { get; set; }
  private HttpClient Client { get; set; }

  public AppEngine()
  {
    IsRunning = true;
    Client = new();
    Client.DefaultRequestHeaders.Clear();
    Client.DefaultRequestHeaders.Add("Accept", "application/json");
  }

  internal async Task MainMenu()
  {
    ConsoleEngine.ShowTitle();

    string choice = ConsoleEngine.GetChoiceSelector("What you would like to do?", ["Show Shifts", "Create Shift", "Update Shift", "Delete Shift", "Quit"]);

    switch (choice)
    {
      case "Show Shifts":
        await ShowShifts();
        PressAnyKey();
        break;
      case "Create Shift":
        await CreateShift();
        PressAnyKey();
        break;
      case "Update Shift":
        await UpdateShift();
        PressAnyKey();
        break;
      case "Delete Shift":
        await DeleteShift();
        PressAnyKey();
        break;
      case "Quit":
        AnsiConsole.Clear();
        AnsiConsole.Markup("[cyan1]GOODBYE[/]");
        IsRunning = false;
        break;
    }
  }

  private async Task ShowShifts()
  {
    List<Shift>? shifts = await ShiftsService.GetShifts(Client);
    ConsoleEngine.ShowShiftsTable(shifts);
  }

  private async Task CreateShift()
  {
    string employeeName = UserInput.GetName();
    if (employeeName == "0") return;

    string startDateStr = UserInput.GetStartDate(employeeName);
    if (startDateStr == "0") return;

    string endDateStr = UserInput.GetEndDate(startDateStr, employeeName);
    if (endDateStr == "0") return;

    DateTime startDate = DateTimeParser.Parse(startDateStr);
    DateTime endDate = DateTimeParser.Parse(endDateStr);

    ShiftInsertRequest shift = new(employeeName, startDate, endDate);

    await ShiftsService.CreateShift(Client, shift);
  }

  private async Task UpdateShift()
  {
    List<Shift>? shifts = await ShiftsService.GetShifts(Client);

    bool rowsPresent = ConsoleEngine.ShowShiftsTable(shifts);

    if (!rowsPresent || shifts == null) return;

    int id = UserInput.GetShiftId(shifts, "update");
    if (id == 0) return;

    Shift shift = shifts.First(shift => shift.ShiftId == id);
    ShiftUpdateRequest updatedShift = new(shift.ShiftId, shift.EmployeeName, shift.StartDate, shift.EndDate);

    string name = UserInput.GetName(shift.EmployeeName);
    if (name == "0") return;
    updatedShift.EmployeeName = name;

    string startDate = UserInput.GetStartDate(name);
    if (startDate == "0") return;
    updatedShift.StartDate = DateTimeParser.Parse(startDate);

    string endDate = UserInput.GetEndDate(startDate, name);
    if (endDate == "0") return;
    updatedShift.EndDate = DateTimeParser.Parse(endDate);

    await ShiftsService.UpdateShift(Client, id, updatedShift);
  }

  private async Task DeleteShift()
  {
    List<Shift>? shifts = await ShiftsService.GetShifts(Client);

    bool rowsPresent = ConsoleEngine.ShowShiftsTable(shifts);

    if (!rowsPresent || shifts == null) return;

    int id = UserInput.GetShiftId(shifts, "delete");
    if (id == 0) return;

    await ShiftsService.DeleteShift(Client, id);
  }

  private void PressAnyKey()
  {
    AnsiConsole.Markup("\n\n[cyan1]Press any key to continue.[/]\n");
    Console.ReadKey();
  }
}