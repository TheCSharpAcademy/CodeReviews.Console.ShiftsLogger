using ShiftsLogger.frockett.UI.UI;
using Spectre.Console;
using ShiftsLogger.frockett.UI.Dtos;
using System.Runtime.CompilerServices;

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
        //var mainMenuOptions = Enum.GetValues(typeof(MainMenuOptions)).Cast<MainMenuOptions>().ToArray();

        var menuSelection = new SelectionPrompt<MainMenuOptions>()
            .Title("Main Menu")
            .AddChoices(Enum.GetValues<MainMenuOptions>())
            .UseConverter(option => option.GetEnumDescription());
        /*
        menuSelection.AddChoices(MainMenuOptions.ViewShifts, MainMenuOptions.ViewShifts,
                                MainMenuOptions.ViewEmployeeShifts, MainMenuOptions.AddShift, 
                                MainMenuOptions.DeleteShift, MainMenuOptions.UpdateShift,
                                MainMenuOptions.AddEmployee, MainMenuOptions.DeleteEmployee,
                                MainMenuOptions.UpdateEmployee, MainMenuOptions.Exit);
        */
        var selection = AnsiConsole.Prompt(menuSelection);

        switch (selection)
        {
            case MainMenuOptions.ViewShifts:
                await HandleViewShifts();
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
                HandleDeleteShifts();
                break;
            case MainMenuOptions.UpdateShift:
                HandleUpdateShift();
                break;
            case MainMenuOptions.AddEmployee:
                await HandleAddEmplyee();
                break;
            case MainMenuOptions.DeleteEmployee:
                HandleDeleteEmployee();
                break;
            case MainMenuOptions.UpdateEmployee:
                HandleUpdateEmployee();
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
        PauseForUser();
        await MainMenuHandler();
    }

    private void PauseForUser()
    {
        AnsiConsole.WriteLine("Press Enter to Continue...");
        Console.ReadLine();
        Console.Clear();
    }

    private async Task HandleViewEmployeeShifts(EmployeeDto employee)
    {
        tableEngine.PrintShifts(employee.Shifts);
        PauseForUser();
        await MainMenuHandler();
    }

    private async Task HandleAddShift(EmployeeDto employee)
    {
        var newShift = userInput.GetNewShift(employee.Id);
        await apiService.AddShift(newShift);
        PauseForUser();
        await MainMenuHandler();
    }

    private void HandleDeleteShifts()
    {
        throw new NotImplementedException();
    }

    private void HandleUpdateShift()
    {
        throw new NotImplementedException();
    }

    private async Task HandleAddEmplyee()
    {
        EmployeeCreateDto newEmployee = userInput.GetNewEmployee();
        await apiService.AddEmployee(newEmployee);
        PauseForUser();
        await MainMenuHandler();
    }

    private void HandleDeleteEmployee()
    {
        throw new NotImplementedException();
    }

    private void HandleUpdateEmployee()
    {
        throw new NotImplementedException();
    }

    private void HandleDisplayEmployees()
    {
        throw new NotImplementedException();
    }
}
