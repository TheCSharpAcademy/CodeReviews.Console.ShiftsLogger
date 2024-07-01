namespace ShiftLoggerUI.Core;

using Ardalis.Result;
using ShiftLoggerUI;
using ShiftLoggerUI.Enums;
using ShiftLoggerUI.Services;
using ShiftLoggerUI.UI;

internal class App
{
    private readonly APIClient _client;
    private readonly ConnectionHelper _connectionHelper;
    private readonly EmployeeService _employeeService;
    private bool _isRunning = true;

    public App(string baseUrl) // TODO move baseUrl over to configmanager?
    {
        var httpClient = new HttpClient();
        _client = new APIClient(baseUrl, httpClient);
        _connectionHelper = new ConnectionHelper(_client);
        _employeeService = new EmployeeService(_client);
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
                var getAllEmployesResult = await _employeeService.GetAllEmployes();
                if (getAllEmployesResult.IsSuccess)
                {
                    UserInputManager.DisplayAllEmployees(getAllEmployesResult.Value);
                }
                else
                {
                    UserInputManager.Error(getAllEmployesResult.Errors.FirstOrDefault()!);
                }
                break;

            case MenuOptions.GetEmployee:
                while (true)
                {
                    var id = UserInputManager.GetId();
                    var getEmployerResult = await _employeeService.GetEmployer(id);

                    if (getEmployerResult.IsSuccess)
                    {
                        UserInputManager.DisplayEmployee(getEmployerResult.Value);
                        break;
                    }

                    string errorMessage = getEmployerResult.IsNotFound()
                        ? "This id does not exist. Status code: 404"
                        : getEmployerResult.Errors.FirstOrDefault() 
                        ?? "An unknown error occurred";

                    UserInputManager.Error(errorMessage);

                    if (!UserInputManager.Retry())
                        return;
                }
                break;
            case MenuOptions.CreateEmployee:
                var employee = UserInputManager.CreateEmployee();
                var createEmployerResult = await _employeeService.CreateEmployer(employee);

                if (!createEmployerResult.IsSuccess)
                {
                     UserInputManager.Error(createEmployerResult.Errors.First());
                }

                break;
            case MenuOptions.UpdateEmployee:
                break;
            case MenuOptions.DeleteEmployee:
                break;
            case MenuOptions.GetAllShifts:
                break;
            case MenuOptions.GetShift:
                break;
            case MenuOptions.CreateShift:
                break;
            case MenuOptions.UpdateShift:
                break;
            case MenuOptions.DeleteShift:
                break;
            case MenuOptions.Exit:
                _isRunning = false;
                break;
        }


    }

    private static async Task HandleConnectionFailureAsync()
    {
        await Console.Out.WriteLineAsync("Could not connect. Application will close on next key press.");
        Console.ReadLine();
    }
}

