public class Shift
{
}

public record class ShiftRecord(int id, int workerId, DateTime startDateTime, DateTime endDateTime);

public class ShiftDto_WithoutId
{
    public int WorkerId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }

    public ShiftDto_WithoutId(int workerId, DateTime start, DateTime end)
    {
        WorkerId = workerId;
        StartDateTime = start;
        EndDateTime = end;
    }
}