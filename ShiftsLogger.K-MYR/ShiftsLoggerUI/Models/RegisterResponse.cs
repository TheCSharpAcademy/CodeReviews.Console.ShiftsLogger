namespace ShiftsLoggerUI.Models
{
    public class RegisterResponse
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public Errors Errors { get; set; }
    }
}
