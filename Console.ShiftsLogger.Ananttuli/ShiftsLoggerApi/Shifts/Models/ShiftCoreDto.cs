namespace ShiftsLoggerApi.Shifts.Models;

public class ShiftCoreDto
{
    public int ShiftId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public ShiftCoreDto(
        int shiftId,
        DateTime startTime,
        DateTime endTime
    )
    {
        ShiftId = shiftId;
        StartTime = startTime;
        EndTime = endTime;
    }
}