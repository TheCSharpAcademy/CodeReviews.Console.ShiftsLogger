using ShiftLoggerClient.ClientControllers;
using ShiftLoggerClient.ClientServices;
using ShiftLoggerClient.Models;
using Spectre.Console;
using static ShiftLoggerClient.Models.Enums;

namespace ShiftLoggerClient;

internal class UserInterface
{
    internal static void MainMenu()
    {
        bool exitProgram = false;
        while (!exitProgram)
        {

            ShowTitle();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOptions>()
                .Title("What would you like to do?")
                .AddChoices(
                    MainMenuOptions.ManagementFunctions,
                    MainMenuOptions.PunchCard,
                    MainMenuOptions.ExitProgram));

            switch (option)
            {
                case MainMenuOptions.ManagementFunctions:
                    ManagmentFunctionsMenu();
                    break;
                case MainMenuOptions.PunchCard:
                    PunchCardClientService.PunchCardPunch();
                    break;
                case MainMenuOptions.ExitProgram:
                    exitProgram = true;
                    break;
            }
        }
    }



    internal static void ManagmentFunctionsMenu()
    {
        bool exitMenu = false;
        while (!exitMenu)
        {
            ShowTitle();

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<ManagmentFunctionOptions>()
                .Title("Management Menu")
                .AddChoices(
                    ManagmentFunctionOptions.ManageEmployees,
                    ManagmentFunctionOptions.ManageShifts,
                    ManagmentFunctionOptions.Back));

            switch (option)
            {
                case ManagmentFunctionOptions.ManageEmployees:
                    ManageEmployeeMenu();
                    break;
                case ManagmentFunctionOptions.ManageShifts:
                    ManageShiftsMenu();
                    break;
                case ManagmentFunctionOptions.Back:
                    exitMenu = true;
                    break;
            }
        }
    }

    private static void ManageShiftsMenu()
    {
        bool exitMenu = false;
        while (!exitMenu)
        {
            ShowTitle();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<ManageShiftsOptions>()
                .Title("Manage Shifts")
                .AddChoices(
                    ManageShiftsOptions.ViewOpenShifts,
                    ManageShiftsOptions.ViewEditAllShiftsByEmployee,
                    ManageShiftsOptions.ViewEditAllShifts,
                    ManageShiftsOptions.Back));

            if (option == ManageShiftsOptions.Back)
            {
                exitMenu = true;
            }
            else
            {
                ShiftsMenu(option);
            }
        }
    }


    internal static async Task ManageEmployeeMenu()
    {
        bool exitMenu = false;
        while (!exitMenu)
        {
            Console.Clear();
            ShowTitle();
            List<EmployeeDTO> employees = EmployeeClientController.GetEmployeeDTOList();

            EmployeeClientService.EmployeeTable(employees);

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<ManageEmployeeOptions>()
                .Title("Manage Employees")
                .AddChoices(
                    ManageEmployeeOptions.AddEmployee,
                    ManageEmployeeOptions.UpdateEmployee,
                    ManageEmployeeOptions.DeleteEmployee,
                    ManageEmployeeOptions.Back));

            switch (option)
            {
                case ManageEmployeeOptions.AddEmployee:
                    EmployeeClientService.AddEmployeeService();
                    break;
                case ManageEmployeeOptions.UpdateEmployee:
                    EmployeeClientService.UpdateEmployeeService();
                    break;
                case ManageEmployeeOptions.DeleteEmployee:
                    EmployeeClientService.DeleteEmployeeService();
                    break;
                case ManageEmployeeOptions.Back:
                    exitMenu = true;
                    break;
            }
        }
    }

    private static void ShiftsMenu(Enums.ManageShiftsOptions option)
    {

        var exitMenu = false;
        var shifts = new List<ShiftDTO>();
        while (!exitMenu)
        {
            switch (option)
            {
                case ManageShiftsOptions.ViewOpenShifts:
                    shifts = PunchCardClientService.GetOpenShifts();
                    break;
                case ManageShiftsOptions.ViewEditAllShiftsByEmployee:
                    shifts = PunchCardClientService.GetShiftsByEmpId();
                    break;
                case ManageShiftsOptions.ViewEditAllShifts:
                    shifts = PunchCardClientService.GetAllShifts();
                    break;
                case ManageShiftsOptions.Back:
                    exitMenu = true;
                    break;
            }


            Console.Clear();
            ShowTitle();

            PunchCardClientService.ShowShifts(shifts);

            if (shifts.Count <= 0)
            {
                Console.WriteLine($"There are no Shifts in {option}.");

                var option2 = AnsiConsole.Prompt(
                new SelectionPrompt<ManageShiftOptions>()
                .Title("Manage Shifts")
                .AddChoices(
                ManageShiftOptions.NewShift,
                ManageShiftOptions.DeleteShift,
                ManageShiftOptions.Back));

                switch (option2)
                {
                    case ManageShiftOptions.NewShift:
                        PunchCardClientService.NewShiftService();
                        break;
                    case ManageShiftOptions.DeleteShift:
                        PunchCardClientService.DeleteShift();
                        break;
                    case ManageShiftOptions.Back:
                        exitMenu = true;
                        break;
                }
            }
            else
            {
                var option2 = AnsiConsole.Prompt(
                new SelectionPrompt<ManageShiftOptions>()
                .Title("Manage Shifts")
                .AddChoices(
                    ManageShiftOptions.NewShift,
                    ManageShiftOptions.UpdateShift,
                    ManageShiftOptions.DeleteShift,
                    ManageShiftOptions.Back));

                switch (option2)
                {
                    case ManageShiftOptions.NewShift:
                        PunchCardClientService.NewShiftService();
                        break;
                    case ManageShiftOptions.UpdateShift:
                        PunchCardClientService.UpdateShift();
                        break;
                    case ManageShiftOptions.DeleteShift:
                        PunchCardClientService.DeleteShift();
                        break;
                    case ManageShiftOptions.Back:
                        exitMenu = true;
                        break;
                }
            }
        }
    }


    private static void ShowTitle()
    {
        Console.Clear();
        AnsiConsole.Write(
            renderable: new FigletText("Shift Logger Client")
            .LeftJustified());
    }
}
