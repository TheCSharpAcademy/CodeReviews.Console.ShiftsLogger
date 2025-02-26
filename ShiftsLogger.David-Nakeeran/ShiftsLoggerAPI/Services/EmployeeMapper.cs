using ShiftsLoggerAPI.Interface;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Services;

public class EmployeeMapper : IEmployeeMapper
{

    public EmployeeDTO EmployeeToDTO(Employee employee) =>
        new EmployeeDTO
        {
            EmployeeId = employee.EmployeeId,
            Name = employee.Name,
            ShiftId = employee.Shifts.Select(x => x.ShiftId).ToList()
        };
}