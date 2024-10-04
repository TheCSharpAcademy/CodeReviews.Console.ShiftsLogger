using System.ComponentModel.DataAnnotations;

namespace ShiftLogger_Shared.Arashi256.Models
{
    public class WorkerInputDto
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;
    }
}
