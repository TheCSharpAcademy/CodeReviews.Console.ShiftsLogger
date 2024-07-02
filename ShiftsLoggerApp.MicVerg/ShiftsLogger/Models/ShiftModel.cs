namespace ShiftsLogger.Models
{
    public class ShiftModel
    {
        public int Id { get; set; }
        public string WorkerName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
