using System.ComponentModel.DataAnnotations;

namespace WorkerShiftsAPI.DTOs
{
    public class WorkerDTO
    {
        public int WorkerId { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<ShiftDTO> Shifts { get; set; }
    }
}