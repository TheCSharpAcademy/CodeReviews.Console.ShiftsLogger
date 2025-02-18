using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftLoggerApi.Models;

public class Shift
{
    [Key]
    public int ShiftId {  get; set; }

    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public int WorkerId { get; set; }

    [ForeignKey(nameof(WorkerId))]
    public Worker Worker { get; set; } = null;
}
