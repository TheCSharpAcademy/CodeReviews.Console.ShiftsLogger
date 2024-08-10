using Shared;
using Shared.Enums;

namespace Server.Services.Interfaces;

public interface IEmployeeService : IService<Employee>
{
    Task<List<Employee>> GetEmployeesByShift(ShiftClassification classification);
}
