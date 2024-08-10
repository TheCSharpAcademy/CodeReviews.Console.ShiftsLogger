using Client.Api;
using Client.Utils;
using Client.Views;
using Shared;
using Shared.Enums;
using Spectre.Console;

namespace Client.Services
{
  public class EmployeeHandler(EmployeeApi api, EmployeeShiftApi employeeShiftApi)
  {
    private readonly EmployeeApi _api = api;
    private readonly EmployeeShiftApi _employeeShiftApi = employeeShiftApi;

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
            running = false;
            break;
          default:
            break;
        }
      }
    }

    private async Task HandleAddEmployee()
    {
      string name = StringPrompt.GetAndConfirmResponse<string>("What is the employee's name?");
      double pay = StringPrompt.GetAndConfirmResponse<double>("What do they get paid per hour?");
      ShiftClassification shift = SelectionMenus.SelectShiftClassification();
      Employee employee = new() { Name = name, PayRate = pay, ShiftAssignment = shift };
      try
      {
        await _api.CreateAsync(employee);
      }
      catch (Exception e)
      {
        AnsiConsole.WriteLine(e.Message);
        return;
      }
    }

    private async Task HandleViewEmployees()
    {
      try
      {
        List<Employee> employees = await _api.GetAllAsync();
        Employee employee = SelectionMenus.SelectEmployee(employees);
        bool running = true;
        while (running)
        {
          string choice = SelectionMenus.EmployeeOptionsMenu();
          running = await HandleEmployeeAction(employee, choice);
        }
      }
      catch (Exception e)
      {
        AnsiConsole.WriteLine(e.Message);
        return;
      }
    }

    private async Task<bool> HandleEmployeeAction(Employee employee, string choice)
    {
      try
      {
        switch (choice)
        {
          case "View all shifts for employee":
            List<EmployeeShift> empShifts = await _employeeShiftApi.GetEmployeeShiftByIds(employee.EmployeeId);
            Tables.EmployeeShiftsTable($"Shifts for {employee.Name}", employee.Name!, empShifts);
            return true;
          case "View employees pay":
            await HandleViewPay(employee);
            return true;
          case "Edit employee attributes":
            string editOption = SelectionMenus.EmployeeEditSelection();
            await HandleEdit(editOption, employee);
            return true;
          case "Delete employee":
            await HandleDelete(employee);
            return true;
          case "Back":
            return false;
          default:
            return true;
        }
      }
      catch (Exception e)
      {
        AnsiConsole.WriteLine(e.Message);
        return false;
      }
    }

    private async Task HandleDelete(Employee employee)
    {
      bool confirm = AnsiConsole.Confirm($"Are you sure you want to delete {employee.Name}?");
      if (confirm)
      {
        try
        {
          await _api.DeleteByIdAsync(employee.EmployeeId);
        }
        catch (Exception e)
        {
          AnsiConsole.WriteLine(e.Message);
          return;
        }
      }
    }

    private async Task HandleEdit(string editOption, Employee employee)
    {
      try
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
        await _api.UpdateAsync(employee.EmployeeId, employee);
      }
      catch (Exception e)
      {
        AnsiConsole.WriteLine(e.Message);
        return;
      }
    }

    private async Task HandleViewPay(Employee employee)
    {
      try
      {
        var payRate = employee.PayRate;
        var shifts = await _employeeShiftApi.GetEmployeeShiftByIds(employee.EmployeeId);
        var firstWorked = shifts.Min(s => s.ClockInTime).Date;
        var lastWorked = shifts.Max(s => s.ClockOutTime).Date;
        var totalWorked = (lastWorked - firstWorked).TotalDays + 1;
        var totalWeeks = totalWorked / 7;
        var totalMonths = totalWorked / 30.44;
        var totalHours = shifts.Aggregate(0.0, (total, shift) => total + (shift.ClockOutTime - shift.ClockInTime).TotalHours);
        var totalPay = totalHours * payRate;
        var payPerWeek = totalPay / totalWeeks;
        var payPerMonth = totalPay / totalMonths;
        var payPerYear = payPerWeek * 52;
        AnsiConsole.MarkupLine($"{employee.Name}'s pay:\n Year: ${payPerYear:F2} \n Month: ${payPerMonth:F2} \n Week: ${payPerWeek:F2}");
        AnsiConsole.MarkupLine("[bold blue]Press any key to exit..[/]");
        Console.ReadKey(true);
      }
      catch (Exception e)
      {
        AnsiConsole.WriteLine(e.Message);
        return;
      }
    }
  }
}