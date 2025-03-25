using Spectre.Console;
using ShiftsLogger.Console.Models;

namespace ShiftsLogger.Console;

public class UserInterface
{
    private ApiClient _apiClient = new ApiClient();
    private UserInterfaceHelper _uiHelper = new UserInterfaceHelper();
    private Utilities _utilities = new Utilities();
    
    private readonly string[] _menuOptions = { "Shifts", "Employees" };
    private readonly string[] _shiftsMenuOptions = { "Add a new shift", "Update a shift", "View all shifts", "View shifts of a specific employee", "View a specific shift by ID", "Delete a shift", "Go to main menu" };
    private readonly string[] _employeesMenuOptions = { "Add a new employee", "Update employee's info", "View all employees", "View shifts of a specific employee", "View a specific employee by ID", "Delete an employee", "Go to main menu" };
    
    public void Run()
    {
        
        while (true)
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[slateblue1]Shifts Logger[/]");
            
            var menuChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("(Main) What do you want to do?")
                    .AddChoices(_menuOptions)
                    .HighlightStyle(Color.MediumPurple3)
            );

            switch (menuChoice)
            {
                case "Shifts":
                    var shiftMenuChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("(Main -> Shifts) What do you want to do?")
                            .AddChoices(_shiftsMenuOptions)
                            .HighlightStyle(Color.MediumPurple3)
                    );

                    switch (shiftMenuChoice)
                    {
                        case "Add a new shift":
                        {
                            var employee = _uiHelper.ChooseEmployee("Choose an employee");
                            break;
                        }
                        case "Update a shift":
                        {
                            break;
                        }
                        case "View all shifts":
                        {
                            _uiHelper.ShowShifts();
                            
                            System.Console.ReadKey();
                            break;
                        }
                        case "View shifts of a specific employee":
                        {
                            var employee = _uiHelper.ChooseEmployee("Choose an employee to view their shifts");
                            _uiHelper.ShowShifts(employee);
                            
                            System.Console.ReadKey();
                            break;
                        }
                        case "View a specific shift by ID":
                        {
                            break;
                        }
                        case "Delete a shift":
                        {
                            break;
                        }
                        case "Go to main menu":
                        {
                            continue;
                        }
                    }
                    
                    break;
                case "Employees":
                    var employeeMenuChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("(Main -> Employees) What do you want to do?")
                            .AddChoices(_employeesMenuOptions)
                            .HighlightStyle(Color.MediumPurple3)
                    );

                    switch (employeeMenuChoice)
                    {
                        case "Add a new employee":
                        {
                            string firstName = _uiHelper.AskUserForText("Enter new first name"); 
                            string lastName = _uiHelper.AskUserForText("Enter new last name"); 
                            
                            var employee = _utilities.CreateEmployeeEntity(firstName, lastName);
                            
                            _uiHelper.ShowEmployee("Confirm employee creation", employee);

                            if (_uiHelper.Confirmed("Do you want to proceed?"))
                            {
                                var result = _apiClient.CreateEmployee(employee);
                                
                                if (result != null)
                                {
                                    _uiHelper.ShowEmployee("Result", result);
                                    AnsiConsole.MarkupLine("Successfully created the employee!");
                                }
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("Employee creation cancelled.");
                            }
                            
                            System.Console.ReadKey();
                            break;
                        }
                        case "Update employee's info":
                        {
                            var oldEmployee = _uiHelper.ChooseEmployee("Choose an employee to update their info.");

                            if (oldEmployee == null)
                            {
                                AnsiConsole.MarkupLine("[red]Error - no employees found.[/]");
                                
                                System.Console.ReadKey();
                                continue;
                            }

                            string firstName = _uiHelper.AskUserForText("Enter new first name"); 
                            string lastName = _uiHelper.AskUserForText("Enter new last name"); 
                            
                            var newEmployee = new Employee(firstName, lastName);
                            
                            _uiHelper.ShowEmployeeChanges(oldEmployee, newEmployee);

                            if (_uiHelper.Confirmed("Do you want to proceed?"))
                            {
                                var result = _apiClient.UpdateEmployee(oldEmployee.Id, newEmployee);

                                if (result != null)
                                {
                                    _uiHelper.ShowEmployee("Result", result);
                                    AnsiConsole.MarkupLine("Successfully updated the employee!");
                                }
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("Employee update cancelled.");
                            }

                            System.Console.ReadKey();
                            break;
                        }
                        case "View all employees":
                        {
                            _uiHelper.ShowEmployees();
                            
                            System.Console.ReadKey();
                            break;
                        }
                        case "View shifts of a specific employee":
                        {
                            var employee = _uiHelper.ChooseEmployee("Choose an employee to view their shifts");

                            if (employee == null)
                            {
                                AnsiConsole.MarkupLine("[red]Error - no employees found.[/]");
                                
                                System.Console.ReadKey();
                                continue;
                            }
                            
                            // NOT DONE
                            
                            System.Console.ReadKey();
                            break;
                        }
                        case "View a specific employee by ID":
                        {
                            int id = _uiHelper.AskUserForId();
                            var employee = _apiClient.GetEmployeeById(id);

                            if (employee == null)
                            {
                                AnsiConsole.MarkupLine("[red]Error - no employee found.[/]");
                                System.Console.ReadKey();
                                continue;
                            }
                            
                            _uiHelper.ShowEmployee($"Employee #{employee.Id}", employee);

                            System.Console.ReadKey();
                            break;
                        }
                        case "Delete an employee":
                        {
                            var employee = _uiHelper.ChooseEmployee("Choose an employee to delete");

                            if (employee == null)
                            {
                                AnsiConsole.MarkupLine("[red]Error - no employees found.[/]");
                                System.Console.ReadKey();
                                continue;
                            }
                            
                            var result = _apiClient.DeleteEmployee(employee.Id);
                            
                            if (result != null)
                                AnsiConsole.MarkupLine($"[mediumpurple3]API -> {result}[/]");
                            
                            System.Console.ReadKey();
                            break;
                        }
                        case "Go to main menu":
                        {
                            continue;
                        }
                    }
                    
                    break;
            }
        }
    }
}