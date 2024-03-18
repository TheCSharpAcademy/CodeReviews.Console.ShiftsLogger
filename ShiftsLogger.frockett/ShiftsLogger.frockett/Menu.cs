using ShiftsLogger.frockett.UI.UI;
using Spectre.Console;
using ShiftsLogger.frockett.UI.Dtos;

namespace ShiftsLogger.frockett.UI;

public class Menu
{
    private readonly ApiService apiService;
    private readonly TableEngine tableEngine;
    private readonly UserInput userInput;

    public Menu(ApiService apiService, TableEngine tableEngine, UserInput userInput)
    {
        this.apiService = apiService;
        this.tableEngine = tableEngine;
        this.userInput = userInput;
    }

    public async Task MainMenuHandler()
    {
        var menuSelection = new SelectionPrompt<MainMenuOptions>()
            .Title("Main Menu")
            .AddChoices(Enum.GetValues<MainMenuOptions>())
            .UseConverter(option => option.GetEnumDescription());

        var selection = AnsiConsole.Prompt(menuSelection);

        switch (selection)
        {
            case MainMenuOptions.ViewShifts:
                await HandleViewShifts();
                await PauseForUser();
                break;
            case MainMenuOptions.ViewEmployeeShifts:
                var selectedEmployee = await GetEmployeeSelection();
                await HandleViewEmployeeShifts(selectedEmployee);
                break;
            case MainMenuOptions.AddShift:
                var employeeToAddShift = await GetEmployeeSelection();
                await HandleAddShift(employeeToAddShift);
                break;
            case MainMenuOptions.DeleteShift:
                await HandleDeleteShifts();
                break;
            case MainMenuOptions.UpdateShift:
                await HandleUpdateShift();
                break;
            case MainMenuOptions.AddEmployee:
                await HandleAddEmplyee();
                break;
            case MainMenuOptions.DeleteEmployee:
                await HandleDeleteEmployee();
                break;
            case MainMenuOptions.UpdateEmployee:
                await HandleUpdateEmployee();
                break;
            case MainMenuOptions.Exit:
                Environment.Exit(0);
                break;
        }
    }

    private async Task<EmployeeDto> GetEmployeeSelection()
    {
        var employees = await apiService.GetListOfEmployees();
        var selectedEmployee = tableEngine.SelectEmployeeFromList(employees);
        return selectedEmployee;
    }

    private async Task HandleViewShifts()
    {
        var shiftDtos = await apiService.GetAllShifts();
        tableEngine.PrintShifts(shiftDtos);
    }

    private async Task PauseForUser()
    {
        AnsiConsole.WriteLine("Press Enter to Continue...");
        Console.ReadLine();
        Console.Clear();
        await MainMenuHandler();
    }

    private async Task HandleViewEmployeeShifts(EmployeeDto employee)
    {
        tableEngine.PrintShifts(employee.Shifts);
        await PauseForUser();
    }

    private async Task HandleAddShift(EmployeeDto employee)
    {
        var newShift = userInput.GetNewShift(employee.Id);
        await apiService.AddShift(newShift);
        await PauseForUser();
    }

    private async Task HandleDeleteShifts()
    {
        await HandleViewShifts();
        int shiftId = userInput.GetShiftId("Enter the System ID corresponding to the shift you'd like to delete: ");
        await apiService.DeleteShift(shiftId);
        await PauseForUser();
    }

    private async Task HandleUpdateShift()
    {
        await HandleViewShifts();
        int shiftId = userInput.GetShiftId("Enter the System ID corresponding to the shift you'd like to update: ");
        var newShiftData = userInput.GetUpdatedShift(shiftId);
        var shiftToUpdate = await apiService.GetShiftById(shiftId);

        if(shiftToUpdate != null)
        {
            shiftToUpdate.StartTime = newShiftData.StartTime;
            shiftToUpdate.EndTime = newShiftData.EndTime;

            await apiService.UpdateShift(shiftToUpdate);
            await PauseForUser();
        }
        else
        {
            await PauseForUser();
        }
    }

    private async Task HandleAddEmplyee()
    {
        EmployeeCreateDto newEmployee = userInput.GetNewEmployee();
        await apiService.AddEmployee(newEmployee);
        await PauseForUser();
    }

    private async Task HandleDeleteEmployee()
    {
        EmployeeDto employeeToDelete = await GetEmployeeSelection();
        
        if(AnsiConsole.Confirm($"Are you sure you want to delete {employeeToDelete.Name} and all associated shifts?"))
        {
            await apiService.DeleteEmployee(employeeToDelete.Id);
        }
        else
        {
            AnsiConsole.WriteLine("Returning to main menu.");
        }
        await PauseForUser();
    }

    private async Task HandleUpdateEmployee()
    {
        var selectedEmployee = await GetEmployeeSelection();
        var updatedEmployee = userInput.GetUpdatedEmployee(selectedEmployee);
        await apiService.UpdateEmployee(updatedEmployee);
        await PauseForUser();
    }
}
