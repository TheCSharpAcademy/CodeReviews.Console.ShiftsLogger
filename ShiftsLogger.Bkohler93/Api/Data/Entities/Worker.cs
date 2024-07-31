namespace Api.Data.Entities;

public class Worker {
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Position { get; set; }
    public List<WorkerShift>? WorkerShifts { get; set; }
}