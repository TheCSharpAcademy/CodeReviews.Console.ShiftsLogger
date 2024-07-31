namespace Api.Data.Entities;

public class Shift {
    public int Id { get; set; }
    public required string Name { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public required List<WorkerShift> WorkerShifts { get; set; }
}