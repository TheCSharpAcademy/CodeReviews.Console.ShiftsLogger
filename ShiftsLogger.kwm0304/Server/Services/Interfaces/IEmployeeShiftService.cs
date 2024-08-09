using Shared;
using Server.Models.Dtos;

namespace Server.Services.Interfaces;

public interface IEmployeeShiftService : IService<EmployeeShift>
{
    Task<EmployeeShift> CreateEmployeeShiftAsync(EmployeeShiftDto employeeShiftDto);
    Task<EmployeeShift> GetEmployeeShiftAsync(int employeeId, int shiftId);

    Task<EmployeeShift> UpdateEmployeeShiftAsync(int employeeId, int shiftId, EmployeeShiftDto employeeShiftDto);
    Task DeleteEmployeeShiftAsync(int employeeId, int shiftId);
    Task<object> GetLateEmployeesForShiftAsync(int shiftId);
    Task<List<EmployeeShift>> GetEmployeesForShiftAsync(int shiftId);
    Task<List<EmployeeShift>> GetShiftsForEmployeeAsync(int employeeId);
}
