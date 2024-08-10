using Client.Api;
using Client.Utils;
using Client.Views;
using Server.Models.Dtos;
using Shared;
using Shared.Enums;
using Spectre.Console;

namespace Client.Services;

public class ShiftHandler(ShiftApi api, EmployeeShiftApi employeeShiftApi, EmployeeApi employeeApi)
{
  private readonly ShiftApi _api = api;
  private readonly EmployeeShiftApi _employeeShiftApi = employeeShiftApi;
  private readonly EmployeeApi _employeeApi = employeeApi;

  public async Task HandleShiftChoice()
  {
    bool running = true;

    while (running)
    {
      string choice = SelectionMenus.ShiftMenu();
      switch (choice)
      {
        case "Add shift":
          await HandleAddShift();
          break;
        case "View shifts":
          await HandleViewShifts();
          break;
        case "Back":
          running = false;
          break;
        default:
          running = false;
          break;
      }
    }
  }

  private async Task HandleAddShift()
  {
    ShiftClassification classification = SelectionMenus.SelectShiftClassification();
    Shift newShift = new()
    {
      Classification = classification
    };
    if (AnsiConsole.Confirm("Do you want to add this shift?"))
    {
      try
      {
        await _api.CreateAsync(newShift);
        await AssignEmployeesToCreatedShift(classification);
        Console.WriteLine("Shift added and employees assigned successfully.");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error adding shift: {ex.Message}");
      }
    }
    else
    {
      Console.WriteLine("Shift addition cancelled.");
    }
  }

  private async Task AssignEmployeesToCreatedShift(ShiftClassification classification)
  {
    try
    {
      Shift? shift = await _api.GetNewestShift();
      if (shift != null && shift.Classification == classification)
      {
        List<Employee> employees = await _employeeApi.GetEmployeeByShiftClassification(classification);
        if (employees != null)
        {
          foreach (var employee in employees)
          {
            EmployeeShiftDto empShift = new()
            {
              EmployeeId = employee.EmployeeId,
              ShiftId = shift.ShiftId,
              ClockInTime = SharedUtils.GetRandomTime(shift.StartTime),
              ClockOutTime = SharedUtils.GetRandomTime(shift.EndTime)
            };
            await _employeeShiftApi.CreateEmployeeShift(empShift);
            AnsiConsole.WriteLine($"Assigned {employee.Name} to Shift: {shift.ShiftId}");
          }
          AnsiConsole.WriteLine("Employee shifts assigned and created");
        }
        else
        {
          AnsiConsole.WriteLine("No employees are assigned this shift classification");
        }
      }
      else
      {
        AnsiConsole.WriteLine("Shift was not found or was incorrect");
      }
    }
    catch (Exception e)
    {
      AnsiConsole.WriteException(e);
    }
  }


  private async Task HandleViewShifts()
  {
    List<Shift> shifts = await _api.GetAllAsync();
    Shift shift = SelectionMenus.SelectShift(shifts);
    int id = shift.ShiftId;
    string choice = SelectionMenus.ShiftOptionsMenu();
    switch (choice)
    {
      case "View all employees on shift":
        await HandleViewEmployeeShifts(id);
        break;
      case "View late employees on shift":
        await HandleViewLate(id);
        break;
      case "Edit shift attributes":
        string editOption = SelectionMenus.ShiftEditSelection();
        await HandleEditShift(editOption, shift);
        break;
      case "Delete shift":
        await HandleDeleteShift(id);
        break;
      case "Back":
        return;
      default:
        return;
    }
  }

  private async Task HandleDeleteShift(int id)
  {
    bool confirm = AnsiConsole.Confirm($"Are you sure you want to delete this shift?");
    if (confirm)
    {
      await _api.DeleteByIdAsync(id);
      return;
    }
  }

  private async Task HandleEditShift(string option, Shift shift)
  {
    bool running = true;
    while (running)
    {
      Tables.ShowShiftDetails(shift);
      switch (option)
      {
        case "Start Time":
          int startChange = ChangeTime("start");
          shift.StartTime = shift.StartTime.AddHours(startChange);
          break;
        case "End Time":
          int endChange = ChangeTime("end");
          shift.EndTime = shift.EndTime.AddHours(endChange);
          break;
        case "Back":
          running = false;
          return;
        default:
          running = false;
          return;
      }
      int id = shift.ShiftId;
      await _api.UpdateAsync(id, shift);
      return;
    }
  }

  private static int ChangeTime(string input)
  {
    string response = SelectionMenus.SelectTimeChange(input);
    AnsiConsole.MarkupLine("[bold red]Max 4 hours[/]");
    int change = AnsiConsole.Prompt(
        new TextPrompt<int>($"How many hours do you want to {response.ToLower()} the {input} time by?")
            .ValidationErrorMessage("[bold red]Please enter a valid number between 1 and 4.[/]")
            .Validate(hours =>
            {
              return hours >= 1 && hours <= 4
                  ? ValidationResult.Success()
                  : ValidationResult.Error("[bold red]Max 4 hours[/]");
            })
    );
    return response == "Sooner" ? change : -change;
  }

  private async Task HandleViewLate(int id)
  {
    List<EmployeeShift> late = await _employeeShiftApi.GetLateEmployees(id);
    if (late != null && late.Count > 0)
    {
      Tables.ShowEmployeesForShift($"All late employees for shift {id}", late);
    }
    else
    {
      AnsiConsole.WriteLine("No late employees found for this shift.");
    }
  }

  private async Task HandleViewEmployeeShifts(int shiftId)
  {
    List<EmployeeShift> employees = await _employeeShiftApi.GetAllEmployeesOnShift(shiftId);
    Tables.ShowEmployeesForShift($"All employees for shift {shiftId}", employees);
  }
}