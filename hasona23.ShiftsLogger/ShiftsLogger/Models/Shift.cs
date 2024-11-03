using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShiftsLoggerAPI.Models;

public class Shift
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("WorkerId"), Required]
    public int WorkerId { get; set; }
    [JsonIgnore]
    public virtual Worker Worker { get; set; }

    [Required]
    public DateTime Start { get; set;}
    [Required]
    public DateTime End { get; set;}

    [NotMapped]
    public TimeSpan Duration
    {
        get { return End - Start; }
    }

    #pragma warning disable
    public Shift() { }
    public Shift(int id, int workerId, DateTime start, DateTime end)
    {
        Id = id;
        WorkerId = workerId;
        Start = start;
        End = end;
        if (Start > End)
            End = new DateTime(Start.Year, Start.Month, Start.Day + 1, End.Hour, End.Minute, End.Second);
    }
    public Shift(ShiftCreate shiftCreate) 
    {
        WorkerId = shiftCreate.WorkerId;
        Start = shiftCreate.Start;
        End = shiftCreate.End;
        if (Start > End)
            End = new DateTime(Start.Year,Start.Month,Start.Day+1,End.Hour,End.Minute,End.Second);
    }
}
public record ShiftUpdate(int Id,DateTime? Start,DateTime? End);

public record ShiftCreate(int WorkerId,DateTime Start,DateTime End);