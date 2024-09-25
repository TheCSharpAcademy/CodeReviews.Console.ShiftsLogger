using ShiftLoggerUI;
using ShiftsLoggerLibrary;
using Spectre.Console;

namespace ShiftsLoggerUI;

public class ShiftService
{
    public static async void CreateShift()
    {
        MenuPresentation.PresentMenu("[blue]Inserting[/]");
        bool isCancelled;
        string start, end, employeeId;

        DateTimeValidator dateValidator = new();

        (isCancelled, start) = AskForStartDate(dateValidator);
        if (isCancelled) return;

        FutureDateTimeValidator futureDateTimeValidator = new(dateValidator);

        (isCancelled, end) = AskForEndDate(Convert.ToDateTime(start), futureDateTimeValidator);
        if (isCancelled) return;

        ExistingModelValidator<string, Employee> existingEmployee = new()
        {
            ErrorMsg = "Employee Id doesn't exist.",
            GetModel = EmployeeService.GetEmployeeById
        };

        (isCancelled, employeeId) = EmployeeService.AskForEmployeeId(existingEmployee);
        if (isCancelled) return;

        var shift = await ShiftController.InsertShiftAsync(new Shift
        {
            Start = Convert.ToDateTime(start),
            End = Convert.ToDateTime(end),
            EmployeeId = Convert.ToInt32(employeeId)
        });

        if (shift == null)
        {
            AnsiConsole.MarkupLine("[red]Error creating the Shift[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[green]Shift created successfully[/]");
        }

        Prompter.PressKeyToContinuePrompt();
    }

    public static void UpdateShift()
    {
        MenuPresentation.PresentMenu("[yellow]Updating[/]");
        bool isCancelled;
        string oldShiftId, start, end, employeeId;

        ShowShiftTable();

        ExistingModelValidator<string, Shift> existingShift = new()
        {
            ErrorMsg = "Employee Id doesn't exist.",
            GetModel = GetShiftById
        };

        (isCancelled, oldShiftId) = AskForShiftId(existingShift);
        if (isCancelled) return;

        DateTimeValidator dateValidator = new();

        (isCancelled, start) = AskForStartDate(dateValidator);
        if (isCancelled) return;

        FutureDateTimeValidator futureDateTimeValidator = new(dateValidator);

        (isCancelled, end) = AskForEndDate(Convert.ToDateTime(start), futureDateTimeValidator);
        if (isCancelled) return;

        ExistingModelValidator<string, Employee> existingEmployee = new()
        {
            ErrorMsg = "Employee Id doesn't exist.",
            GetModel = EmployeeService.GetEmployeeById
        };

        EmployeeService.ShowEmployeeTable();

        (isCancelled, employeeId) = EmployeeService.AskForEmployeeId(existingEmployee);
        if (isCancelled) return;

        bool updated = ShiftController.UpdateShiftAsync(new Shift
        {
            ShiftId = Convert.ToInt32(oldShiftId),
            Start = Convert.ToDateTime(start),
            End = Convert.ToDateTime(end),
            EmployeeId = Convert.ToInt32(employeeId)
        }).Result;

        if (updated)
        {
            AnsiConsole.MarkupLine("[green]Shift updated successfully[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Error updating the shift[/]");
        }
        Prompter.PressKeyToContinuePrompt();
    }

    public static void DeleteShift()
    {
        MenuPresentation.PresentMenu("[red]Deleting[/]");
        bool isCancelled;
        string id;

        ShowShiftTable();

        ExistingModelValidator<string, Shift> existingShift = new()
        {
            ErrorMsg = "Employee Id doesn't exist.",
            GetModel = GetShiftById
        };

        (isCancelled, id) = AskForShiftId(existingShift);
        if (isCancelled) return;

        if (ShiftController.DeleteShiftByIdAsync(int.Parse(id)).Result)
        {
            AnsiConsole.MarkupLine("[green]Shift deleted successfully[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Error deleting the Shift[/]");
        }
        Prompter.PressKeyToContinuePrompt();
    }

    public static void ShowShifts()
    {
        ShowShiftTable();
        Prompter.PressKeyToContinuePrompt();
    }

    public static void ShowShiftsByEmployee()
    {
        bool isCancelled;
        string employeeId;

        EmployeeService.ShowEmployeeTable();

        ExistingModelValidator<string, Employee> existingEmployee = new()
        {
            ErrorMsg = "Employee Id doesn't exist.",
            GetModel = EmployeeService.GetEmployeeById
        };

        (isCancelled, employeeId) = EmployeeService.AskForEmployeeId(existingEmployee);
        if (isCancelled) return;

        var employee = EmployeeService.GetEmployeeById(employeeId);
        var shifts = ShiftController.GetShiftsAsync().Result.Where(s => s.EmployeeName == employee.Name).Select(s => ShiftMapper.MapToDto(s)).ToList();
        OutputRenderer.ShowTable(shifts, $"{employee.Name} Shifts");
        Prompter.PressKeyToContinuePrompt();
    }

    public static (bool IsCancelled, string Result) AskForShiftId(params IValidator[] validators)
    {
        string message = "Enter a Shift Id";
        return Prompter.PromptWithValidation(message, validations: validators);
    }

    public static (bool IsCancelled, string Result) AskForStartDate(params IValidator[] validators)
    {
        string message = "Enter a Start Date. Format: (yyyy-MM-dd HH:mm)";
        return Prompter.PromptWithValidation(message, defaultValue: DateTime.Now.ToString(), validations: validators);
    }

    public static (bool IsCancelled, string Result) AskForEndDate(DateTime startDate, params IValidator[] validators)
    {
        string message = "Enter a End Date. Format: (yyyy-MM-dd HH:mm)";
        return Prompter.PromptWithValidation(message, defaultValue: startDate.AddHours(8).ToString(), validations: validators);
    }

    public static Shift GetShiftById(string input)
    {
        var shifts = ShiftController.GetShiftsAsync().Result;
        return shifts.Where(s => s.ShiftId.ToString() == input).FirstOrDefault();
    }

    public static void ShowShiftTable()
    {
        List<ShiftDto> shifts = ShiftController.GetShiftsAsync().Result.Select(s => ShiftMapper.MapToDto(s)).ToList();
        OutputRenderer.ShowTable(shifts, "Shifts");
    }
}