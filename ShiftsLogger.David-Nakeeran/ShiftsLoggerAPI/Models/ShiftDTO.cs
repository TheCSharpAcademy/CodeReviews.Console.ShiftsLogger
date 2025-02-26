using System.ComponentModel.DataAnnotations;

namespace ShiftsLoggerAPI.Models;

public class ShiftDTO : IValidatableObject
{
    public long ShiftId { get; set; }

    public long EmployeeId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string EmployeeName { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndTime <= StartTime)
        {
            yield return new ValidationResult(
                "EndTime must be greater than StartTime",
                new[] { nameof(EndTime) });
        }
    }
}
