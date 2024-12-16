using System.ComponentModel.DataAnnotations;

namespace ShiftsUI.Models;

internal class Shift
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string Duration { get; set; }
}
