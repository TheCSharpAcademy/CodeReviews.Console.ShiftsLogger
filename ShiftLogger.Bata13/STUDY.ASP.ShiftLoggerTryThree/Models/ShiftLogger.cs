namespace STUDY.ASP.ShiftLoggerTryThree.Models
{
    public class ShiftLogger
    {
        public int Id { get; set; }
        public string EmployeeFirstName { get; set; } = string.Empty;
        public string EmployeeLastName { get; set; } = string.Empty;
        public DateTime ClockIn { get; set; } 
        public DateTime ClockOut { get; set; }
    }
}
