using System.Globalization;
using ShiftsLogger.Console.Models;
using Spectre.Console;

namespace ShiftsLogger.Console;

public class UserInterfaceHelper
{
    private readonly ApiClient _apiClient = new ApiClient();
    private readonly Utilities _utilities = new Utilities();
    
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
    
    public void ShowEmployee(Employee employee, string title, string? caption = null)
    {
        AnsiConsole.Clear();
        
        var table = new Table();
        table.Title($"[slateblue1]{title}[/]");
        table.BorderColor(Color.MediumPurple3);
        table.Alignment(Justify.Center);

        if (caption != null)
            table.Caption($"[slateblue1]{caption}[/]");
        
        table.AddColumn(new TableColumn("[mediumpurple3]ID[/]"));
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
        table.Alignment(Justify.Center);
        
        table.AddColumn(new TableColumn($"[mediumpurple3]ID[/]"));
        table.AddColumn(new TableColumn("[mediumpurple3]First Name[/]"));
        table.AddColumn(new TableColumn("[mediumpurple3]Last Name[/]"));
        table.AddRow(idCell, firstNameCell, lastNameCell);
        
        AnsiConsole.Write(table);
    }
    
    public void ShowEmployeeChanges(Employee oldEmployee, Employee newEmployee, string? caption = null)
    {
        AnsiConsole.Clear();
        
        var table = new Table();
        table.Title("[slateblue1]Confirm employee's update[/]");
        table.BorderColor(Color.MediumPurple3);
        table.Alignment(Justify.Center);

        if (caption != null)
        {
            table.Caption($"[mediumpurple3]{caption}[/]");
        }
        
        table.AddColumn(new TableColumn(""));
        table.AddColumn(new TableColumn($"[mediumpurple3]Old version[/]"));
        table.AddColumn(new TableColumn("[mediumpurple3]New version[/]"));
        table.AddRow("ID", oldEmployee.Id.ToString(), oldEmployee.Id.ToString());
        table.AddRow("First Name", oldEmployee.FirstName, newEmployee.FirstName);
        table.AddRow("Last Name", oldEmployee.LastName, newEmployee.LastName);
        
        AnsiConsole.Write(table);
    }
    
    public void ShowShift(Shift shift, string title, string? caption = null)
    {
        AnsiConsole.Clear();

        long durationInSeconds = _utilities.CalculateTimeDifference(shift.StartTime, shift.EndTime);
        var duration = _utilities.CalculateDuration(durationInSeconds);
        
        var table = new Table();
        table.Title($"[slateblue1]{title}[/]");
        table.BorderColor(Color.MediumPurple3);
        table.Alignment(Justify.Center);

        if (caption != null)
            table.Caption($"[slateblue1]{caption}[/]");
        
        table.AddColumn(new TableColumn("[mediumpurple3]ID[/]"));
        table.AddColumn(new TableColumn("[mediumpurple3]Employee ID[/]"));
        table.AddColumn(new TableColumn("[mediumpurple3]Start Time[/]"));
        table.AddColumn(new TableColumn("[mediumpurple3]End Time[/]"));
        table.AddColumn(new TableColumn("[mediumpurple3]Duration[/]"));
        table.AddRow(
            shift.Id.ToString(),
            shift.EmployeeId.ToString(),
            shift.StartTime.ToString("g", CultureInfo.CurrentCulture),
            shift.EndTime.ToString("g", CultureInfo.CurrentCulture),
            duration.ToString("g", CultureInfo.InvariantCulture));
        
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
        
        if (employees == null || employees.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Error - no employees found.[/]");
            
            return;
        }

        if (shifts == null || shifts.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Error - no shifts found.[/]");
            
            return;
        }
        
        List<int> id = new List<int>();
        List<int> employeeId = new List<int>();
        List<string> firstName = new List<string>();
        List<string> lastName = new List<string>();
        List<string> startTime = new List<string>();
        List<string> endTime = new List<string>();
        List<string> duration = new List<string>();

        foreach (var shift in shifts)
        {
            var currentEmployee = employees.Find(e => e.Id == shift.EmployeeId);
            
            id.Add(shift.Id);
            employeeId.Add(shift.EmployeeId);
            
            if (currentEmployee != null)
            {
                firstName.Add(currentEmployee.FirstName);
                lastName.Add(currentEmployee.LastName);
            }
            else
            {
                firstName.Add("");
                lastName.Add("");
            }
            
            startTime.Add(shift.StartTime.ToString("g", CultureInfo.CurrentCulture));
            endTime.Add(shift.EndTime.ToString("g", CultureInfo.CurrentCulture));
            duration.Add(_utilities.CalculateDuration(shift.DurationInSeconds).ToString("g", CultureInfo.InvariantCulture));
        }
        
        string idCell = string.Join("\n", id);
        string employeeIdCell = string.Join("\n", employeeId);
        string firstNameCell = string.Join("\n", firstName);
        string lastNameCell = string.Join("\n", lastName);
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

        return;
    }
    
