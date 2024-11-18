using System.ComponentModel.DataAnnotations;

namespace ShiftLogger.Mefdev.ShiftLoggerAPI.Models;

public class WorkerShift
{
    [Key]
    public int Id { get; set; }
    public string EmployeeName { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}