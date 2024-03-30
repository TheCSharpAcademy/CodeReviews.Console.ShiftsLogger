using System.ComponentModel.DataAnnotations;

namespace ShiftLogger.Models;

public class Employee
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }

}
