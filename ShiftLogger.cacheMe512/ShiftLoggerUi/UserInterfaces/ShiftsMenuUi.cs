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
            Utilities.DisplayMessage("No worker selected. Returning to menu...", "red");
            Console.ReadKey();
            return;
        }

        Console.Clear();
        AnsiConsole.MarkupLine($"Creating a shift for {selectedWorker.FirstName} {selectedWorker.LastName}\n");

        DateTime startDate = UserInput.GetDateTimeInput("Enter shift start date and time (YYYY-MM-DD HH:mm):");
        DateTime endDate = UserInput.GetDateTimeInput("Enter shift end date and time (YYYY-MM-DD HH:mm):");

        if (endDate <= startDate)
        {
            Utilities.DisplayMessage("End time must be after start time.", "red");
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
            AnsiConsole.MarkupLine($"[green]Shift for {selectedWorker.FirstName} {selectedWorker.LastName} created successfully![/]");
        }
        else
        {
            Utilities.DisplayMessage("Failed to create shift.", "red");
        }

        Utilities.DisplayMessage("Press any key to continue...");
        Console.ReadKey();
    }


    public static void GetAllShifts()
    {
        var shiftService = new ShiftService();
        var shifts = shiftService.GetAllShifts();

        if (shifts.Count == 0)
        {
            Utilities.DisplayMessage("No shifts found.", "red");
        }
        else
        {
            ShowTable(shifts, new[] { "Shift ID", "First Name", "Last Name", "Start Date", "End Date" },
                s => new[] { s.ShiftId.ToString(), s.FirstName, s.LastName, s.StartDate.ToString(), s.EndDate.ToString() });
        }
    }

    public static void GetShiftsByWorker()
    {
        var selectedWorker = UserInput.GetWorkerOptionInput();
        if (selectedWorker == null)
        {
            Utilities.DisplayMessage("No worker selected. Returning to menu...", "red");
            Console.ReadKey();
            return;
        }

        var shiftService = new ShiftService();
        var shifts = shiftService.GetShiftsByWorker(selectedWorker.WorkerId);

        if (shifts.Count == 0)
        {
            AnsiConsole.MarkupLine($"No shifts found for {selectedWorker.FirstName} {selectedWorker.LastName}.");
        }
        else
        {
            ShowTable(shifts, new[] { "Shift ID", "First Name", "Last Name", "Start Date", "End Date" },
                s => new[] { s.ShiftId.ToString(), s.FirstName, s.LastName, s.StartDate.ToString(), s.EndDate.ToString() });
        }
    }

    public static void GetShiftsByDepartment()
    {
        var selectedDepartment = UserInput.GetDepartmentOptionInput();
        if (selectedDepartment == null)
        {
            Utilities.DisplayMessage("No department selected. Returning to menu...", "red");
            Console.ReadKey();
            return;
        }

        var shiftService = new ShiftService();
        var shifts = shiftService.GetShiftsByDepartment(selectedDepartment.DepartmentId);

        if (shifts.Count == 0)
        {
            AnsiConsole.MarkupLine($"No shifts found for {selectedDepartment.DepartmentName}.");
        }
        else
        {
            ShowTable(shifts, new[] { "Shift ID", "First Name", "Last Name", "Start Date", "End Date" },
                s => new[] { s.ShiftId.ToString(), s.FirstName, s.LastName, s.StartDate.ToString(), s.EndDate.ToString() });
        }
    }

    public static void UpdateShift()
    {
        Console.Clear();
        Utilities.DisplayMessage("Select a worker to update shift:", "cyan");
        var selectedWorker = UserInput.GetWorkerOptionInput();
        if (selectedWorker == null)
            return;

        var selectedShift = UserInput.GetShiftOptionInput(selectedWorker.WorkerId);
        if (selectedShift == null)
            return;

        Console.Clear();
        Utilities.DisplayMessage("Enter new shift details:", "cyan");
        DateTime startDate = UserInput.GetDateTimeInput("Enter new shift start date and time (YYYY-MM-DD HH:mm):");
        DateTime endDate = UserInput.GetDateTimeInput("Enter new shift end date and time (YYYY-MM-DD HH:mm):");

        if (endDate <= startDate)
        {
            Utilities.DisplayMessage("End time must be after start time.", "red");
            Console.ReadKey();
            return;
        }

        var shiftService = new ShiftService();
        var updatedShift = new ShiftDto { ShiftId = selectedShift.ShiftId, StartDate = startDate, EndDate = endDate, FirstName = selectedShift.FirstName, LastName = selectedShift.LastName };

        if (shiftService.UpdateShift(selectedShift.ShiftId, updatedShift))
        {
            Utilities.DisplayMessage("Shift updated successfully!", "green");
        }
        else
        {
            Utilities.DisplayMessage("Failed to update shift.", "red");
        }

        Utilities.DisplayMessage("Press any key to continue...");
        Console.ReadKey();
    }

    public static void DeleteShift()
    {
        Console.Clear();
        Utilities.DisplayMessage("Select a worker to delete shift:", "cyan");
        var selectedWorker = UserInput.GetWorkerOptionInput();
        if (selectedWorker == null)
            return;

        var selectedShift = UserInput.GetShiftOptionInput(selectedWorker.WorkerId);
        if (selectedShift == null)
            return;

        var shiftService = new ShiftService();
        if (shiftService.DeleteShift(selectedShift.ShiftId))
        {
            Utilities.DisplayMessage("Shift deleted successfully!", "green");
        }
        else
        {
            Utilities.DisplayMessage("Failed to delete shift.", "red");
        }

        Utilities.DisplayMessage("Press any key to continue...");
        Console.ReadKey();
    }
}
