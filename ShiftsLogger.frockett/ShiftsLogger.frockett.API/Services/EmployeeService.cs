using ShiftsLogger.frockett.API.DTOs;
using ShiftsLogger.frockett.API.Repositories;

namespace ShiftsLogger.frockett.API.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        this.employeeRepository = employeeRepository;
    }

    public async Task<EmployeeDto>
}
