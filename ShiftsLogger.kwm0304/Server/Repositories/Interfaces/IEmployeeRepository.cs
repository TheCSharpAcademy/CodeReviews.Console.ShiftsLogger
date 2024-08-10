using Shared;
using Shared.Enums;

namespace Server.Repositories.Interfaces;

public interface IEmployeeRepository :IRepository<Employee>
{
    Task<List<Employee>> GetEmployeesByShiftClassification(ShiftClassification classification);
}
