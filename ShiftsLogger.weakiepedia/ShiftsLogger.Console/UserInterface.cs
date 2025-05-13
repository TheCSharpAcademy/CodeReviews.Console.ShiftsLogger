using Spectre.Console;
using ShiftsLogger.Console.Models;

namespace ShiftsLogger.Console;

public class UserInterface
{
    private UserInterfaceHelper _uiHelper = new UserInterfaceHelper();
    private ApiClient _apiClient = new ApiClient();
    
    private readonly string[] _menuOptions = { "Shifts", "Employees" };
    private readonly string[] _shiftsMenuOptions = { "Add a new shift", "Update a shift", "View all shifts", "View shifts of a specific employee", "View a specific shift by ID", "Delete a shift", "Delete shifts of no longer employees" ,"Go to main menu" };
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
                            var employee = _uiHelper.ChooseEmployee("Choose an employee to add their shift");
                            
                            if (employee == null)
                            {
                                AnsiConsole.MarkupLine("[red]Error - no employees found.[/]");

                                _uiHelper.PressAnyKey();
                                continue;
                            }
                            
                            var shiftDates = _uiHelper.AskForShiftDates();
                            
                            var shift = new Shift(employee.Id, shiftDates[0], shiftDates[1]);
                            
                            _uiHelper.ShowShift(shift,
                                $"Confirm shift creation for {employee.FirstName} {employee.LastName}", 
                                "(ID will be set automatically)");

                            if (_uiHelper.Confirmed("Do you want to proceed?"))
                            {
                                var result = _apiClient.CreateShift(shift);
                                
                                if (result != null)
                                {
                                    _uiHelper.ShowShift(result, "Result");
                                    AnsiConsole.MarkupLine("Successfully created the shift!");
                                }
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("Shift creation cancelled.");
                            }
                            
