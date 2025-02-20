namespace Shifts_Logger.DTOs;

public class WorkerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ShiftDto> Shifts { get; set; } = new();
}
