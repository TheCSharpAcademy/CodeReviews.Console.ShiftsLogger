using ShiftsLoggerClient.Models;
using ShiftsLoggerClient.UI;
using ShiftsLoggerClient.Helpers;
using ShiftsLoggerClient.Services;

namespace ShiftsLoggerClient.Controllers;

public class DataController
{
    private ConsoleKey? PressedKey;
    private bool RunMainMenu;
    private bool? AdminUser;
    private int? UserId;
    private string? UserName;
    private bool RunUserMenu;
    private bool RunAdminMenu;
    private readonly EmployeesWebService Employees;
    private readonly ShiftsWebService Shifts;

    public DataController(string baseAddress)
    {
        RunMainMenu = true;
        AdminUser = false;
        Employees = new(baseAddress);
        Shifts = new(baseAddress);
    }

    public async Task<bool> Login()
    {
        int? id;
        Task<HttpResponseMessage>? employeeTask;
        while(true)
        {
            id = InputController.InputLogin();
            if (id == null)
                return false;
            MainUI.LoadingMessage();
            employeeTask = Employees.GetEmployeeById(id ?? 0);
            try
            {
                var response = await employeeTask;
                if(response.IsSuccessStatusCode)
                {
                    var employee = await JsonHelper.DeserializeResponse<Employee>(response);
                    AdminUser = employee?.FirstOrDefault()?.Admin;
                    UserId = employee?.FirstOrDefault()?.EmployeeId;
                    UserName = employee?.FirstOrDefault()?.Name;
                    return true;
                }
                else
                    MainUI.DisplayUIMessage(HandleHttpResponseMessage(response));
            }
            catch
            {
                MainUI.DisplayUIMessage("There's an error with the server. Please try again later!");
            }
            Console.ReadKey(false);
        }
    }

    public void Main()
    {
        MainUI.WelcomeMessage();
        JsonHelper.AppSettings();
        while(RunMainMenu)
        {
            RunMainMenu = Login().Result;
            if(RunMainMenu && (AdminUser ?? false))
            {
                AdminMenu();
            }
            else if(RunMainMenu)
                UserMenu();
        }
        MainUI.ExitMessage();
    }

    public void UserMenu()
    {
        RunUserMenu = true;
        while(RunUserMenu)
        {
            MainUI.DisplayUserMenu(UserName);
            PressedKey = Console.ReadKey(false).Key;
            switch(PressedKey)
            {
                case(ConsoleKey.D1):
                    StartShift();
                    break;
                case(ConsoleKey.D2):
                    FinishShift();
                    break;
                case(ConsoleKey.D3):
                    ShiftHistory();
                    break;
                case(ConsoleKey.Escape):
                case(ConsoleKey.Backspace):
                    RunUserMenu = false;
                    break;
            }
        }
    }

    public void AdminMenu()
    {
        RunAdminMenu = true;
        while(RunAdminMenu)
        {
            MainUI.DisplayAdminMenu(UserName);
            PressedKey = Console.ReadKey(false).Key;
            switch(PressedKey)
            {
                case(ConsoleKey.D1):
                    StartShift();
                    break;
                case(ConsoleKey.D2):
                    FinishShift();
                    break;
                case(ConsoleKey.D3):
                    ShiftHistory();
                    break;
                case(ConsoleKey.D4):
                    EmployeeByName();
                    break;
                case(ConsoleKey.D5):
                    EmployeeByID();
                    break;
                case(ConsoleKey.D6):
                    CreateNewEmployee();
                    break;
                case(ConsoleKey.D7):
                    ModifyEmployee();
                    break;
                case(ConsoleKey.Escape):
                case(ConsoleKey.Backspace):
                    RunAdminMenu = false;
                    break;
            }
        }
    }

    public void ShiftHistory()
    {
        var shiftTask = Shifts.GetShifts(UserId);
        MainUI.LoadingMessage();
        try
        {
            var response = shiftTask.Result;
            if(response.IsSuccessStatusCode)
            {
                var shiftData = JsonHelper.DeserializeResponse<ShiftJson>(response);
                MainUI.DisplayList(shiftData.Result?.Select( p => new ShiftDto(p)).ToList());
            }
            else
                MainUI.DisplayUIMessage(HandleHttpResponseMessage(response));
        }
        catch
        {
            MainUI.DisplayUIMessage("There's an error with the server. Please try again later!");
        }
        Console.ReadKey(false);
    }

