using ShiftLoggerClient.ClientControllers;
using ShiftLoggerClient.Models;
using Spectre.Console;
using static ShiftLoggerClient.Models.Enums;

namespace ShiftLoggerClient.ClientServices;

internal class PunchCardClientService
{
    internal static void PunchCardPunch()
    {
        bool exitService = false;
        while (!exitService)
        {
            Console.WriteLine("Please enter your employee ID.");
            var employeeId = AnsiConsole.Ask<int>("Employee ID:");
            var employee = EmployeeClientController.GetEmployeeDTO(employeeId);
            EmployeeClientService.SingleEmployeeTable(employee);

            var option = AnsiConsole.Prompt(
    employee.Id < 1 ?
        new SelectionPrompt<ContinueExitMenuOptions>().Title($"{employeeId} is not a valid Employee ID")
            .AddChoices(
                ContinueExitMenuOptions.ReEnter,
                ContinueExitMenuOptions.ExitMenu) :
        new SelectionPrompt<ContinueExitMenuOptions>().Title("Is this the correct user?")
            .AddChoices(
                ContinueExitMenuOptions.Continue,
                ContinueExitMenuOptions.ReEnter,
                ContinueExitMenuOptions.ExitMenu));

            switch (option)
            {
                case ContinueExitMenuOptions.Continue:
                    TypeOfPunch(employee);
                    exitService = true;
                    break;
                case ContinueExitMenuOptions.ReEnter:
                    //stay in the loop
                    break;
                case ContinueExitMenuOptions.ExitMenu:
                    exitService = true;
                    break;
            }
        }
    }

    internal static List<ShiftDTO> GetOpenShifts()
    {
        List<ShiftDTO> openShifts = PunchCardClientController.GetOpenShiftsController();
        return openShifts;
    }

    internal static List<ShiftDTO> GetShiftsByEmpId()
    {
        Console.Clear();
        bool exitMenu = false;
        List<ShiftDTO> shiftsByID = new List<ShiftDTO>();
        while (!exitMenu)
        {
            List<EmployeeDTO> employees = EmployeeClientController.GetEmployeeDTOList();

            EmployeeClientService.EmployeeTable(employees);

            Console.WriteLine("Please enter an employee ID");
            int employeeId = AnsiConsole.Ask<int>("Employee Id:");

            var employee = EmployeeClientController.GetEmployeeDTO(employeeId);
            Console.Clear();
            if (employee.Id == 0)
            {
                Console.WriteLine($"An Employee with the ID: {employeeId} Does not exist.");
                Console.ReadKey();
                Console.WriteLine("Press any key to continue.");
            }
            else
            {
                shiftsByID = PunchCardClientController.GetShiftByEmployeeId(employeeId);
                exitMenu = true;
            }
        }
        return shiftsByID;
    }

    internal static List<ShiftDTO> GetAllShifts()
    {
        List<ShiftDTO> allShifts = PunchCardClientController.GetAllShiftsController();
        return allShifts;
    }

    private static void TypeOfPunch(EmployeeDTO employee)
    {
        var activeShift = PunchCardClientController.GetOpenShift(employee);
        if (activeShift.Id < 1)
        {
            ShiftDTO newShift = new ShiftDTO
            {
                EmployeeId = employee.Id,
                ShiftStart = DateTime.Now,
                ShiftOpen = true
            };
            Console.Clear();
            var loggedShift = PunchCardClientController.CreateShift(newShift);
            ShowSingleShift(loggedShift);
            Console.WriteLine("Successfully clocked in.");
            Console.WriteLine("Press any key to continue.");

        }
        else
        {
            activeShift.ShiftEnd = DateTime.Now;
            activeShift.ShiftDuration = SetShiftDuration(activeShift);
            activeShift.ShiftOpen = false;

            var updatedShift = PunchCardClientController.UpdateShiftController(activeShift);
            ShowSingleShift(updatedShift);
            Console.WriteLine("Successfully clocked out.");
            Console.WriteLine("Press any key to continue.");
        }

        Console.ReadKey();
    }

