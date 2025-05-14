using System.ComponentModel.DataAnnotations;

namespace ShiftLogger.Brozda.API.Models
{
    /// <summary>
    /// Represents Worker model in the database
    /// </summary>
    public class Worker
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public ICollection<AssignedShift> AssignedShifts { get; set; } = null!;
    }
}