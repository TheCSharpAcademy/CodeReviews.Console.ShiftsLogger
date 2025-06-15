using System.ComponentModel.DataAnnotations;

namespace ConsoleFrontEnd.Models.Dtos;

public class WorkerApiRequestDto
{
    public int WorkerId { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [Phone]
    public string? PhoneNumber { get; set; }

    [EmailAddress]
    public string? Email { get; set; }
}