    public void ShowShiftChanges(Shift oldShift, Shift newShift, Employee employee, string? caption = null)
    {
        AnsiConsole.Clear();
        
        var table = new Table();
        table.Title($"[slateblue1]Confirm {employee.FirstName} {employee.LastName}'s shift update[/]");
        table.BorderColor(Color.MediumPurple3);
        table.Alignment(Justify.Center);

        if (caption != null)
        {
            table.Caption($"[slateblue1]{caption}[/]");
        }
        
        table.AddColumn(new TableColumn(""));
        table.AddColumn(new TableColumn($"[mediumpurple3]Old version[/]"));
        table.AddColumn(new TableColumn("[mediumpurple3]New version[/]"));
        table.AddRow("ID", oldShift.Id.ToString(), oldShift.Id.ToString());
        table.AddRow("Employee ID", oldShift.EmployeeId.ToString(), newShift.EmployeeId.ToString());
        table.AddRow(
            "Start Time", 
            oldShift.StartTime.ToString("g", CultureInfo.CurrentCulture),
            newShift.StartTime.ToString("g", CultureInfo.CurrentCulture));
        table.AddRow(
            "End Time", 
            oldShift.EndTime.ToString("g", CultureInfo.CurrentCulture), 
            newShift.EndTime.ToString("g", CultureInfo.CurrentCulture));
        table.AddRow(
            "Duration", 
            _utilities.CalculateDuration(oldShift.DurationInSeconds).ToString("g", CultureInfo.InvariantCulture), 
            _utilities.CalculateDuration(newShift.DurationInSeconds).ToString("g", CultureInfo.InvariantCulture));
        
        AnsiConsole.Write(table);
    }

    public DateTime[] AskForShiftDates()
    {
        DateTime startDate = DateTime.MinValue;
        DateTime endDate = DateTime.MinValue;
        
        while (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
        {
            AnsiConsole.MarkupLine("");
                                
            while (startDate == DateTime.MinValue)
            {
                string startDateInput = AskUserForText("Please enter the start time (dd/MM/yyyy HH:mm)");
                startDate = _utilities.ConvertToDate(startDateInput);
            }
                                
            AnsiConsole.MarkupLine("");

            while (endDate == DateTime.MinValue)
            {
                string endDateInput = AskUserForText("Please enter the end time (dd/MM/yyyy HH:mm)");
                endDate = _utilities.ConvertToDate(endDateInput);
            }

            if (!_utilities.ValidateDates(startDate, endDate))
            {
                startDate = DateTime.MinValue;
                endDate = DateTime.MinValue;
            }
        }

        DateTime[] dates = { startDate, endDate };

        return dates;
    }
    
    public int AskUserForId()
    {
        while (true)
        {
            int id = AnsiConsole.Prompt(
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
                .Title($"[mediumpurple3]{message}[/]")
                .AddChoices("Y", "N")
                .HighlightStyle(Color.MediumPurple3)
        ) == "Y";
        
        return confirmed;
    }
    
    public void PressAnyKey()
    {
        AnsiConsole.MarkupLine("\n[grey66]Press any key to continue.[/]");
        System.Console.ReadKey(true);
    }
}