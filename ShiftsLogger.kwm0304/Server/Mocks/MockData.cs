using System.Collections.Concurrent;
using Server.Models.Dtos;
using Server.Services.Interfaces;
using Shared;
using Shared.Enums;
using Spectre.Console;

namespace Server.Mocks;

public class MockData
{
  private readonly IEmployeeService _service;
  private readonly IShiftService _shiftService;
  private readonly IEmployeeShiftService _empShiftService;
  private static readonly Random _random = new();
  public MockData(IEmployeeService service, IShiftService shiftService, IEmployeeShiftService empShiftService)
  {
    _service = service;
    _shiftService = shiftService;
    _empShiftService = empShiftService;
  }
  public async Task CreateMockEmployees()
  {
    for (int i = 0; i <= 30; i++)
    {
      string name = $"Employee Name {i}";
      ShiftClassification shift = GetRandomShift();
      double payRate = _random.NextDouble() * (50.0 - 15.0) + 15.0;
      Employee newEmployee = new()
      {
        Name = name,
        ShiftAssignment = shift,
        PayRate = Math.Round(payRate, 2)
      };
      await _service.AddAsync(newEmployee);
      AnsiConsole.WriteLine($"Adding {newEmployee.Name} | Shift: {newEmployee.ShiftAssignment} | Pay: ${newEmployee.PayRate}");
    }
    AnsiConsole.WriteLine("Done creating mock employees");
  }

  public async Task<bool> CheckIfEmployeesExist()
  {
    Employee employee = await _service.GetByIdAsync(1);
    return employee != null;
  }
  public async Task<bool> CheckIfEmployeeShiftsExist()
  {
    List<EmployeeShift> emptShift = await _empShiftService.GetEmployeesForShiftAsync(1);
    return emptShift != null;
  }

  public async Task<bool> CheckIfShiftsExist()
  {
    Shift shift = await _shiftService.GetByIdAsync(1);
    return shift != null;
  }

  public async Task CreateMockShifts()
  {
    DateTime startDate = DateTime.Today.AddYears(-1);
    for (int i = 0; i <= 90; i++)
    {
      ShiftClassification classification = i < 201 ? ShiftClassification.First : i > 201 && i < 401 ? ShiftClassification.Second : ShiftClassification.Third;
      Shift newShift = new()
      {
        Classification = classification,
        StartTime = startDate,
        EndTime = GetEndTime(startDate, classification)
      };

      await _shiftService.AddAsync(newShift);
      AnsiConsole.WriteLine($"Adding {newShift.ShiftId} | Classification: {newShift.Classification} | Start Time: {newShift.StartTime} EndTime: {newShift.EndTime}");
      startDate = startDate.AddDays(1);
    }
    AnsiConsole.WriteLine("Done creating mock shifts");
  }

  private DateTime GetEndTime(DateTime startDate, ShiftClassification classification)
  {
    DateTime today = startDate.Date;
    DateTime tomorrow = today.AddDays(1);
    return classification switch
    {
      ShiftClassification.First => today.AddHours(17),
      ShiftClassification.Second => tomorrow.AddHours(1),
      ShiftClassification.Third => tomorrow.AddHours(8),
      _ => throw new ArgumentException("Invalid shift classification", nameof(classification))
    };
  }

  public async Task DeleteAllShifts()
  {
    List<Shift> allShifts = await _shiftService.GetAllAsync();
    foreach (var shift in allShifts)
    {
      await _shiftService.DeleteAsync(shift.ShiftId);
      AnsiConsole.MarkupLine("[bold yellow]Shift Deleted[/]");
    }
  }

  private static ShiftClassification GetRandomShift()
  {
    List<ShiftClassification> classifications = [ShiftClassification.First, ShiftClassification.Second, ShiftClassification.Third];
    Random random = new();
    int index = random.Next(classifications.Count);
    return classifications[index];
  }

  public async Task AssignMockEmployeeShifts()
  {
    var shiftClassifications = Enum.GetValues(typeof(ShiftClassification)).Cast<ShiftClassification>();
    var assignmentTasks = new List<Task>();

    foreach (var classification in shiftClassifications)
    {
      assignmentTasks.Add(AssignShiftsForClassification(classification));
    }

    await Task.WhenAll(assignmentTasks);
  }

  private async Task AssignShiftsForClassification(ShiftClassification classification)
  {
    var employees = await _service.GetEmployeesByShift(classification);
    var shifts = await _shiftService.GetShiftsByClassification(classification);

    var assignmentTasks = new ConcurrentBag<Task>();

    foreach (var shift in shifts)
    {
      foreach (var employee in employees)
      {
        var dto = new EmployeeShiftDto
        {
          ShiftId = shift.ShiftId,
          EmployeeId = employee.EmployeeId,
          ClockInTime = GetRandomTime(shift.StartTime),
          ClockOutTime = GetRandomTime(shift.EndTime)
        };

        assignmentTasks.Add(_empShiftService.CreateEmployeeShiftAsync(dto));
      }

    }
    await Task.WhenAll(assignmentTasks);
    AnsiConsole.WriteLine($"Assigned {employees.Count} employees to {shifts.Count} {classification} shifts");
  }

  private static DateTime GetRandomTime(DateTime shiftTime)
  {
    int minutesOffset = _random.Next(-60, 61);
    return shiftTime.AddMinutes(minutesOffset);
  }
}