    public void StartShift()
    {
        var newShift = new ShiftJson(DateTime.UtcNow, null);
        var shiftTask = Shifts.PutShift(UserId, newShift);
        MainUI.LoadingMessage();
        try
        {
            var response = shiftTask.Result;
            MainUI.DisplayUIMessage(HandleHttpResponseMessage(response));
        }
        catch
        {
            MainUI.DisplayUIMessage("There's an error with the server. Please try again later!");
        }

        Console.ReadKey(false);
        return;
    }

    public void FinishShift()
    {
        if(!InputController.GetShiftEndConfirmation())
            return;
        var patchContent = JsonHelper.CreateShiftPatch(DateTime.UtcNow);
        var shiftTask = Shifts.PatchShift(UserId, patchContent);
        MainUI.LoadingMessage();
        try
        {
            var response = shiftTask.Result;
            MainUI.DisplayUIMessage(HandleHttpResponseMessage(response));
        }
        catch
        {
            MainUI.DisplayUIMessage("There's an error with the server. Please try again later!");
        }

        Console.ReadKey(false);
        return;
    }

    public void EmployeeByID()
    {
        var id = InputController.InputID("search");
        if (id == null)
            return;

        var employeeTask = Employees.GetEmployeeById(Convert.ToInt32(id));
        MainUI.LoadingMessage();
        try
        {
            var response = employeeTask.Result;
            if(response.IsSuccessStatusCode)
            {
                var employeeData = JsonHelper.DeserializeResponse<Employee>(response);
                MainUI.DisplayList(employeeData.Result);
            }
            else
                MainUI.DisplayUIMessage(HandleHttpResponseMessage(response));
        }
        catch
        {
            MainUI.DisplayUIMessage("There's an error with the server. Please try again later!");
        }

        Console.ReadKey(false);
        return;
    }

    public void EmployeeByName()
    {
        var name = InputController.InputName("search");
        if (name == null)
            return;
        var employeeTask = Employees.GetEmployeeByName(name);
        
        MainUI.LoadingMessage();
        try
        {
            var response = employeeTask.Result;
            if(response.IsSuccessStatusCode)
            {
                var employeeData = JsonHelper.DeserializeResponse<Employee>(response);
                MainUI.DisplayList(employeeData.Result);
            }
            else
                MainUI.DisplayUIMessage(HandleHttpResponseMessage(response));
        }
        catch
        {
            MainUI.DisplayUIMessage("There's an error with the server. Please try again later!");
        }

        Console.ReadKey(false);
        return;
    }

    public void CreateNewEmployee()
    {
        var name = InputController.InputName("create");
        if(name == null)
            return;
        var isEmployeeAdmin = InputController.InputEmployeeAdmin("create");
        var employeeTask = Employees
            .PutEmployee( new Employee( EmployeeId: 0, Name: name, Admin: isEmployeeAdmin ));
        
        MainUI.LoadingMessage();
        try
        {
            var response = employeeTask.Result;
            MainUI.DisplayUIMessage(HandleHttpResponseMessage(response));
        }
        catch
        {
            MainUI.DisplayUIMessage("There's an error with the server. Please try again later!");
            return;
        }

        Console.ReadKey(false);
        return;
    }

    public void ModifyEmployee()
    {
        var id = InputController.InputID("modify");
        if(id == null)
            return;
        var name = InputController.InputName("modify");
        if(name == null)
            return;
        var isEmployeeAdmin = InputController.InputEmployeeAdmin("modify");
        var employeeTask = Employees
            .PostEmployee(new Employee( EmployeeId: id ?? 0, Name: name, Admin: isEmployeeAdmin ));

        MainUI.LoadingMessage();
        try
        {
            var response = employeeTask.Result;
            MainUI.DisplayUIMessage(HandleHttpResponseMessage(response));
        }
        catch
        {
            MainUI.DisplayUIMessage("There's an error with the server. Please try again later!");
        }

        Console.ReadKey(false);
        return;
    }

    public static string HandleHttpResponseMessage(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return "Successful operation!";
        else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return "Operation failed. The employee/shift you entered does not exist.";
        else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            return "Operation failed. You have to start or finish your shift first.";
        return "Operation failed. Please try again later.";
    }
}