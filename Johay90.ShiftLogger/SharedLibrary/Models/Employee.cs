using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(18, 100)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        // Navigation property
        public ICollection<Shift> Shifts { get; set; } = new HashSet<Shift>();
    }
}
