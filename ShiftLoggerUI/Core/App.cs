namespace ShiftLoggerUI.Core;

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
                var employeList = await _employeeService.GetAllEmployers();
                _userInputManager.DisplayAllEmployees(employeList);
                break;
            case MenuOptions.GetEmployee:
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

