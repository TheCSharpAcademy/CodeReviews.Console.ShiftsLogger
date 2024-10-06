using ShiftLoggerUI;
using ShiftsLoggerLibrary;
using Spectre.Console;

namespace ShiftsLoggerUI;

public class EmployeeService
{
    public static async void CreateEmployee()
    {
        MenuPresentation.PresentMenu("[blue]Inserting[/]");
        bool isCancelled;
        string name;

        UniquePropertyValidator<string, Employee> uniqueEmployee = new()
        {
            ErrorMsg = "Name must be unique.",
            GetModel = GetEmployeeByName
        };

        (isCancelled, name) = AskForEmployeeName(uniqueEmployee);
        if (isCancelled) return;

        var employee = await EmployeeController.InsertEmployeeAsync(new Employee { Name = name });

        if(employee == null)
        {
            AnsiConsole.MarkupLine("[red]Error creating the Employee[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[green]Employee created successfully[/]");
        }

        Prompter.PressKeyToContinuePrompt();
    }

    public static void UpdateEmployee()
    {
        MenuPresentation.PresentMenu("[yellow]Updating[/]");
        bool isCancelled;
        string oldId, oldName, newName;

        if (!ShowEmployeeTable()) return;

        ExistingModelValidator<string, Employee> existingEmployee = new()
        {
            ErrorMsg = "Employee Id doesn't exist.",
            GetModel = GetEmployeeById
        };

        (isCancelled, oldId) = AskForEmployeeId(existingEmployee);
        if (isCancelled) return;

        oldName = GetEmployeeById(oldId).Name;

        AnsiConsole.WriteLine("New Name");
        UniquePropertyValidator<string, Employee> uniqueEmployee = new()
        {
            ErrorMsg = "Name must be unique.",
            GetModel = GetEmployeeByName,
            PropertyName = "Name",
            ExcludedValues = [(object)oldName]
        };
        (isCancelled, newName) = AskForEmployeeName(uniqueEmployee);
        if (isCancelled) return;

        if(EmployeeController.UpdateEmployeeAsync(new Employee { EmployeeId = GetEmployeeByName(oldName).EmployeeId, Name = newName }).Result)
        {
            AnsiConsole.MarkupLine("[green]Employee updated successfully[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Error updating the Employee[/]");
        }
        Prompter.PressKeyToContinuePrompt();
    }

    public static void DeleteEmployee()
    {
        MenuPresentation.PresentMenu("[red]Deleting[/]");
        bool isCancelled;
        string id;

        if (!ShowEmployeeTable()) return;

        ExistingModelValidator<string, Employee> existingEmployee = new()
        {
            ErrorMsg = "Employee Id doesn't exist.",
            GetModel = GetEmployeeById
        };

        (isCancelled, id) = AskForEmployeeId(existingEmployee);
        if (isCancelled) return;

        if (EmployeeController.DeleteEmployeeByIdAsync(int.Parse(id)).Result)
        {
            AnsiConsole.MarkupLine("[green]Employee deleted successfully[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Error deleting the Employee[/]");
        }
        Prompter.PressKeyToContinuePrompt();
    }

    public static void ShowEmployees()
    {
        if (!ShowEmployeeTable()) return;
        Prompter.PressKeyToContinuePrompt();
    }

    public static (bool IsCancelled, string Result) AskForEmployeeName(params IValidator[] validators)
    {
        string message = "Enter a Employee Name";
        return Prompter.PromptWithValidation(message, validations: validators);
    }

    public static (bool IsCancelled, string Result) AskForEmployeeId(params IValidator[] validators)
    {
        string message = "Enter a Employee Id";
        return Prompter.PromptWithValidation(message, validations: validators);
    }

    private static Employee GetEmployeeByName(string input)
    {
        var employees = EmployeeController.GetEmployeesAsync().Result;
        return employees.Where(e => e.Name == input).FirstOrDefault();
    }

    public static Employee GetEmployeeById(string input)
    {
        var employees = EmployeeController.GetEmployeesAsync().Result;
        return employees.Where(e => e.EmployeeId.ToString() == input).FirstOrDefault();
    }

    public static bool ShowEmployeeTable()
    {
        List<Employee> employees = EmployeeController.GetEmployeesAsync().Result;
        if (employees == null)
        {
            Prompter.PressKeyToContinuePrompt();
            return false;
        }

        List<EmployeeDto> employeesDto = employees.Select(e => EmployeeMapper.MapToDto(e)).ToList();
        OutputRenderer.ShowTable(employeesDto, "Employees");
        return true;
    }

}