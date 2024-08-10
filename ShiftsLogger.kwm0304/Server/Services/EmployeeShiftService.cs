using Shared;
using Server.Models.Dtos;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;
using Spectre.Console;

namespace Server.Services;

public class EmployeeShiftService : Service<EmployeeShift>, IEmployeeShiftService
{
    private readonly IEmployeeShiftRepository _employeeShiftRepository;
    public EmployeeShiftService(
        IRepository<EmployeeShift> repository,
        IEmployeeShiftRepository employeeShiftRepository
        ) : base(repository)
    {
        _employeeShiftRepository = employeeShiftRepository;
    }

    public async Task<EmployeeShift> CreateEmployeeShiftAsync(EmployeeShiftDto employeeShiftDto)
    {
        var emplopyeeShift = new EmployeeShift
        {
            EmployeeId = employeeShiftDto.EmployeeId,
            ShiftId = employeeShiftDto.ShiftId,
            ClockInTime = employeeShiftDto.ClockInTime,
            ClockOutTime = employeeShiftDto.ClockOutTime
        };
        await AddAsync(emplopyeeShift);
        return emplopyeeShift;
    }

    public async Task DeleteEmployeeShiftAsync(int employeeId, int shiftId)
    {
        var employeeShift = await GetEmployeeShiftAsync(employeeId, shiftId);
        if (employeeShift == null)
        {
            throw new NullReferenceException("No employeeshift found with this id");
        }
        else
        {
            await _employeeShiftRepository.DeleteEmployeeShift(employeeId, shiftId);
        }
    }

    public async Task<List<EmployeeShift>> GetEmployeesForShiftAsync(int shiftId)
    {
        if (shiftId <= 0)
            throw new ArgumentOutOfRangeException(nameof(shiftId), "Employee ID must be greater than zero.");
        try
        {
            var employeeShifts = await _employeeShiftRepository.GetEmployeesForShiftAsync(shiftId);
            return employeeShifts ?? ([]);
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            throw new ApplicationException("An error occurred while retrieving employee shifts.", e);
        }
    }

    public async Task<EmployeeShift> GetEmployeeShiftAsync(int employeeId, int shiftId)
    {
        var emplopyeeShift = await _employeeShiftRepository.GetEmployeeShiftByIds(employeeId, shiftId)
        ?? throw new NullReferenceException("Employee-shift not found");
        return emplopyeeShift;
    }

    public async Task<List<EmployeeShift>> GetLateEmployeesForShiftAsync(int shiftId)
    {
        try
        {
            var lateEmployees = await _employeeShiftRepository.GetLateEmployeesForShiftAsync(shiftId);
    
            return lateEmployees;
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            throw new ApplicationException("An error occurred while retrieving late employees for this shift");
        }
    }

    public async Task<List<EmployeeShift>> GetShiftsForEmployeeAsync(int employeeId)
    {
        if (employeeId <= 0)
            throw new ArgumentOutOfRangeException(nameof(employeeId), "Employee ID must be greater than zero.");
        try
        {
            var employeeShifts = await _employeeShiftRepository.GetShiftsForEmployeeAsync(employeeId);
            return employeeShifts ?? ([]);
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            throw new ApplicationException("An error occurred while retrieving employee shifts.", e);
        }
    }


    public async Task<EmployeeShift> UpdateEmployeeShiftAsync(int employeeId, int shiftId, EmployeeShiftDto employeeShiftDto)
    {
        var empShift = await _employeeShiftRepository.GetEmployeeShiftByIds(employeeId, shiftId) ?? throw new NullReferenceException("No employeeshift found with this id");
        empShift.ClockInTime = employeeShiftDto.ClockInTime;
        empShift.ClockOutTime = employeeShiftDto.ClockOutTime;
        await UpdateAsync(empShift);
        return empShift;
    }
}