namespace WorkerShiftsUI.Models
{
    public class Shift
    {
        public int ShiftId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? WorkerId { get; set; }
    }
}