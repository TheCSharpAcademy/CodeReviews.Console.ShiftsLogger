namespace ShiftLoggerUI.Core;

using Ardalis.Result;
using SharedLibrary.Models;
using ShiftLoggerUI;
using ShiftLoggerUI.Enums;
using ShiftLoggerUI.Services;
using ShiftLoggerUI.UI;

internal class App
{
    private readonly APIClient _client;
    private readonly ConnectionHelper _connectionHelper;
    private readonly EmployeeService _employeeService;
    private readonly ShiftService _shiftService;
    private bool _isRunning = true;

    public App(string baseUrl)
    {
        var httpClient = new HttpClient();
        _client = new APIClient(baseUrl, httpClient);
        _connectionHelper = new ConnectionHelper(_client);
        _employeeService = new EmployeeService(_client);
        _shiftService = new ShiftService(_client);
    }

    public async Task Run()
    {
        while (_isRunning)
        {
            if (await _connectionHelper.CheckConnectionAsync(5))
            {
                await HandleUserInteractionAsync();
            }
            else
            {
                await HandleConnectionFailureAsync();
                break;
            }
        }
    }

    private async Task HandleUserInteractionAsync()
    {
        var choice = UserInputManager.GetMenuOption();
        switch (choice)
        {
            case MenuOptions.GetAllEmployes:
                await GetAllEmployes();
                break;

            case MenuOptions.GetEmployee:
                await GetEmployeeById();
                break;
            case MenuOptions.CreateEmployee:
                var employee = UserInputManager.CreateEmployee();
                var createEmployerResult = await _employeeService.CreateEmployee(employee);

                if (!createEmployerResult.IsSuccess)
                {
                    UserInputManager.Error(createEmployerResult.Errors.First());
                }

                break;
            case MenuOptions.UpdateEmployee:
                var updateId = await GetEmployeeById();
                if (updateId == 0) break;

                var updatedBody = UserInputManager.UpdateEmployee();
                var updateEmployerResult = await _employeeService.UpdateEmployee(updateId, updatedBody);

                if (!updateEmployerResult.IsSuccess)
                {
                    UserInputManager.Error(updateEmployerResult.Errors.First());
                }

                break;
            case MenuOptions.DeleteEmployee:
                var deleteId = await GetEmployeeById();
                if (deleteId == 0) break;

                var deleteResult = await _employeeService.DeleteEmploye(deleteId);

                if (!deleteResult.IsSuccess)
                {
                    UserInputManager.Error(deleteResult.Errors.First());
                }
                break;
            case MenuOptions.GetAllShifts:
                await GetAllShifts();
                break;
            case MenuOptions.GetShift:
                await GetShiftById();
                break;
            case MenuOptions.CreateShift:
                var employerId = await GetEmployeeById();
                var newShift = UserInputManager.CreateShift(employerId);
                var newShiftResult = await _shiftService.CreateEmployer(newShift);

                if (!newShiftResult.IsSuccess)
                {
                    UserInputManager.Error(newShiftResult.Errors.First());
                }

                break;
            case MenuOptions.UpdateShift:
                var shiftUpdateId = await GetShiftById();
                if (shiftUpdateId == 0) break;

                var shiftUpdateBody = UserInputManager.UpdateShift();
                var updateShiftResult = await _shiftService.UpdateShift(shiftUpdateId, shiftUpdateBody);

                if (!updateShiftResult.IsSuccess)
                {
                    UserInputManager.Error(updateShiftResult.Errors.First());
                }
                break;
            case MenuOptions.DeleteShift:
                var shiftDeleteId = await GetShiftById();
                if (shiftDeleteId == 0) break;

                var shiftDeleteResult = await _shiftService.DeleteShift(shiftDeleteId);

                if (!shiftDeleteResult.IsSuccess)
                {
                    UserInputManager.Error(shiftDeleteResult.Errors.First());
                }
                break;
            case MenuOptions.ShiftByEmployee:
                var employeeIdRes = _employeeService.GetEmployee(UserInputManager.GetId()).Result;

                if (employeeIdRes.IsSuccess)
                {
                    var shifts = await _shiftService.GetAllShifts();
                    var filterShifts = shifts.Value.Where(x => x.EmployeeId == employeeIdRes.Value.Id).ToList();
                    UserInputManager.DisplayAllShifts(filterShifts);
                }
                else
                {
                    UserInputManager.Error(employeeIdRes.Errors.First());
                }

                break;
            case MenuOptions.Exit:
                _isRunning = false;
                break;
        }
    }

    private async Task GetAllEmployes()
    {
        var result = await _employeeService.GetAllEmployees();
        if (result.IsSuccess)
        {
            UserInputManager.DisplayAllEmployees(result.Value);
        }
        else
        {
            UserInputManager.Error(result.Errors.FirstOrDefault()!);
        }
    }

    private async Task<int> GetEmployeeById()
    {
        while (true)
        {
            var result = await _employeeService.GetEmployee(UserInputManager.GetId());

            if (result.IsSuccess)
            {
                UserInputManager.DisplayEmployee(result.Value);
                return result.Value.Id;
            }

            string errorMessage = result.IsNotFound()
                ? "This id does not exist. Status code: 404"
                : result.Errors.FirstOrDefault()
                ?? "An unknown error occurred";

            UserInputManager.Error(errorMessage);

            if (!UserInputManager.Retry())
                return 0;
        }
    }

    private async Task GetAllShifts()
    {
        var result = await _shiftService.GetAllShifts();
        if (result.IsSuccess)
        {
            UserInputManager.DisplayAllShifts(result.Value);
        }
        else
        {
            UserInputManager.Error(result.Errors.FirstOrDefault()!);
        }
    }

    private async Task<int> GetShiftById()
    {
        while (true)
        {
            var result = await _shiftService.GetShift(UserInputManager.GetId());

            if (result.IsSuccess)
            {
                UserInputManager.DisplayShift(result.Value);
                return result.Value.Id;
            }

            string errorMessage = result.IsNotFound()
                ? "This id does not exist. Status code: 404"
                : result.Errors.FirstOrDefault()
                ?? "An unknown error occurred";

            UserInputManager.Error(errorMessage);

            if (!UserInputManager.Retry())
                return 0;
        }
    }

    private static async Task HandleConnectionFailureAsync()
    {
        await Console.Out.WriteLineAsync("Could not connect. Application will close on next key press.");
        Console.ReadLine();
    }
}

