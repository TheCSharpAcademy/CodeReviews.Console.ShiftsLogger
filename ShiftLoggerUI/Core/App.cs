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
    private readonly UserInputManager _userInputManager;
    private readonly EmployeeService _employeeService;
    private bool _isRunning = true;

    public App(string baseUrl) // TODO move baseUrl over to configmanager?
    {
        var httpClient = new HttpClient();
        _client = new APIClient(baseUrl, httpClient);
        _connectionHelper = new ConnectionHelper(_client);
        _userInputManager = new UserInputManager();
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
        var choice = _userInputManager.GetMenuOption();
        switch (choice)
        {
            case MenuOptions.GetAllEmployers:
                var getAllEmployersResult = await _employeeService.GetAllEmployers();
                if (getAllEmployersResult.IsSuccess)
                {
                    _userInputManager.DisplayAllEmployees(getAllEmployersResult.Value);
                }
                else
                {
                    _userInputManager.Error(getAllEmployersResult.Errors.FirstOrDefault()!);
                }
                break;

            case MenuOptions.GetEmployee:
                while (true)
                {
                    var id = _userInputManager.GetId();
                    var getEmployerResult = await _employeeService.GetEmployer(id);

                    if (getEmployerResult.IsSuccess)
                    {
                        _userInputManager.DisplayEmployee(getEmployerResult.Value);
                        break;
                    }

                    string errorMessage = getEmployerResult.IsNotFound()
                        ? "This id does not exist. Status code: 404"
                        : getEmployerResult.Errors.FirstOrDefault() ?? "An unknown error occurred";

                    _userInputManager.Error(errorMessage);

                    if (!_userInputManager.Retry())
                        return;
                }
                break;
            case MenuOptions.CreateEmployee:
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

    private async Task HandleConnectionFailureAsync()
    {
        await Console.Out.WriteLineAsync("Could not connect. Application will close on next key press.");
        Console.ReadLine();
    }
}

