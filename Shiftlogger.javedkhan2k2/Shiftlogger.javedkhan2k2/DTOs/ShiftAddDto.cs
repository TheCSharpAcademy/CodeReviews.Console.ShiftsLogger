using Shiftlogger.Entities;

namespace Shiftlogger.DTOs;

public class ShiftAddDto
{
    public int Id { get; set; }
    public int WorkerId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }

    public Shift ToShift()
    {
        return new Shift
        {
            WorkerId = this.WorkerId,
            StartDateTime = this.StartDateTime,
            EndDateTime = this.EndDateTime
        };
    }

    public Shift ToShiftPut()
    {
        return new Shift
        {
            Id = this.Id,
            WorkerId = this.WorkerId,
            StartDateTime = this.StartDateTime,
            EndDateTime = this.EndDateTime
        };
    }
    
}

public class ShiftRequestDto
{
    public int Id { get; set; }
    public int WorkerId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }

    public string Duration {
        get 
        {
            var temp = EndDateTime.Subtract(StartDateTime);
            return $"{temp.Hours} Hours, {temp.Minutes} Minutes, {temp.Seconds} Seconds";
        }
    }
    public WorkerRequestDto Worker { get; set; }
}