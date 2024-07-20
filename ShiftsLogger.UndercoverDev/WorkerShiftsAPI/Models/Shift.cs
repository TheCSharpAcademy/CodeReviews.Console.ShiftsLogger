using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkerShiftsAPI.Models
{
    public class Shift
    {
        public int ShiftId { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public int WorkerId { get; set; }
        [ForeignKey(nameof(WorkerId))]
        public Worker Worker { get; set; }
    }
}