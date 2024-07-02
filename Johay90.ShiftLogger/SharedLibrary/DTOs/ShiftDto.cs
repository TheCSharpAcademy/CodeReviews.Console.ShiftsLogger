namespace SharedLibrary.DTOs
{
    public class ShiftDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Duration => Diff.ToString(@"hh\:mm\:ss");

        private TimeSpan Diff => EndTime - StartTime;
       
    }
}
