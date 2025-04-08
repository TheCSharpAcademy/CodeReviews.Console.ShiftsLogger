public class Shift
{
}

public class ShiftDto_WithoutId
{
    public int WorkerId;
    public DateTime StartDatetime;
    public DateTime EndDatetime;

    public ShiftDto_WithoutId(int workerId, DateTime start, DateTime end)
    {
        WorkerId = workerId;
        StartDatetime = start;
        EndDatetime = end;
    }
}