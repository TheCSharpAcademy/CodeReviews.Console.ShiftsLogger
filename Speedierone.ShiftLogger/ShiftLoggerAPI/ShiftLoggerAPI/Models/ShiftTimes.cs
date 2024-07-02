namespace ShiftLoggerAPI.Models
{
    public class ShiftTimes
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string StartDate {  get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }  
    }
}