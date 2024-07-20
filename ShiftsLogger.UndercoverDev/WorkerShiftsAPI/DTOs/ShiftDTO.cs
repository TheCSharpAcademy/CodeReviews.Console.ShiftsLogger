using System.ComponentModel.DataAnnotations;

namespace WorkerShiftsAPI.DTOs
{
    public class ShiftDTO
    {
        public int ShiftId { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public int WorkerId { get; set; }
        public string WorkerName { get; set; }
    }
}