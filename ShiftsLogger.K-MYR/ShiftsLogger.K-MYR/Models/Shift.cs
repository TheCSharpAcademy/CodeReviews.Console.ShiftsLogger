using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.K_MYR;

public class Shift
{
    public int Id {get; set;}
    [Required]
    public DateTime StartTime {get; set;}
    [Required]
    public DateTime EndTime {get; set;}    
    public TimeSpan Duration {get;}

    public Shift(DateTime startTime, DateTime endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
        Duration = endTime - startTime;
    }
}
