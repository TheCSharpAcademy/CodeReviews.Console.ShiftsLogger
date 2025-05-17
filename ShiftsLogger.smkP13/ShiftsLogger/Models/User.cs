using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShiftWebApi.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }
    public bool IsActive { get; set; } = true;

    [JsonIgnore]
    internal ICollection<Shift>? Shifts { get; set; }
}