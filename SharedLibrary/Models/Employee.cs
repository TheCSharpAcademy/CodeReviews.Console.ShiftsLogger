using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
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
