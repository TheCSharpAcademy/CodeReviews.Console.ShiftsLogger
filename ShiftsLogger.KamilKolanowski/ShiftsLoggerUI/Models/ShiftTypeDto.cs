using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.KamilKolanowski.Models;

public class ShiftTypeDto
{
    [Key]
    public int ShiftTypeId { get; set; }
    public string ShiftTypeName { get; set; } = String.Empty;

    public List<ShiftDto> Shifts { get; set; }
}