    private static TimeSpan? SetShiftDuration(ShiftDTO activeShift)
    {
        TimeSpan maxDuration = TimeSpan.FromHours(23);
        TimeSpan calculatedDuration = (TimeSpan)(activeShift.ShiftEnd - activeShift.ShiftStart);
        activeShift.ShiftDuration = calculatedDuration <= maxDuration ? calculatedDuration : maxDuration;

        return activeShift.ShiftDuration;

    }

    private static void ShowSingleShift(ShiftDTO newShift)
    {
        var table = new Spectre.Console.Table();

        table.AddColumns("ID", "EmployeeID", "Shift Start", "Shift End", "Shift Duration", "Shift Open");

        table.AddRow($@"{newShift.Id}",
                     $@"{newShift.EmployeeId}",
                     $@"{newShift.ShiftStart}",
                     $@"{newShift.ShiftEnd}",
                     $@"{newShift.ShiftDuration}",
                     $@"{newShift.ShiftOpen}");

        AnsiConsole.Write(table);
    }

    internal static void ShowShifts(List<ShiftDTO> shifts)
    {
        var table = new Spectre.Console.Table();

        table.AddColumns("ID", "EmployeeID", "Shift Start", "Shift End", "Shift Duration", "Shift Open");
        foreach (var shift in shifts)
        {
            table.AddRow($@"{shift.Id}",
                     $@"{shift.EmployeeId}",
                     $@"{shift.ShiftStart}",
                     $@"{shift.ShiftEnd}",
                     $@"{shift.ShiftDuration}",
                     $@"{shift.ShiftOpen}");
        }

        AnsiConsole.Write(table);
    }

    internal static void UpdateShift()
    {
        Console.WriteLine("Please enter the Id for the shift you would like to update.");
        int shiftId = AnsiConsole.Ask<int>("Shift Id:");

        var shift = PunchCardClientController.GetShiftByID(shiftId);
        Console.Clear();

        if (shift.Id == 0)
        {
            Console.WriteLine($"A Shift with the ID: {shiftId} Does not exist.");
            Console.WriteLine("Press any key to continue.");
            return;
        }
        else
        {
            bool shiftUpdated = false;
            var shiftToUpdate = shift;
            while (!shiftUpdated)
            {
                ShowSingleShift(shiftToUpdate);
                shiftToUpdate = shift;
                shiftToUpdate.ShiftStart = AnsiConsole.Confirm("Update Shift Start?")
                    ? shiftToUpdate.ShiftStart = Helpers.GetDateTime()
                    : shiftToUpdate.ShiftStart;
                shiftToUpdate.ShiftEnd = AnsiConsole.Confirm("Update Shift End ?")
                    ? shiftToUpdate.ShiftEnd = Helpers.GetDateTime()
                    : shiftToUpdate.ShiftEnd;

                if (!shiftToUpdate.ShiftEnd.HasValue)
                {
                    shiftToUpdate.ShiftOpen = true;
                    PunchCardClientController.UpdateShiftController(shiftToUpdate);
                    ShowSingleShift(shiftToUpdate);
                    Console.WriteLine("Press any key to Continue.");
                    shiftUpdated = true;
                }
                else if (shiftToUpdate.ShiftStart < shiftToUpdate.ShiftEnd)
                {
                    shiftToUpdate.ShiftDuration = SetShiftDuration(shiftToUpdate);
                    shiftToUpdate.ShiftOpen = false;
                    ShiftDTO updatedShift = PunchCardClientController.UpdateShiftController(shiftToUpdate);
                    ShowSingleShift(shiftToUpdate);
                    Console.WriteLine("Press any key to Continue.");
                    shiftUpdated = true;
                }
                else if (shiftToUpdate.ShiftStart > shiftToUpdate.ShiftEnd)
                {
                    var option = AnsiConsole.Prompt(
                    new SelectionPrompt<ContinueExitMenuOptions>().Title("The shift start cannot be before the shift end")
                    .AddChoices(
                    ContinueExitMenuOptions.ReEnter,
                    ContinueExitMenuOptions.ExitMenu));

                    switch (option)
                    {
                        case ContinueExitMenuOptions.ReEnter:
                            //stay in the loop
                            break;
                        case ContinueExitMenuOptions.ExitMenu:
                            shiftUpdated = true;
                            break;

                    }
                }

            }
        }
    }

