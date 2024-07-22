namespace WorkerShiftsUI.Models
{
    public class Worker
    {
        public string? WorkerId { get; set;}
        public string? Name { get; set; }
        public List<Shift>? Shifts { get; set;}
    }
}