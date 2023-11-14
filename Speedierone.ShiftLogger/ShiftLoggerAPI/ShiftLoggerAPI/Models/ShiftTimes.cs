namespace ShiftLoggerAPI.Models
{
    public class ShiftTimes
    {
        public long Id { get; set; }
        public DateTime StartDatetime { get; set; }

        public DateTime EndDatetime { get; set; }

        public string? Name { get; set; }
    }
}