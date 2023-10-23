namespace ShiftsLogger.API.DTOs.Worker;
public class UpdateWorkerDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}