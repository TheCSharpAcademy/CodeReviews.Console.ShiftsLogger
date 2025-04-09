public class Shift
{
}

public class ShiftDto_WithoutId
{
    public int WorkerId;
    public DateTime StartDateTime;
    public DateTime EndDateTime;

    public ShiftDto_WithoutId(int workerId, DateTime start, DateTime end)
    {
        WorkerId = workerId;
        StartDateTime = start;
        EndDateTime = end;
    }
}