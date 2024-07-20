using System.ComponentModel.DataAnnotations;

namespace WorkerShiftsAPI.Models
{
    public class Worker
    {
        public int WorkerId { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Shift> Shifts { get; set; }
    }
}