    internal static void DeleteShift()
    {
        var exitMenu = false;
        while (!exitMenu)
        {
            Console.WriteLine("Please enter the Id for the shift you would like to delete.");
            int shiftId = AnsiConsole.Ask<int>("Shift Id:");

            var shift = PunchCardClientController.GetShiftByID(shiftId);
            if (shift.Id == 0)
            {
                Console.WriteLine($"A shift with the ID of {shiftId} does not exist");
                Console.WriteLine("Press any key to continue.");
            }
            else
            {
                ShowSingleShift(shift);

                var option = AnsiConsole.Prompt(
                       new SelectionPrompt<ContinueExitMenuOptions>().Title("Is this the correct shift?")
                       .AddChoices(
                       ContinueExitMenuOptions.Continue,
                       ContinueExitMenuOptions.ReEnter,
                       ContinueExitMenuOptions.ExitMenu));

                switch (option)
                {
                    case ContinueExitMenuOptions.Continue:
                        PunchCardClientController.DeleteShiftController(shift);
                        Console.WriteLine("The Shift has been deleted.");
                        exitMenu = true;
                        Console.ReadKey();
                        break;
                    case ContinueExitMenuOptions.ReEnter:
                        //stay in the loop
                        break;
                    case ContinueExitMenuOptions.ExitMenu:
                        exitMenu = true;
                        break;

                }
            }

        }
    }

    internal static void NewShiftService()
    {
        var shiftToCreate = new ShiftDTO();
        bool employeeIdValid = false;

        while (!employeeIdValid)
        {
            List<EmployeeDTO> employees = EmployeeClientController.GetEmployeeDTOList();

            EmployeeClientService.EmployeeTable(employees);

            Console.WriteLine("What employee would you like to create a a time card for?");
            shiftToCreate.EmployeeId = AnsiConsole.Ask<int>("Employee Id:");

            employeeIdValid = Helpers.ValidateEmployeeID(shiftToCreate.EmployeeId);
        }

        bool timesValid = false;

        while (!timesValid)
        {
            Console.WriteLine("Please enter a start time for this shift.");

            shiftToCreate.ShiftStart = Helpers.GetDateTime();

            shiftToCreate.ShiftEnd = AnsiConsole.Confirm("Update Shift End ?")
                        ? shiftToCreate.ShiftEnd = Helpers.GetDateTime()
                        : shiftToCreate.ShiftEnd;

            if (!shiftToCreate.ShiftEnd.HasValue)
            {
                shiftToCreate.ShiftOpen = true;
                PunchCardClientController.CreateShift(shiftToCreate);
                ShowSingleShift(shiftToCreate);
                Console.WriteLine("Press any key to Continue.");
                Console.ReadKey();
                timesValid = true;
            }
            else if (shiftToCreate.ShiftStart < shiftToCreate.ShiftEnd)
            {
                shiftToCreate.ShiftDuration = SetShiftDuration(shiftToCreate);
                shiftToCreate.ShiftOpen = false;
                ShiftDTO updatedShift = PunchCardClientController.CreateShift(shiftToCreate);
                ShowSingleShift(shiftToCreate);
                Console.WriteLine("Press any key to Continue.");
                Console.ReadKey();
                timesValid = true;
            }
            else if (shiftToCreate.ShiftStart > shiftToCreate.ShiftEnd)
            {
                var option = AnsiConsole.Prompt(
                new SelectionPrompt<ContinueExitMenuOptions>().Title("The shift start cannot be before the shift end")
                .AddChoices(
                ContinueExitMenuOptions.ReEnter,
                ContinueExitMenuOptions.ExitMenu));

                switch (option)
                {
                    case ContinueExitMenuOptions.ReEnter:
                        //stay in the loop
                        break;
                    case ContinueExitMenuOptions.ExitMenu:
                        timesValid = true;
                        break;

                }
            }
        }
    }
}

