using ShiftLoggerUi.Services;
using Spectre.Console;
using ShiftLoggerUi.DTOs;
using static ShiftLoggerUi.Utilities;
using static ShiftLoggerUi.Enums;

namespace ShiftLoggerUi.UserInterfaces;

class ShiftsMenuUi
{
    static internal void ShiftsMenu()
    {
        var isShiftMenuRunning = true;
        while (isShiftMenuRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
            new SelectionPrompt<ShiftMenu>()
            .Title("Shifts Menu")
            .AddChoices(
                ShiftMenu.AddShift,
                ShiftMenu.ViewAllShifts,
                ShiftMenu.ViewShiftsByWorker,
                ShiftMenu.ViewShiftsByDepartment,
                ShiftMenu.UpdateShift,
                ShiftMenu.DeleteShift,
                ShiftMenu.GoBack));

            switch (option)
            {
                case ShiftMenu.AddShift:
                    CreateShift();
                    break;
                case ShiftMenu.ViewAllShifts:
                    GetAllShifts();
                    break;
                case ShiftMenu.ViewShiftsByWorker:
                    GetShiftsByWorker();
                    break;
                case ShiftMenu.ViewShiftsByDepartment:
                    GetShiftsByDepartment();
                    break;
                case ShiftMenu.UpdateShift:
                    UpdateShift();
                    break;
                case ShiftMenu.DeleteShift:
                    DeleteShift();
                    break;
                case ShiftMenu.GoBack:
                    isShiftMenuRunning = false;
                    break;
            }
        }
    }

    public static void CreateShift()
    {
        Console.Clear();

        var selectedWorker = UserInput.GetWorkerOptionInput();
        if (selectedWorker == null)
        {
            Console.WriteLine("No worker selected. Returning to menu...");
            Console.ReadKey();
            return;
        }

        Console.Clear();
        Console.WriteLine($"Creating a shift for {selectedWorker.FirstName} {selectedWorker.LastName}\n");

        DateTime startDate = UserInput.GetDateTimeInput("Enter shift start date and time (YYYY-MM-DD HH:mm):");
        DateTime endDate = UserInput.GetDateTimeInput("Enter shift end date and time (YYYY-MM-DD HH:mm):");

        if (endDate <= startDate)
        {
            Console.WriteLine("End time must be after start time.");
            Console.ReadKey();
            return;
        }

        var shift = new ShiftDto
        {
            StartDate = startDate,
            EndDate = endDate,
            WorkerId = selectedWorker.WorkerId
        };

        var shiftService = new ShiftService();
        var createdShift = shiftService.CreateShift(shift);

        if (createdShift != null)
        {
            Console.WriteLine($"Shift for {selectedWorker.FirstName} {selectedWorker.LastName} created successfully!");
        }
        else
        {
            Console.WriteLine("Failed to create shift.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }


    public static void GetAllShifts()
    {
        var shiftService = new ShiftService();
        var shifts = shiftService.GetAllShifts();

        if (shifts.Count == 0)
        {
            Console.WriteLine("No shifts found.");
        }
        else
        {
            ShowTable(shifts, new[] { "Shift ID", "Worker ID", "Start Date", "End Date" },
                s => new[] { s.ShiftId.ToString(), s.WorkerId.ToString(), s.StartDate.ToString(), s.EndDate.ToString() });
        }
    }

    public static void GetShiftsByWorker()
    {
        int workerId = UserInput.GetIntInput("Enter Worker ID:");
        var shiftService = new ShiftService();
        var shifts = shiftService.GetShiftsByWorker(workerId);

        if (shifts.Count == 0)
        {
            Console.WriteLine("No shifts found for this worker.");
        }
        else
        {
            ShowTable(shifts, new[] { "Shift ID", "Start Date", "End Date" },
                s => new[] { s.ShiftId.ToString(), s.StartDate.ToString(), s.EndDate.ToString() });
        }
    }

    public static void GetShiftsByDepartment()
    {
        int departmentId = UserInput.GetIntInput("Enter Department ID:");
        var shiftService = new ShiftService();
        var shifts = shiftService.GetShiftsByDepartment(departmentId);

        if (shifts.Count == 0)
        {
            Console.WriteLine("No shifts found for this department.");
        }
        else
        {
            ShowTable(shifts, new[] { "Shift ID", "Worker ID", "Start Date", "End Date" },
                s => new[] { s.ShiftId.ToString(), s.WorkerId.ToString(), s.StartDate.ToString(), s.EndDate.ToString() });
        }
    }

    public static void UpdateShift()
    {
        Console.Clear();
        Console.WriteLine("Select a worker to update shift:");
        var selectedWorker = UserInput.GetWorkerOptionInput();
        if (selectedWorker == null)
            return;

        var selectedShift = UserInput.GetShiftOptionInput(selectedWorker.WorkerId);
        if (selectedShift == null)
            return;

        Console.Clear();
        Console.WriteLine("Enter new shift details:");
        DateTime startDate = UserInput.GetDateTimeInput("Enter new shift start date and time (YYYY-MM-DD HH:mm):");
        DateTime endDate = UserInput.GetDateTimeInput("Enter new shift end date and time (YYYY-MM-DD HH:mm):");

        if (endDate <= startDate)
        {
            Console.WriteLine("End time must be after start time.");
            Console.ReadKey();
            return;
        }

        var shiftService = new ShiftService();
        var updatedShift = new ShiftDto { ShiftId = selectedShift.ShiftId, StartDate = startDate, EndDate = endDate };

        if (shiftService.UpdateShift(selectedShift.ShiftId, updatedShift))
        {
            Console.WriteLine("Shift updated successfully!");
        }
        else
        {
            Console.WriteLine("Failed to update shift.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public static void DeleteShift()
    {
        Console.Clear();
        Console.WriteLine("Select a worker to delete shift:");
        var selectedWorker = UserInput.GetWorkerOptionInput();
        if (selectedWorker == null)
            return;

        var selectedShift = UserInput.GetShiftOptionInput(selectedWorker.WorkerId);
        if (selectedShift == null)
            return;

        var shiftService = new ShiftService();
        if (shiftService.DeleteShift(selectedShift.ShiftId))
        {
            Console.WriteLine("Shift deleted successfully!");
        }
        else
        {
            Console.WriteLine("Failed to delete shift.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
