using ShiftLoggerApi.Models;

namespace ShiftLoggerApi.DTOs;

public class ShiftDto
{
    public int ShiftId { get; set; }
    public int WorkerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public ShiftDto() { }

    public ShiftDto(Shift shift)
    {
        ShiftId = shift.ShiftId;
        WorkerId = shift.WorkerId;
        FirstName = shift.Worker?.FirstName;
        LastName = shift.Worker?.LastName;
        StartDate = shift.StartDate;
        EndDate = shift.EndDate;
    }
}

