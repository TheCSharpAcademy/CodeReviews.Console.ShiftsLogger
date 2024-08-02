using Server.Repositories.Interfaces;
using Server.Services.Interfaces;
using Shared;
using Shared.Enums;

namespace Server.Services;

public class EmployeeService : Service<Employee>, IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    public EmployeeService(IRepository<Employee> repository, IEmployeeRepository employeeRepository) : base(repository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<List<Employee>> GetEmployeesByShift(ShiftClassification classification)
    {
        return await _employeeRepository.GetEmployeesByShiftClassification(classification);
    }
}
