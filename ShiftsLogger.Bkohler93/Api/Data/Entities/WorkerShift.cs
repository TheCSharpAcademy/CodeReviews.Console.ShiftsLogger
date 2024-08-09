namespace Api.Data.Entities;

# pragma warning disable CS1591
public class WorkerShift{
    public int Id { get; set; }
    public int WorkerId { get; set; }
    public int ShiftId { get; set; }
    public DateOnly ShiftDate { get; set; }
    public Worker? Worker { get; set; }
    public Shift? Shift { get; set; }
}
# pragma warning restore CS1591