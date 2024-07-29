namespace ShiftsLogger.ukpagrace.Models
{
    public class ShiftLogDto
    {
        public long? Id { get; set; }
        public long EmployeeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
