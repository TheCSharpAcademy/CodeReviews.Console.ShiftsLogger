using Server.Models;

namespace Server.Repositories.Interfaces;

public interface IEmployeeShiftRepository : IRepository<EmployeeShift>
{
    Task DeleteEmployeeShift(int employeeId, int shiftId);
    Task<EmployeeShift> GetEmployeeShiftByIds(int employeeId, int shiftId);
}
