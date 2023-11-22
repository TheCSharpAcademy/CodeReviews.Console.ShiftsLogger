using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.K_MYR;

public class Shift
{
    [SwaggerSchema(ReadOnly = true)]
    public int Id { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
    [SwaggerSchema(ReadOnly = true)]
    public long Duration { get; set; }
    public string? UserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }

    public ShiftDTO GetDTO()
    {
        return new ShiftDTO
        {
            Id = Id,
            StartTime = StartTime,
            EndTime = EndTime,
            Duration = TimeSpan.FromTicks(Duration)
        };
    }
}
