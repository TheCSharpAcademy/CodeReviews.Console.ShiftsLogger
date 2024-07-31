using Server.Models;
using Server.Models.Dtos;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;

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

    public async Task<EmployeeShift> GetEmployeeShiftAsync(int employeeId, int shiftId)
    {
        var emplopyeeShift = await _employeeShiftRepository.GetEmployeeShiftByIds(employeeId, shiftId)
        ?? throw new NullReferenceException("Employee-shift not found");
        return emplopyeeShift;
    }

    public async Task<object> GetLateEmployeesForShiftAsync(int shiftId)
    {
        var lateEmployees = await _employeeShiftRepository.GetLateEmployeesForShiftAsync(shiftId);
        if (lateEmployees.Count == 0)
        {
            return new { Message = "No late employees found for this shift." };
        }
        return lateEmployees;
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