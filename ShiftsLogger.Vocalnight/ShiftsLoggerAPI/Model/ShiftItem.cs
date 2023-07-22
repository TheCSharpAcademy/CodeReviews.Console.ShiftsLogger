using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.Model
{
    public class ShiftItem
    {
        public long Id { get; set; }
        public int Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Duration { get; set; }

        [Required]
        public int EmployeeId { get; set; }
    }
}
