

using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Interface;

public interface IEmployeeMapper
{
    EmployeeDTO EmployeeToDTO(Employee employee);
}