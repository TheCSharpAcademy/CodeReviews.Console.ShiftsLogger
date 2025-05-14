using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.KamilKolanowski.Models;

public class ShiftType
{
    [Key]
    public int ShiftTypeId { get; set; }
    public string ShiftTypeName { get; set; } = String.Empty;

    public List<Shift> Shifts { get; set; }
}