                            _uiHelper.PressAnyKey();
                            break;
                        }
                        case "Update a shift":
                        {
                            var employee = _uiHelper.ChooseEmployee("Choose an employee to update their shift.");

                            if (employee == null)
                            {
                                AnsiConsole.MarkupLine("[red]Error - no employees found.[/]");
                                
                                _uiHelper.PressAnyKey();
                                continue;
                            }
                            
                            var employeeShifts = _apiClient.GetShiftsByEmployeeId(employee.Id);
                            
                            if (employeeShifts == null || !employeeShifts.Any())
                            {
                                AnsiConsole.MarkupLine("[red]Error - no employee's shifts found.[/]\n");
                                
                                _uiHelper.PressAnyKey();
                                continue;
                            }
                            
                            _uiHelper.ShowShifts();

                            int id;
                            while (true)
                            {
                                id = _uiHelper.AskUserForId();

                                if (employeeShifts.Exists(s => s.Id == id)) 
                                {
                                    break;
                                }
                                else
                                {
                                    AnsiConsole.MarkupLine("[red]Error - please enter the shift ID of the chosen employee.[/]\n");
                                    
                                    continue;
                                }
                            }
                            
                            var shiftDates = _uiHelper.AskForShiftDates();
                            
                            var oldShift = _apiClient.GetShiftById(id);
                            var newShift = new Shift(employee.Id, shiftDates[0], shiftDates[1]);
                            
                            if (oldShift == null)
                            {
                                AnsiConsole.MarkupLine("[red]Error - no shift found.[/]");
                                
                                _uiHelper.PressAnyKey();
                                continue;
                            }
                            
                            _uiHelper.ShowShiftChanges(oldShift, newShift, employee, "(Duration will be set automatically)");

                            if (_uiHelper.Confirmed("Do you want to proceed?"))
                            {
                                var result = _apiClient.UpdateShift(oldShift.Id, newShift);

                                if (result != null)
                                {
                                    _uiHelper.ShowShift(result, "Result");
                                    AnsiConsole.MarkupLine("Successfully updated the shift!");
                                }
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("Shift update cancelled.");
                            }

                            _uiHelper.PressAnyKey();
                            break;
                        }
                        case "View all shifts":
                        {
                            _uiHelper.ShowShifts();
                            
                            _uiHelper.PressAnyKey();
                            break;
                        }
                        case "View shifts of a specific employee":
                        {
                            var employee = _uiHelper.ChooseEmployee("Choose an employee to view their shifts");
                            
                            _uiHelper.ShowShifts(employee);
                            
                            _uiHelper.PressAnyKey();
                            break;
                        }
                        case "View a specific shift by ID":
                        {
                            int id = _uiHelper.AskUserForId();
                            var shift = _apiClient.GetShiftById(id);

                            if (shift == null)
                            {
                                AnsiConsole.MarkupLine("[red]Error - no shift found.[/]");
                                System.Console.ReadKey();
                                continue;
                            }
                            
                            _uiHelper.ShowShift(shift, $"Shift #{shift.Id}");

                            System.Console.ReadKey();
                            break;
                        }
                        case "Delete a shift":
                        {
                            var shifts = _apiClient.GetShifts();
                            
                            if (shifts == null || !shifts.Any())
                            {
                                AnsiConsole.MarkupLine("[red]Error - no shifts found.[/]\n");
                                
                                _uiHelper.PressAnyKey();
                                continue;
                            }
                            
                            _uiHelper.ShowShifts();
                            
                            int id = _uiHelper.AskUserForId();

                            if (_uiHelper.Confirmed("Do you really want to delete this shift?"))
                            {
                                var result = _apiClient.DeleteShift(id);
                            
                                if (result != null)
                                    AnsiConsole.MarkupLine(result);
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("Shift deletion cancelled.");
                            }
                            
                            _uiHelper.PressAnyKey();
                            break;
                        }
                        case "Delete shifts of no longer employees":
                        {
                            var shifts = _apiClient.GetShifts();

                            if (shifts == null || !shifts.Any())
                            {
                                AnsiConsole.MarkupLine("[red]Error - no shifts found.[/]\n");
                                
                                _uiHelper.PressAnyKey();
                                continue;
                            }

                            int deletionCounter = 0;
                            
                            foreach (var shift in shifts)
                            {
                                var employee = _apiClient.GetEmployeeById(shift.EmployeeId);
                                
                                if (employee == null)
                                {
                                    _apiClient.DeleteShift(shift.Id);
                                    deletionCounter++;
                                }
                            }

                            if (deletionCounter > 0)
                            {
                                AnsiConsole.MarkupLine($"Deleted {deletionCounter} shifts of no longer employees.");
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("Found no shifts of no longer employees.");
                            }
                            
                            _uiHelper.PressAnyKey();
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
                            
                            var employee = new Employee(firstName, lastName);
                            
                            _uiHelper.ShowEmployee(employee, 
                                "Confirm employee creation",
                                "(ID will be set automatically)");

                            if (_uiHelper.Confirmed("Do you want to proceed?"))
                            {
                                var result = _apiClient.CreateEmployee(employee);
                                
                                if (result != null)
                                {
                                    _uiHelper.ShowEmployee(result, "Result");
                                    AnsiConsole.MarkupLine("Successfully created the employee!");
                                }
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("Employee creation cancelled.");
                            }
                            
                            _uiHelper.PressAnyKey();
                            break;
                        }
                        case "Update employee's info":
                        {
                            var oldEmployee = _uiHelper.ChooseEmployee("Choose an employee to update their info.");

                            if (oldEmployee == null)
                            {
                                AnsiConsole.MarkupLine("[red]Error - no employees found.[/]");
                                
                                _uiHelper.PressAnyKey();
                                continue;
                            }

                            string firstName = _uiHelper.AskUserForText("Enter new first name"); 
                            string lastName = _uiHelper.AskUserForText("Enter new last name"); 
                            
                            var newEmployee = new Employee(firstName, lastName);
                            
                            _uiHelper.ShowEmployeeChanges(oldEmployee, newEmployee, "(ID will be set automatically)");

                            if (_uiHelper.Confirmed("Do you want to proceed?"))
                            {
                                var result = _apiClient.UpdateEmployee(oldEmployee.Id, newEmployee);

                                if (result != null)
                                {
                                    _uiHelper.ShowEmployee(result, "Result");
                                    AnsiConsole.MarkupLine("Successfully updated the employee!");
                                }
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("Employee update cancelled.");
                            }

                            _uiHelper.PressAnyKey();
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
                            
                            _uiHelper.ShowShifts(employee);
                            
                            _uiHelper.PressAnyKey();
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
                            
                            _uiHelper.ShowEmployee(employee, $"Employee #{employee.Id}");

                            System.Console.ReadKey();
                            break;
                        }
                        case "Delete an employee":
                        {
                            var employee = _uiHelper.ChooseEmployee("Choose an employee to delete");

                            if (employee == null)
                            {
                                AnsiConsole.MarkupLine("[red]Error - no employees found.[/]");
                                
                                _uiHelper.PressAnyKey();
                                continue;
                            }
                            
                            if (_uiHelper.Confirmed("Do you really want to delete this employee?"))
                            {
                                var result = _apiClient.DeleteEmployee(employee.Id);
                            
                                if (result != null)
                                    AnsiConsole.MarkupLine(result);
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("Employee deletion cancelled.");
                            }
                            
                            _uiHelper.PressAnyKey();
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