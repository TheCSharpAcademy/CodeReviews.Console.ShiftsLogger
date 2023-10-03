namespace ShiftsLoggerWebAPI.Models
{
    public class Shift
    {
        public int Id {  get; set; }
        public string? EmployeeName { get; set; }
        public DateTime StartOfShift {  get; set; }
        public DateTime EndOfShift { get; set; }
        public TimeSpan Duration => EndOfShift - StartOfShift;
    }
}