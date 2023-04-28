using System.ComponentModel.DataAnnotations;
namespace sadklouds.ShiftLogger.DTOs;
public class AddShiftDto
{
    [Required(ErrorMessage = "Shift start is required")]
    public DateTime ShiftStart { get; set; }

    [Required(ErrorMessage = "Shift end is required")]
    public DateTime ShiftEnd { get; set; }

}
