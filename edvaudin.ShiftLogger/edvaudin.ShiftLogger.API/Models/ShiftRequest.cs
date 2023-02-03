using System.ComponentModel.DataAnnotations;

namespace ShiftLogger.API.Models
{
    public class ShiftRequest
    {
        [Required]
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
