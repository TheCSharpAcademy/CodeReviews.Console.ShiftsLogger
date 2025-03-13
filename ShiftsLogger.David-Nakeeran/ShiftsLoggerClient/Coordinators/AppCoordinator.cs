using ShiftsLoggerClient.Display;
using ShiftsLoggerClient.Models;
using ShiftsLoggerClient.Services;
using ShiftsLoggerClient.Utilities;

namespace ShiftsLoggerClient.Coordinators;

class AppCoordinator
{
    private readonly UserInput _userInput;
    private readonly ShiftService _shiftService;
    private readonly DisplayManager _displayManager;
    private readonly EmployeeService _employeeService;
    private readonly Validation _validation;

    public AppCoordinator(UserInput userInput, ShiftService shiftService, DisplayManager displayManager, EmployeeService employeeService, Validation validation)
    {
        _userInput = userInput;
        _shiftService = shiftService;
        _displayManager = displayManager;
        _employeeService = employeeService;
        _validation = validation;
    }
    internal async Task Start()
    {
        bool isAppActive = true;

        while (isAppActive)
        {
            var userSelection = _userInput.ShowMenu();

            switch (userSelection)
            {
                case "View all employees":
                    await AllEmployees();
                    break;
                case "Create employee":
                    await CreateEmployee();
                    break;
                case "Update employee":
                    await UpdateEmployee();
                    break;
                case "Delete employee":
                    await DeleteEmployee();
                    break;
                case "View all shifts":
                    await AllShifts();
                    break;
                case "Create shift":
                    await CreateShift();
                    break;
                case "Update shift":
                    await UpdateShift();
                    break;
                case "Delete shift":
                    await DeleteShift();
                    break;
                case "Quit application":
                    isAppActive = false;
                    break;
            }
        }
    }

    internal async Task<ApiResponse<List<EmployeeDTO>>> GetAllEmployees()
    {
        var employees = await _employeeService.GetAllEmployees();
        return employees;
    }

    internal async Task<bool> AllEmployees()
    {
        var employees = await GetAllEmployees();
        if (employees.Success == false)
        {
            Console.WriteLine(employees.Message);
            _userInput.WaitForUserInput();
            return false;
        }
        if (employees.Data.Count == 0)
        {
            Console.WriteLine("No employess founds");
            _userInput.WaitForUserInput();
            return false;
        }
        _displayManager.RenderGetAllEmployeesTable(employees.Data);
        _userInput.WaitForUserInput();
        return true;
    }

    internal async Task<Dictionary<long, long>> GetKeyValuePairsEmployees()
    {
        var employees = await GetAllEmployees();
        var keyValuePairs = new Dictionary<long, long>();
        long displayId = 1;

        foreach (var employee in employees.Data)
        {
            keyValuePairs[displayId] = employee.EmployeeId;
            displayId++;
        }
        return keyValuePairs;
    }

    internal async Task<ApiResponse<EmployeeDTO>?> GetEmployee(long id)
    {
        var idPairs = await GetKeyValuePairsEmployees();
        if (!idPairs.ContainsKey(id))
        {
            _displayManager.IncorrectId();
            _userInput.WaitForUserInput();
            return null;
        }

        long employeeId = idPairs[id];

        return await _employeeService.GetEmployeeById(employeeId);
    }

    internal async Task CreateEmployee()
    {
        var input = _userInput.GetEmployeeName("Please enter employee name or enter 0 to return to main menu");
        if (input == "0") return;

        var createdShift = await _employeeService.PostEmployee(
            new EmployeeDTO
            {
                Name = input
            }
        );

        if (!createdShift.Success)
        {
            _displayManager.ShowMessage(createdShift.Message);
            _userInput.WaitForUserInput();
        }
        else
        {
            _displayManager.ShowMessage(createdShift.Message);
            _userInput.WaitForUserInput();
        }
    }

    internal async Task UpdateEmployee()
    {

        bool allEmployees = await AllEmployees();
        if (!allEmployees) return;

        var displayId = _userInput.GetId("Please enter the id of employee you wish to update or enter 0 to return to main menu");
        if (displayId == 0) return;

        if (await GetEmployee(displayId) == null)
        {
            return;
        }
        ApiResponse<EmployeeDTO> employeeObject = await GetEmployee(displayId);
        employeeObject.Data.Name = _userInput.GetEmployeeName("Please enter updated name for employee");


        var updatedEmployee = await _employeeService.UpdateEmployee(employeeObject.Data, employeeObject.Data.EmployeeId);

        if (!updatedEmployee.Success)
        {
            _displayManager.ShowMessage(updatedEmployee.Message);
            _userInput.WaitForUserInput();
        }
        else
        {
            _displayManager.ShowMessage(updatedEmployee.Message);
            _userInput.WaitForUserInput();
        }
    }

    internal async Task DeleteEmployee()
    {
        bool allEmployees = await AllEmployees();
        if (!allEmployees) return;

        var displayId = _userInput.GetId("Please enter the id of employee or enter 0 to return to main menu");
        if (displayId == 0) return;

        var idPairs = await GetKeyValuePairsEmployees();

        if (!idPairs.ContainsKey(displayId))
        {
            _displayManager.IncorrectId();
            _userInput.WaitForUserInput();
            return;
        }

        long employeeId = idPairs[displayId];

        var result = await _employeeService.DeleteEmployee(employeeId);

        if (result.Success)
        {
            _displayManager.ShowMessage(result.Message);
            _userInput.WaitForUserInput();
        }
        else
        {
            _displayManager.ShowMessage(result.Message);
            _userInput.WaitForUserInput();
        }
    }

