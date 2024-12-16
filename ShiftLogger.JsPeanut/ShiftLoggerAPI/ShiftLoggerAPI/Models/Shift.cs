namespace ShiftLogger.JsPeanut.Models
{
    public class Shift
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public TimeSpan WorkedTime { get; set;}
    }
}
