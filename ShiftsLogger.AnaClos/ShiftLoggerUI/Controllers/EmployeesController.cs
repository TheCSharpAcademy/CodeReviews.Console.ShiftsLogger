using ShiftLoggerUI.Dtos;
using ShiftLoggerUI.Services;
using ShiftsLoggerWebAPI.DTOs;

namespace ShiftLoggerUI.Controllers;

public class EmployeesController : IController
{
    ConsoleController _consoleController;
    EmployeesService _employeesService;
    public EmployeesController(ConsoleController consoleController, EmployeesService employeesService)
    {
        _consoleController = consoleController;
        _employeesService = employeesService;
    }

    public void Add()
    {
        string name = _consoleController.GetString("Employee's name");

        EmployeeDto employeeDto = new EmployeeDto
        {
            Id = 0,
            Name = name
        };

        var response = _employeesService.AddEmployee(employeeDto);
        _consoleController.MessageAndPressKey(response, "orange1");
    }

    public void Delete()
    {
        EmployeeDto employee = GetEmployeeFromMenu("Select a employee to delete");
        if (employee == null)
        {
            return;
        }

        var response = _employeesService.RemoveEmployee(employee.Id);
        _consoleController.MessageAndPressKey(response, "orange1");
    }

    public void Update()
    {
        EmployeeDto employee = GetEmployeeFromMenu("Select an employee to update");
        if (employee == null)
        {
            return;
        }
        string newName = _consoleController.GetString("New employee's name");
        employee.Name = newName;

        var response = _employeesService.UpdateEmployee(employee);
        _consoleController.MessageAndPressKey(response, "orange1");
    }

    public void View()
    {
        EmployeeDto employee = GetEmployeeFromMenu("Select an employee to view details");
        if (employee == null)
        {
            return;
        }

        string[] columns = { "Property", "Value" };

        var recordEmployees = EmployeeToProperties(employee);

        _consoleController.ShowTable("Employee", columns, recordEmployees);
        _consoleController.PressKey("Press a key to continue.");
    }

    public void ViewAll()
    {
        string[] columns = { "Id", "Name" };
        var employees = _employeesService.GetEmployees();
        if (employees.Count == 0)
        {
            _consoleController.MessageAndPressKey("There is no Employee to view.", "Red");
            return;
        }
        var recordEmployee = EmployeeToRecord(employees);
        _consoleController.ShowTable("Employees", columns, recordEmployee);
        _consoleController.PressKey("Press a key to continue.");
    }

    public List<RecordDto> EmployeeToRecord(List<EmployeeDto> employees)
    {
        var tableRecord = new List<RecordDto>();
        foreach (var employee in employees)
        {
            var record = new RecordDto { Column1 = employee.Id.ToString(), Column2 = employee.Name };
            tableRecord.Add(record);
        }
        return tableRecord;
    }

    public List<RecordDto> EmployeeToProperties(EmployeeDto employee)
    {
        var tableRecord = new List<RecordDto>();
        foreach (var property in employee.GetType().GetProperties())
        {
            if (property.GetValue(employee) != null)
            {
                var record = new RecordDto { Column1 = property.Name, Column2 = property.GetValue(employee).ToString() };
                tableRecord.Add(record);
            }
        }
        return tableRecord;
    }

    public List<string> EmployeesToString(List<EmployeeDto> employees)
    {
        var tableRecord = new List<string>();

        foreach (var employee in employees)
        {
            tableRecord.Add(employee.Name);
        }
        return tableRecord;
    }

    public EmployeeDto GetEmployeeFromMenu(string title)
    {
        var employees = _employeesService.GetEmployees();
        if (employees.Count == 0)
        {
            _consoleController.MessageAndPressKey("There is no Employee to select.", "Red");
            return null;
        }

        List<string> stringEmployees = EmployeesToString(employees);
        stringEmployees.Add("Exit Menu");

        string name = _consoleController.Menu(title, "blue", stringEmployees);
        if (name == "Exit Menu")
        {
            return null;
        }

        var employee = employees.FirstOrDefault(x => x.Name == name);
        return employee;
    }
}