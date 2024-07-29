using ShiftsLogger.Dejmenek.UI.Data.Interfaces;
using ShiftsLogger.Dejmenek.UI.Helpers;
using ShiftsLogger.Dejmenek.UI.Models;
using ShiftsLogger.Dejmenek.UI.Services.Interfaces;
using Spectre.Console;

namespace ShiftsLogger.Dejmenek.UI.Services;
public class ShiftService : IShiftService
{
    private readonly IShiftsRepository _shiftsRepository;
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IUserInteractionService _userInteractionService;

    public ShiftService(IShiftsRepository shiftsRepository, IEmployeesRepository employeesRepository, IUserInteractionService userInteractionService)
    {
        _shiftsRepository = shiftsRepository;
        _employeesRepository = employeesRepository;
        _userInteractionService = userInteractionService;
    }

    public async Task AddShift()
    {
        var employees = await _employeesRepository.GetAllEmployeesAsync();

        if (employees is null)
        {
            return;
        }

        if (employees.Count == 0)
        {
            AnsiConsole.MarkupLine("There are currently no employees. Please add one before adding a new shift.");
            return;
        }

        int employeeId = _userInteractionService.GetEmployee(employees).Id;

        string startTime = _userInteractionService.GetDateTime();
        string endTime = _userInteractionService.GetDateTime();

        DateTime startDateTime = DateTime.Parse(startTime);
        DateTime endDateTime = DateTime.Parse(endTime);

        while (!Validation.IsChronologicalOrder(startDateTime, endDateTime))
        {
            AnsiConsole.MarkupLine("The ending time should always be after the starting time. Try again.");
            startTime = _userInteractionService.GetDateTime();
            endTime = _userInteractionService.GetDateTime();

            startDateTime = DateTime.Parse(startTime);
            endDateTime = DateTime.Parse(endTime);
        }

        var shiftToAdd = new ShiftAddDTO
        {
            EmployeeID = employeeId,
            StartTime = startDateTime,
            EndTime = endDateTime,
            Duration = CalculateDuration(startDateTime, endDateTime)

        };

        await _shiftsRepository.AddShift(shiftToAdd);
    }

    public string CalculateDuration(DateTime startTime, DateTime endTime)
    {
        TimeSpan duration = endTime.Subtract(startTime);

        return Math.Floor(duration.TotalMinutes).ToString();
    }

    public async Task DeleteShift()
    {
        var shifts = await _shiftsRepository.GetAllShifts();

        if (shifts is null)
        {
            return;
        }

        if (shifts.Count == 0)
        {
            AnsiConsole.MarkupLine("There are currently no shifts. Please add one before removing a shift.");
            return;
        }

        int shiftId = _userInteractionService.GetShift(shifts).Id;

        await _shiftsRepository.DeleteShift(shiftId);
    }

    public async Task<List<ShiftReadDTO>> GetAllShifts()
    {
        var shifts = await _shiftsRepository.GetAllShifts();

        if (shifts is null)
        {
            return [];
        }

        return Mapper.ToShiftReadDtoList(shifts);
    }

    public async Task UpdateShift()
    {
        var shifts = await _shiftsRepository.GetAllShifts();

        if (shifts is null)
        {
            return;
        }

        if (shifts.Count == 0)
        {
            AnsiConsole.MarkupLine("There are currently no shifts. Please add one before updating a shift.");
            return;
        }

        var oldShift = _userInteractionService.GetShift(shifts);

        string endTime = _userInteractionService.GetDateTime();

        DateTime endDateTime = DateTime.Parse(endTime);

        while (!Validation.IsChronologicalOrder(oldShift.StartTime, endDateTime))
        {
            AnsiConsole.MarkupLine("The ending time should always be after the starting time. Try again.");
            endTime = _userInteractionService.GetDateTime();

            endDateTime = DateTime.Parse(endTime);
        }

        var newShift = new ShiftUpdateDTO
        {
            EndTime = endDateTime,
            Duration = CalculateDuration(oldShift.StartTime, endDateTime)
        };

        await _shiftsRepository.UpdateShift(oldShift.Id, newShift);
    }
}
