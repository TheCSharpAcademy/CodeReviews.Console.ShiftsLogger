namespace STUDY.ASP.ShiftLoggerTryThree.Models
{
    public class ShiftLogger
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime ClockIn { get; set; } 
        public DateTime ClockOut { get; set; }
    }
}
