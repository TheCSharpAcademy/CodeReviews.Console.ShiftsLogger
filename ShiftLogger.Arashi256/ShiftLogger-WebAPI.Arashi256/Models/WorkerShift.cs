using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShiftLogger_WebAPI.Arashi256.Models
{
    public class WorkerShift
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int WorkerId { get; set; }

        [Required]
        public DateTime ShiftStart { get; set; }

        [Required]
        public DateTime ShiftEnd { get; set; }

        [Required]
        public long DurationSeconds { get; set; } // Store duration in seconds

        // Navigation Property
        [ForeignKey("WorkerId"), Required]
        public required Worker Worker { get; set; }

        [NotMapped]
        public TimeSpan Duration
        {
            get => TimeSpan.FromSeconds(DurationSeconds);
            set => DurationSeconds = (long)value.TotalSeconds;
        }
    }
}
