using ShiftsLogger.Console.Models;
using Spectre.Console;

namespace ShiftsLogger.Console;

public class UserInterfaceHelper
{
    private ApiClient _apiClient = new ApiClient();
    private Utilities _utilities = new Utilities();
    
    public Employee? ChooseEmployee(string title)
    {
        var employees = _apiClient.GetEmployees();
        
        if (employees == null || employees.Count == 0)
            return null;
        
        var employee = AnsiConsole.Prompt(
            new SelectionPrompt<Employee>()
                .Title(title)
                .AddChoices(employees)
                .HighlightStyle(Color.MediumPurple3)
                .UseConverter(e => e.FirstName + " " + e.LastName)
        );
        
        return employee;
    }
    
    public void ShowEmployee(string message, Employee employee)
    {
        AnsiConsole.Clear();
        
        var table = new Table();
        table.Title($"[slateblue1]{message}[/]");
        table.BorderColor(Color.MediumPurple3);
        table.AddColumn(new TableColumn($"[mediumpurple3]ID[/]"));
        table.AddColumn(new TableColumn("[mediumpurple3]First Name[/]"));
        table.AddColumn(new TableColumn("[mediumpurple3]Last Name[/]"));
        table.AddRow(employee.Id.ToString(), employee.FirstName, employee.LastName);
        
        AnsiConsole.Write(table);
    }

    public void ShowEmployees()
    {
        AnsiConsole.Clear();
        
        var employees = _apiClient.GetEmployees();

        if (employees == null || employees.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Error - no employees found.[/]");
            return;
        }

        List<int> id = new List<int>();
        List<string> firstName = new List<string>();
        List<string> lastName = new List<string>();

        foreach (var employee in employees)
        {
            id.Add(employee.Id);
            firstName.Add(employee.FirstName);
            lastName.Add(employee.LastName);
        }
        
        string idCell = string.Join("\n", id);
        string firstNameCell = string.Join("\n", firstName);
        string lastNameCell = string.Join("\n", lastName);
        
        var table = new Table();
        table.Title("[slateblue1]Employees[/]");
        table.BorderColor(Color.MediumPurple3);
        table.AddColumn(new TableColumn($"[mediumpurple3]ID[/]"));
        table.AddColumn(new TableColumn("[mediumpurple3]First Name[/]"));
        table.AddColumn(new TableColumn("[mediumpurple3]Last Name[/]"));
        table.AddRow(idCell, firstNameCell, lastNameCell);
        
        AnsiConsole.Write(table);
    }
    
    public void ShowEmployeeChanges(Employee oldEmployee, Employee newEmployee)
    {
        AnsiConsole.Clear();
        
        var table = new Table();
        table.Title("[slateblue1]Confirm employee's update[/]");
        table.BorderColor(Color.MediumPurple3);
        table.AddColumn(new TableColumn(""));
        table.AddColumn(new TableColumn($"[mediumpurple3]Old version[/]"));
        table.AddColumn(new TableColumn("[mediumpurple3]New version[/]"));
        table.AddRow("ID", oldEmployee.Id.ToString(), oldEmployee.Id.ToString());
        table.AddRow("First Name", oldEmployee.FirstName, newEmployee.FirstName);
        table.AddRow("Last Name", oldEmployee.LastName, newEmployee.LastName);
        
        AnsiConsole.Write(table);
    }
    
    public void ShowShifts(Employee? employee = null)
    {
        AnsiConsole.Clear();
        
        List<Shift>? shifts = new List<Shift>();
        List<Employee>? employees = _apiClient.GetEmployees();

        string tableTitle = "";

        if (employee == null)
        {
            shifts = _apiClient.GetShifts();
            tableTitle = "[slateblue1]Shifts[/]";
        }
        else
        {
            shifts = _apiClient.GetShiftsByEmployeeId(employee.Id);
            tableTitle = $"[slateblue1](#{employee.Id}) {employee.FirstName} {employee.LastName}'s shifts[/]";
        }

        if (shifts == null || shifts.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Error - no shifts found.[/]");
            return;
        }
        
        if (employees == null || employees.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Error - no employees found.[/]");
            return;
        }
        
        List<int> id = new List<int>();
        List<int> employeeId = new List<int>();
        
        List<string> firstName = new List<string>();
        List<string> LastName = new List<string>();
        
        List<string> startTime = new List<string>();
        List<string> endTime = new List<string>();
        List<string> duration = new List<string>();

        foreach (var shift in shifts)
        {
            id.Add(shift.Id);
            employeeId.Add(shift.EmployeeId);
            firstName.Add(employees.FirstOrDefault(e => e.Id == shift.EmployeeId).FirstName);
            LastName.Add(employees.FirstOrDefault(e => e.Id == shift.EmployeeId).LastName);
            startTime.Add(shift.StartTime.ToString("g"));
            endTime.Add(shift.EndTime.ToString("g"));
            duration.Add(_utilities.CalculateDuration(shift.DurationInSeconds).ToString("g"));
        }
        
        string idCell = string.Join("\n", id);
        string employeeIdCell = string.Join("\n", employeeId);
        
        string firstNameCell = string.Join("\n", firstName);
        string lastNameCell = string.Join("\n", LastName);
        
        string startTimeCell = string.Join("\n", startTime);
        string endTimeCell = string.Join("\n", endTime);
        string durationCell = string.Join("\n", duration);
        
        var table = new Table();
        table.Title(tableTitle);
        table.BorderColor(Color.MediumPurple3);
        table.Alignment(Justify.Center);
        table.AddColumn(new TableColumn($"[mediumpurple3]ID[/]"));
        table.AddColumn(new TableColumn("[mediumpurple3]Employee ID[/]"));
        table.AddColumn(new TableColumn("[mediumpurple3]First Name[/]"));
        table.AddColumn(new TableColumn("[mediumpurple3]Last Name[/]"));
        table.AddColumn(new TableColumn("[mediumpurple3]Start Time[/]"));
        table.AddColumn(new TableColumn("[mediumpurple3]End Time[/]"));
        table.AddColumn(new TableColumn("[mediumpurple3]Duration[/]"));
        table.AddRow(idCell, employeeIdCell, firstNameCell, lastNameCell, startTimeCell, endTimeCell, durationCell);
        
        AnsiConsole.Write(table);
    }
    
    public int AskUserForId()
    {
        int id = -1;

        while (id <= 0)
        {
            id = AnsiConsole.Prompt(
                new TextPrompt<int>("[grey66]Please enter the ID:[/]").PromptStyle(Color.Grey66));

            if (id <= 0)
            {
                AnsiConsole.MarkupLine("[red]Error - invalid ID.[/]\n"); ;
            }
            else
            {
                return id;
            }
        }

        return -1;
    }

    public string AskUserForText(string message)
    {
        string text = AnsiConsole.Prompt(
            new TextPrompt<string>($"[grey66]{message}:[/]").PromptStyle(Color.Grey66));
        
        return text;
    }

    public bool Confirmed(string message)
    {
        bool confirmed = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(message)
                .AddChoices("Y", "N")
                .HighlightStyle(Color.MediumPurple3)
        ) == "Y";
        
        return confirmed;
    }
}