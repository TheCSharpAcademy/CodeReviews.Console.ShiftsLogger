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
    List<ShiftClassification> shifts = [ShiftClassification.First, ShiftClassification.Second, ShiftClassification.Third];
    ShiftClassification shift = SelectionMenus.SelectShift(shifts);
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
        List<EmployeeShift> empShifts = await _employeeShiftApi.GetEmployeeShiftByIds();
        ShowEmployeeShiftsTable(empShifts, employee);
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
        break;
      case "Back":
        return;
      default:
        return;
    }

  }

    private async Task HandleEdit(string editOption, Employee employee)
    {
      switch (editOption)
      {
        case "Name":
        string name = StringPrompt.GetAndConfirmResponse<string>("What would you like to change their name to?");
        employee.Name = name;
        break;
      }
    }

    private async Task HandleViewPay(string payOption, Employee employee)
  {
    double pay = await _api.GetEmployeePayForRange(payOption);
    AnsiConsole.MarkupLine($"{employee.Name} pay for {payOption}: ${pay}");
    AnsiConsole.MarkupLine("[bold blue]Press any key to exit..[/]");
    Console.ReadKey(true);
  }

  private void ShowEmployeeShiftsTable(List<EmployeeShift> empShifts, Employee employee)
  {
    BaseTable<EmployeeShift> table = new($"Shifts for {employee.Name}", empShifts);
    table.Show();
    AnsiConsole.MarkupLine("[bold blue]Press any key to exit..[/]");
    Console.ReadKey(true);
  }
}