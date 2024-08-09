namespace Api.Data.Entities;

# pragma warning disable CS1591
public class Worker {
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Position { get; set; }
    public List<WorkerShift>? WorkerShifts { get; set; }
}
# pragma warning restore CS1591