using ShiftsLogger.Dejmenek.UI.Models;

namespace ShiftsLogger.Dejmenek.UI.Services.Interfaces;
public interface IEmployeeService
{
    Task AddEmployeeAsync();
    Task DeleteEmployeeAsync();
    Task UpdateEmployeeAsync();
    Task<List<EmployeeReadDTO>> GetAllEmployeesAsync();
    Task<List<ShiftReadDTO>?> GetEmployeeShiftsAsync();
}
