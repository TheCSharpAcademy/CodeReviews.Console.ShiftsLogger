namespace sadklouds.ShiftLogger.DTOs;
public class GetShiftDto
{
    public int Id { get; set; }
    public DateTime ShiftStart { get; set; }
    public DateTime ShiftEnd { get; set; }
    public TimeSpan Duration { get; set; }
}
