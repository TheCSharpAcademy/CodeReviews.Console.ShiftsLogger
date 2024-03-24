
namespace ShiftsLogger.frockett.UI.Dtos;

public class EmployeeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ShiftDto>? Shifts { get; set; }
}
