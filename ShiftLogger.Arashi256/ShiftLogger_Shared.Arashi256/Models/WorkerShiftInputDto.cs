using System.ComponentModel.DataAnnotations;

namespace ShiftLogger_Shared.Arashi256.Models
{
    public class WorkerShiftInputDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int WorkerId { get; set; }

        [Required]
        public string? ShiftStart { get; set; } = string.Empty;

        [Required]
        public string? ShiftEnd { get; set; } = string.Empty;
    }
}
