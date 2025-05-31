using System.ComponentModel.DataAnnotations;

namespace HKhemanthSharma.ShiftLoggerAPI.Model.Dto
{
    public class ShiftDto
    {
        public int WorkerId { get; set; }

        [Required]
        [RegularExpression(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]$",
            ErrorMessage = "ShiftStartTime must be in HH:mm 24-hour format.")]
        public string ShiftStartTime { get; set; }

        [Required]
        [RegularExpression(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]$",
            ErrorMessage = "ShiftEndTime must be in HH:mm 24-hour format.")]
        public string ShiftEndTime { get; set; }

        [Required]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])-(0[1-9]|1[0-2])-\d{4}$",
            ErrorMessage = "ShiftDate must be in dd-MM-yyyy format.")]
        public string ShiftDate { get; set; }
    }
}
