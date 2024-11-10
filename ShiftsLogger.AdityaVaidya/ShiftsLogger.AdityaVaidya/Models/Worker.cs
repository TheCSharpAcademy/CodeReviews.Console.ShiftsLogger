using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.AdityaVaidya.Models;

public class Worker
{
    [Key]
    public int WorkerId { get; set; }
    public string Name { get; set; } = null!;
    public string EmailId { get; set; } = null!;
}