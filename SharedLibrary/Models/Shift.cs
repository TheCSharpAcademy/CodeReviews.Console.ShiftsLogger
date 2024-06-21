using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SharedLibrary.Models
{
    public class Shift
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public int EmployeeId { get; set; }

        // Navigation property
        [JsonIgnore]
        public Employee Employee { get; set; }
    }
}
