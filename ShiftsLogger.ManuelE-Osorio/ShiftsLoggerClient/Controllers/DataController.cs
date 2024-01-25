using Microsoft.VisualBasic;
using ShiftsLoggerClient.Models;
using ShiftsLoggerClient.UI;
using ShiftsLoggerClient.Validation;

namespace ShiftsLoggerClient.Controllers;

public class DataController
{
    private ConsoleKey? PressedKey;
    private bool RunLogin;
    private bool RunMainMenu;
    private bool? AdminUser;
    private int? UserId;
    private bool RunUserMenu;
    private bool RunAdminMenu;
    private EmployeesController Employees;
    private ShiftsController Shifts;

    public DataController()
    {
        PressedKey = null;
        RunMainMenu = true;
        RunLogin = true;
        AdminUser = false;
        Employees = new();
        Shifts = new();
    }

    public async Task<bool> Login()
    {
        string input;
        string? errorMessage = null;
        do
        {
            MainUI.DisplayLoginMenu(errorMessage);
            input = Console.ReadLine() ?? "";
            if(InputValidation.IntValidation(input))
            {
                var employeeTask = Employees.GetEmployeeById(Convert.ToInt32(input));
                MainUI.DisplayUIMessage("Loading ...");
                try
                {
                    var employeeData = await employeeTask;
                    AdminUser = employeeData?.FirstOrDefault()?.Admin; 
                    UserId = employeeData?.FirstOrDefault()?.EmployeeId;
                    return true;
                }
                catch(Exception e)
                {
                    errorMessage = e.Message;
                }
            }
            else if(input.Equals("c", StringComparison.InvariantCultureIgnoreCase))
            {
                RunLogin = false;
            }
            else
            {
                errorMessage = "Please enter a valid id number.";
            }
        }
        while(RunLogin);
        return false;
    }
    public void Main()
    {
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
    }

    public void UserMenu()
    {
        RunUserMenu = true;
        while(RunUserMenu)
        {
            MainUI.DisplayUserMenu();
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
            MainUI.DisplayAdminMenu();  //Change to admin menu
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
                    // Employee4();
                    break;
                // "7) Modify a employee\n"

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
        MainUI.DisplayUIMessage("Loading ...");
        var shiftList = shiftTask.Result?.Select(p => new ShiftDto(p)).ToList();  //Error Handling
        MainUI.DisplayList(shiftList);
        Console.ReadKey(false);
    }

    public void StartShift()
    {
        var newShift = new ShiftJson(DateTime.UtcNow, null);
        var shiftTask = Shifts.PutShift(UserId, newShift);
        MainUI.DisplayUIMessage("Loading ...");
        var status = shiftTask.Result; //Error Handling Finished?
        Console.WriteLine(status.Content.ToString());
        Console.ReadKey(false);
    }

    public void FinishShift()
    {
        var shiftTask = Shifts.PatchShift(UserId, DateTime.UtcNow);
        MainUI.DisplayUIMessage("Loading ...");
        var status = shiftTask.Result; //Error Handling Finished?
        MainUI.DisplayUIMessage(status.Content.ToString());
        Console.WriteLine("Press any key to continue.\n");
        Console.ReadKey(false);
    }

    public void EmployeeByID()
    {
        string employeeId;
        string? errorMessage = null;
        while(true)
        {
            MainUI.EnterEmployeeID(errorMessage);
            employeeId = Console.ReadLine() ?? "";
            if(InputValidation.IntValidation(employeeId))
            {
                var employeeTask = Employees.GetEmployeeById(Convert.ToInt32(employeeId));
                MainUI.DisplayUIMessage("Loading ...");
                try
                {
                    var employeeData = employeeTask.Result;
                    MainUI.DisplayList(employeeData);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.ReadKey(false);
                return;
            }
            else
            {
                errorMessage = "Please enter a valid ID";
            }
        }
    }

    public void EmployeeByName()
    {
        string employeeName;
        string? errorMessage = null;
        MainUI.EnterEmployeeID(errorMessage);
        employeeName = Console.ReadLine() ?? "";

        var employeeTask = Employees.GetEmployeeByName(employeeName);
        MainUI.DisplayUIMessage("Loading ...");
        try
        {
            var employeeData = employeeTask.Result;
            MainUI.DisplayList(employeeData);
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }

        Console.ReadKey(false);
        return;
    }

    public void CreateNewEmployee()
    {
        string name;
        string isNewEmployeeAdminString;
        bool isNewEmployeeAdmin = false;
        string? errorMessage = null;
        while(true)
        {
            MainUI.EnterEmployeeName(errorMessage);
            name = Console.ReadLine() ?? "";
            if(InputValidation.NameValidation(name))
            {
                MainUI.IsEmployeeAdmin();
                isNewEmployeeAdminString = Console.ReadLine() ?? "";
                if(isNewEmployeeAdminString.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                    isNewEmployeeAdmin = true;

                var employeeTask = Employees.PutEmployee(
                    new Employee(EmployeeId:0, Name:name, Admin:isNewEmployeeAdmin));
                MainUI.DisplayUIMessage("Loading ...");
                
                var status = employeeTask.Result;
                MainUI.DisplayUIMessage(status.Content.ToString());
                Console.ReadKey(false);
                return;
            }
            else
            {
                errorMessage = "Invalid Name.";
            }
        }
    }

    public void ModifyEmployee()
    {
        var id = InputController.InputID();
        if(id == null)
            return;
        var name = InputController.InputName();
        if(name == null)
            return;
        var isEmployeeAdmin = InputController.InputEmployeeAdmin();
        var employeeTask = Employees
            .PutEmployee(new Employee(EmployeeId: id ?? 0, Name: name, Admin: isEmployeeAdmin));
        
        MainUI.DisplayUIMessage("Loading ...");
        try
        {
            var response = employeeTask.Result;
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        Console.ReadKey(false);
        return;
    }
}