using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.K_MYR;

public class ShiftDTO
{
    [SwaggerSchema(ReadOnly = true)]
    public int Id { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
    [SwaggerSchema(ReadOnly = true)]
    public TimeSpan Duration { get; set; }
}
