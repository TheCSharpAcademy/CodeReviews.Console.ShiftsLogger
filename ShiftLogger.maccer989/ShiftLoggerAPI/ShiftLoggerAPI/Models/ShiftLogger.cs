namespace ShiftLoggerAPI.Models
{
    public class ShiftLogger
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Date { get; set; }
        public string ShiftStartTime { get; set; }
        public string ShiftEndTime { get; set; }
        public string Duration { get; set; }
    }
}
