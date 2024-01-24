using System.Reflection.Metadata;
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
                var employeeTask = Employees.GetEmployee(Convert.ToInt32(input));
                MainUI.DisplayUIMessage("Loading ...");
                try
                {
                    var employeeData = await employeeTask;
                    AdminUser = employeeData?.Admin; 
                    UserId = employeeData?.EmployeeId;
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
            MainUI.DisplayUserMenu();  //Change to admin menu
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
                    // Employee1();
                    break;
                case(ConsoleKey.D5):
                    // Employee2();
                    break;
                case(ConsoleKey.D6):
                    // Employee3();
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
        MainUI.DisplayUIMessage("Loading ...");
        var shiftList = shiftTask.Result;  //Error Handling
        MainUI.DisplayList(shiftList);
        Console.ReadKey(false);
    }

    public void StartShift()
    {
        var newShift = new Shift(DateTime.UtcNow, null);
        var shiftTask = Shifts.PutShift(UserId, newShift);
        MainUI.DisplayUIMessage("Loading ...");
        var status = shiftTask.Result; //Error Handling
        Console.WriteLine(status);
        Console.ReadKey(false);
    }

    public void FinishShift()
    {
        var shiftTask = Shifts.PatchShift(UserId, DateTime.UtcNow);
        MainUI.DisplayUIMessage("Loading ...");
        try
        {
            var status = shiftTask.Result; //Error Handling
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        Console.WriteLine("Press any key to continue.\n");
        Console.ReadKey(false);
    }
}