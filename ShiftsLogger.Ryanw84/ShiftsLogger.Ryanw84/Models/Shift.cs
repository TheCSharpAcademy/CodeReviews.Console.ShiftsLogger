using System;

namespace ShiftsLogger.Ryanw84.Models;

public class Shift
{
    public int Id { get; set; }
    public string ShiftName { get; set; } = string.Empty; 
    public DateTimeOffset Date { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public TimeSpan Duration => EndTime - StartTime;
    public DateTimeOffset CreatedAt { get; set; } 
    public DateTimeOffset UpdatedAt { get; set; } 

    public int WorkerId { get; set; }
    public int LocationId { get; set; }
    public virtual Worker? Worker { get; set; } 
    public virtual Location? Location { get; set; } 
}
