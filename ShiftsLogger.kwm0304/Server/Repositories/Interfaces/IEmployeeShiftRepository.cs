using Shared;

namespace Server.Repositories.Interfaces;

public interface IEmployeeShiftRepository : IRepository<EmployeeShift>
{
    Task DeleteEmployeeShift(int employeeId, int shiftId);
    Task<EmployeeShift> GetEmployeeShiftByIds(int employeeId, int shiftId);
    Task<List<EmployeeShift>> GetLateEmployeesForShiftAsync(int shiftId);
    Task<List<EmployeeShift>> GetEmployeesForShiftAsync(int shiftId);
    Task<List<EmployeeShift>> GetShiftsForEmployeeAsync(int employeeId);
}
