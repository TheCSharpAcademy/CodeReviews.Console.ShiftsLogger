using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShiftLogger_WebAPI.Arashi256.Models
{
    public class Worker
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        // Navigation Property: Zero-to-many relationship with WorkerShift
        [JsonIgnore] // This will exclude WorkerShifts from being serialized in the JSON response
        public ICollection<WorkerShift> WorkerShifts { get; set; } = new List<WorkerShift>();
    }
}
