using Microsoft.VisualBasic;

namespace ShiftLoggerAPI.Models
{
    public class ShiftTimes
    {
        public long Id { get; set; }

        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        
    }
}