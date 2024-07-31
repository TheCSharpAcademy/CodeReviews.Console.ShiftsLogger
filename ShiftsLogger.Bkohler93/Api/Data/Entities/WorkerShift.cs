namespace Api.Data.Entities;

public class WorkerShift{
    public int Id { get; set; }
    public int WorkerId { get; set; }
    public int ShiftId { get; set; }
    public DateOnly ShiftDate { get; set; }
    public Worker? Worker { get; set; }
    public Shift? Shift { get; set; }
}