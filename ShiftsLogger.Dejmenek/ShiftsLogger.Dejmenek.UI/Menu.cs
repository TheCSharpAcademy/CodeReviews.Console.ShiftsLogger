using ShiftsLogger.Dejmenek.UI.Controllers;
using ShiftsLogger.Dejmenek.UI.Enums;
using ShiftsLogger.Dejmenek.UI.Helpers;
using ShiftsLogger.Dejmenek.UI.Services.Interfaces;

namespace ShiftsLogger.Dejmenek.UI;
public class Menu
{
    private readonly IUserInteractionService _userInteractionService;
    private readonly EmployeesController _employeesController;
    private readonly ShiftsController _shiftsController;

    public Menu(IUserInteractionService userInteractionService, EmployeesController employeesController, ShiftsController shiftsController)
    {
        _userInteractionService = userInteractionService;
        _employeesController = employeesController;
        _shiftsController = shiftsController;
    }

    public async Task Run()
    {
        bool exitMainMenu = false;
        while (!exitMainMenu)
        {
            var userChoice = _userInteractionService.GetMenuOption();

            switch (userChoice)
            {
                case MenuOptions.Exit:
                    exitMainMenu = true;
                    break;

                case MenuOptions.ManageEmployees:
                    await ManageEmployeesMenu();
                    break;

                case MenuOptions.ManageShifts:
                    await ManageShiftsMenu();
                    break;
            }
        }
    }

    public async Task ManageEmployeesMenu()
    {
        bool exitManageEmployeeMenu = false;

        while (!exitManageEmployeeMenu)
        {
            var userChoice = _userInteractionService.GetManageEmployeesOptions();

            switch (userChoice)
            {
                case ManageEmployeesOptions.Exit:
                    exitManageEmployeeMenu = true;
                    break;

                case ManageEmployeesOptions.AddEmployee:
                    await _employeesController.AddEmployeeAsync();
                    _userInteractionService.GetUserInputToContinue();
                    Console.Clear();
                    break;

                case ManageEmployeesOptions.ViewAllEmployees:
                    var employees = await _employeesController.GetEmployeesAsync();
                    DataVisualizer.DisplayEmployees(employees);
                    _userInteractionService.GetUserInputToContinue();
                    Console.Clear();
                    break;

                case ManageEmployeesOptions.UpdateEmployee:
                    await _employeesController.UpdateEmployeeAsync();
                    _userInteractionService.GetUserInputToContinue();
                    Console.Clear();
                    break;

                case ManageEmployeesOptions.DeleteEmployee:
                    await _employeesController.RemoveEmployeeAsync();
                    _userInteractionService.GetUserInputToContinue();
                    Console.Clear();
                    break;

                case ManageEmployeesOptions.ViewEmployeeShifts:
                    var employeeShifts = await _employeesController.GetEmployeeShifts();
                    DataVisualizer.DisplayEmployeeShifts(employeeShifts);
                    _userInteractionService.GetUserInputToContinue();
                    Console.Clear();
                    break;
            }
        }
    }

    public async Task ManageShiftsMenu()
    {
        bool exitanageShiftsMenu = false;

        while (!exitanageShiftsMenu)
        {
            var userChoice = _userInteractionService.GetManageShiftsOptions();

            switch (userChoice)
            {
                case ManageShiftsOptions.Exit:
                    exitanageShiftsMenu = true;
                    break;

                case ManageShiftsOptions.AddShift:
                    await _shiftsController.AddShift();
                    _userInteractionService.GetUserInputToContinue();
                    Console.Clear();
                    break;

                case ManageShiftsOptions.UpdateShift:
                    await _shiftsController.UpdateShift();
                    _userInteractionService.GetUserInputToContinue();
                    Console.Clear();
                    break;

                case ManageShiftsOptions.DeleteShift:
                    await _shiftsController.RemoveShift();
                    _userInteractionService.GetUserInputToContinue();
                    Console.Clear();
                    break;

                case ManageShiftsOptions.ViewAllShifts:
                    var shifts = await _shiftsController.GetShifts();
                    DataVisualizer.DisplayShifts(shifts);
                    _userInteractionService.GetUserInputToContinue();
                    Console.Clear();
                    break;
            }
        }
    }
}
