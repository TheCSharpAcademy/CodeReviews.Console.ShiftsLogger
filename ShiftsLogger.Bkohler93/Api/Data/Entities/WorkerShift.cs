namespace Api.Data.Entities;

public class WorkerShift{
    public int Id { get; set; }
    public int WorkerId { get; set; }
    public int ShiftId { get; set; }
    public DateTime ShiftDate { get; set; }
    public required Worker Worker { get; set; }
    public required Shift Shift { get; set; }
}