namespace ShiftLogger_Frontend.Arashi256.Models
{
    internal class WorkerShiftDetails
    {
        public int WorkerId { get; set; }
        public DateTime ShiftStart { get; set; } = DateTime.Now;
        public DateTime ShiftEnd { get; set; } = DateTime.Now;
        public string DisplayFirstName { get; set; } = string.Empty;
        public string DisplayLastName {  get; set; } = string.Empty;
    }
}