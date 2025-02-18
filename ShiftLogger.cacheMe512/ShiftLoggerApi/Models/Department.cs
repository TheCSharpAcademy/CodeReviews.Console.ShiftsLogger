using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShiftLoggerApi.Models;

[Index(nameof(DepartmentName), IsUnique = true)]
public class Department
{
    [Key]
    public int DepartmentId { get; set; }

    [Required]
    [MaxLength(100)]
    public string DepartmentName { get; set; } = string.Empty;

    public List<Worker> Workers { get; set; } = new();
}
