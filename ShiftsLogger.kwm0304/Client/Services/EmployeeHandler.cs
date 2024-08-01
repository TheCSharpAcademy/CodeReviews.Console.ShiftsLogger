using Client.Api;
using Client.Utils;
using Client.Views;
using Shared;
using Shared.Enums;
using Spectre.Console;

namespace Client.Services;

public class EmployeeHandler
{
  private readonly EmployeeApi _api;
  private readonly EmployeeShiftApi _employeeShiftApi;
  private readonly HttpClient _http;
  public EmployeeHandler(HttpClient http)
  {
    _http = http;
    _api = new(_http);
    _employeeShiftApi = new(_http);
  }
  public async Task HandleEmployeeChoice()
  {
    bool running = true;
    while (running)
    {
      string choice = SelectionMenus.EmployeeMenu();
      switch (choice)
      {
        case "Add employee":
          await HandleAddEmployee();
          break;
        case "View employees":
          await HandleViewEmployees();
          break;
        case "Back":
          return;
        default:
          return;
      }
    }
  }

  private async Task HandleAddEmployee()
  {
    string name = StringPrompt.GetAndConfirmResponse<string>("What is the employees name?");
    double pay = StringPrompt.GetAndConfirmResponse<double>("What do they get paid per hour?");

    ShiftClassification shift = SelectionMenus.SelectShiftClassification();
    Employee employee = new()
    {
      Name = name,
      PayRate = pay,
      ShiftAssignment = shift
    };
    await _api.CreateAsync(employee);
  }

  private async Task HandleViewEmployees()
  {
    List<Employee> employees = await _api.GetAllAsync();
    Employee employee = SelectionMenus.SelectEmployee(employees);
    string choice = SelectionMenus.EmployeeOptionsMenu();
    await HandleEmployeeAction(employee, choice);
  }

  private async Task HandleEmployeeAction(Employee employee, string choice)
  {

    switch (choice)
    {
      case "View all shifts for employee":
      int id = employee.EmployeeId;
        List<EmployeeShift> empShifts = await _employeeShiftApi.GetEmployeeShiftByIds(id);
        Tables.ShowEmployeesForShift($"Shifts for {employee.Name}", empShifts);
        break;
      case "View employees pay":
        string payOption = SelectionMenus.SelectEmployeePayRange();
        await HandleViewPay(payOption, employee);
        break;
      case "Edit employee attributes":
        string editOption = SelectionMenus.EmployeeEditSelection();
        await HandleEdit(editOption, employee);
        break;
      case "Delete employee":
        await HandleDelete(employee);
        break;
      case "Back":
        return;
      default:
        return;
    }
  }

  private async Task HandleDelete(Employee employee)
  {
    bool confirm = AnsiConsole.Confirm($"Are you sure you want to delete {employee.Name}?");
    if (confirm)
    {
      await _api.DeleteByIdAsync(employee.EmployeeId);
      return;
    }
  }

  private async Task HandleEdit(string editOption, Employee employee)
  {
    Tables.ShowEmployeeDetails(employee);
    switch (editOption)
    {
      case "Name":
        string name = StringPrompt.GetAndConfirmResponse<string>("What would you like to change their name to?");
        employee.Name = name;
        break;
      case "Shift":
        ShiftClassification classification = SelectionMenus.SelectShiftClassification();
        employee.ShiftAssignment = classification;
        break;
      case "Pay":
        double pay = StringPrompt.GetAndConfirmResponse<double>("What would you like to change the pay rate to?");
        employee.PayRate = pay;
        break;
      case "Back":
        return;
      default:
        return;
    }
    int id = employee.EmployeeId;
    await _api.UpdateAsync(id, employee);
    return;
  }

  private async Task HandleViewPay(string payOption, Employee employee)
  {
    double pay = await _api.GetEmployeePayForRange(payOption);
    AnsiConsole.MarkupLine($"{employee.Name} pay for {payOption}: ${pay}");
    AnsiConsole.MarkupLine("[bold blue]Press any key to exit..[/]");
    Console.ReadKey(true);
  }
}