    public async Task<ApiResponse<List<ShiftDTO>>> GetAllShifts()
    {
        var shifts = await _shiftService.GetAllShifts();
        return shifts;
    }

    internal async Task<bool> AllShifts()
    {
        var shifts = await GetAllShifts();
        if (shifts.Success == false)
        {
            Console.WriteLine(shifts.Message);
            _userInput.WaitForUserInput();
            return false;
        }
        if (shifts.Data.Count == 0)
        {
            Console.WriteLine("No shifts founds");
            _userInput.WaitForUserInput();
            return false;
        }
        _displayManager.RenderGetAllShiftsTable(shifts.Data);
        _userInput.WaitForUserInput();
        return true;
    }

    internal async Task<Dictionary<long, long>> GetKeyValuePairsShifts()
    {
        var shifts = await GetAllShifts();
        var keyValuePairs = new Dictionary<long, long>();
        long displayId = 1;

        foreach (var shift in shifts.Data)
        {
            keyValuePairs[displayId] = shift.ShiftId;
            displayId++;
        }
        return keyValuePairs;
    }

    internal async Task<ApiResponse<ShiftDTO>> GetShift(long id)
    {
        var idPairs = await GetKeyValuePairsShifts();
        if (!idPairs.ContainsKey(id))
        {
            _displayManager.IncorrectId();
            _userInput.WaitForUserInput();
            return new ApiResponse<ShiftDTO> { }
            ;
        }

        long shiftId = idPairs[id];

        return await _shiftService.GetShiftById(shiftId);
    }

    internal async Task CreateShift()
    {
        bool allEmployees = await AllEmployees();
        if (!allEmployees) return;
        var displayId = _userInput.GetId("Please enter the id of employee you wish to make a shift for or enter 0 to return to main menu");
        if (displayId == 0) return;

        var employeeObject = await GetEmployee(displayId);
        if (employeeObject == null) return;

        var startTime = _userInput.GetShiftTimes("Please enter the start date and time in 'dd-mm-yyyy hh:mm' format, for example '24-02-2025 13:15'");
        var endTime = _userInput.GetShiftTimes("Please enter the end date and time in 'dd-mm-yyyy hh:mm' format, for example '24-02-2025 15:30' later than the start");
        while (!_validation.IsEndTimeLaterThanStartTime(startTime, endTime))
        {
            endTime = _userInput.GetShiftTimes("Please enter the end date and time in 'dd-mm-yyyy hh:mm' format, for example '24-02-2025 15:30' later than the start");
        }

        var createdShift = await _shiftService.PostShift(
            new ShiftDTO
            {
                EmployeeId = employeeObject.Data.EmployeeId,
                StartTime = startTime,
                EndTime = endTime,
                Name = employeeObject.Data.Name
            }
        );

        if (!createdShift.Success)
        {
            _displayManager.ShowMessage(createdShift.Message);
            _userInput.WaitForUserInput();
        }
        else
        {
            _displayManager.ShowMessage(createdShift.Message);
            _userInput.WaitForUserInput();
        }
    }

    internal async Task UpdateShift()
    {
        bool allShifts = await AllShifts();
        if (!allShifts) return;

        var displayId = _userInput.GetId("Please enter the id of shift you wish to update or enter 0 to return to main menu");
        if (displayId == 0) return;

        if (await GetShift(displayId) == null)
        {
            return;
        }
        ApiResponse<ShiftDTO> shiftObject = await GetShift(displayId);

        var startTime = _userInput.GetShiftTimes("Please enter the start date and time in 'dd-mm-yyyy hh:mm' format, for example '24-02-2025 13:15'");
        var endTime = _userInput.GetShiftTimes("Please enter the end date and time in 'dd-mm-yyyy hh:mm' format, for example '24-02-2025 15:30' later than the start");
        while (!_validation.IsEndTimeLaterThanStartTime(startTime, endTime))
        {
            endTime = _userInput.GetShiftTimes("Please enter the end date and time in 'dd-mm-yyyy hh:mm' format, for example '24-02-2025 15:30' later than the start");
        }

        shiftObject.Data.StartTime = startTime;
        shiftObject.Data.EndTime = endTime;

        var updatedShift = await _shiftService.UpdateShift(shiftObject.Data, shiftObject.Data.ShiftId);

        if (!updatedShift.Success)
        {
            _displayManager.ShowMessage(updatedShift.Message);
            _userInput.WaitForUserInput();
        }
        else
        {
            _displayManager.ShowMessage(updatedShift.Message);
            _userInput.WaitForUserInput();
        }
    }

    internal async Task DeleteShift()
    {
        bool allShifts = await AllShifts();
        if (!allShifts) return;

        var displayId = _userInput.GetId("Please enter the id of shift or enter 0 to return to main menu");
        if (displayId == 0) return;

        var idPairs = await GetKeyValuePairsShifts();

        if (!idPairs.ContainsKey(displayId))
        {
            _displayManager.IncorrectId();
            _userInput.WaitForUserInput();
            return;
        }

        long shiftId = idPairs[displayId];

        var result = await _shiftService.DeleteEmployee(shiftId);

        if (result.Success)
        {
            _displayManager.ShowMessage(result.Message);
            _userInput.WaitForUserInput();
        }
        else
        {
            _displayManager.ShowMessage(result.Message);
            _userInput.WaitForUserInput();
        }
    }
}