namespace Api.Data.Entities;

# pragma warning disable CS1591
public class Shift {
    public int Id { get; set; }
    public required string Name { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public List<WorkerShift>? WorkerShifts { get; set; }
}
# pragma warning restore CS1591