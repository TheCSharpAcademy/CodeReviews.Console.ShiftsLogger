using ShiftsLogger.frockett.API.Models;

namespace ShiftsLogger.frockett.API.DTOs;

public class EmployeeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ShiftDto>? Shifts { get; set; }
}
