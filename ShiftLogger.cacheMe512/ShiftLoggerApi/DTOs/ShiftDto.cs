using ShiftLoggerApi.Models;

namespace ShiftLoggerApi.DTOs;

public class ShiftDto
{
    public int ShiftId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int WorkerId { get; set; }

    public ShiftDto() { }

    public ShiftDto(Shift shift)
    {
        ShiftId = shift.ShiftId;
        StartDate = shift.StartDate;
        EndDate = shift.EndDate;
        WorkerId = shift.WorkerId;
    }
}
