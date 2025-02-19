using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShiftLoggerApi.Models;

public class Worker
{
    [Key]
    public int WorkerId { get; set; }

    [Required]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public DateTime HireDate { get; set; }

    [Required]
    public int DepartmentId { get; set; }

    [ForeignKey(nameof(DepartmentId))]
    [JsonIgnore]
    public Department Department { get; set; } = null;

    [JsonIgnore]
    public List<Shift> Shifts { get; set; } = new();
}
