using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftLogger_Shared.Arashi256.Models
{
    public class WorkerShiftOutputDto
    {
        [Key]
        public int Id { get; set; }

        public int? DisplayId { get; set; }

        [Required]
        public int WorkerId { get; set; }

        [Required]
        public DateTime ShiftStart { get; set; }

        [Required]
        public DateTime ShiftEnd { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        public string? DisplayShiftStart { get; set; } = string.Empty;

        public string? DisplayShiftEnd { get; set; } = string.Empty;

        public string? DisplayDuration { get; set; } = string.Empty;

        // Navigation Property
        [ForeignKey("WorkerId"), Required]
        public WorkerOutputDto? Worker { get; set; }
    }
}