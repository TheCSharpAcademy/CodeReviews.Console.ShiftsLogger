using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShiftLoggerApi.Models;

[Index(nameof(DepartmentName), IsUnique = true)]
public class Department
{
    [Key]
    public int DepartmentId { get; set; }

    [Required]
    [MaxLength(100)]
    public string DepartmentName { get; set; } = string.Empty;

    [JsonIgnore]
    public List<Worker> Workers { get; set; } = new();
}
