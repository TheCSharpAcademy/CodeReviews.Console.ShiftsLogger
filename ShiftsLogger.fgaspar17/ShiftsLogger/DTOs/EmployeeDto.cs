namespace ShiftsLogger;

public class EmployeeDto
{
    public int EmployeeId { get; set; }
    public required string Name { get; set; }
    public List<ShiftDto> Shifts { get; set; }
}