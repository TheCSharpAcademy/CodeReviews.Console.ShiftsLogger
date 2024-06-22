using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftsLoggerApi.Models;

public class ShiftModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the shift.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the employee.
    /// </summary>
    [Required]
    [MaxLength(50)]
    [Column(TypeName = "nvarchar(50)")]
    public string EmployeeName { get; set; }

    /// <summary>
    /// Gets or sets the start time of the shift.
    /// </summary>
    /// <value>
    /// The start time of the shift.
    /// </value>
    [Required]
    public DateTime StartOfShift { get; set; }

    /// <summary>
    /// Gets or sets the end time of the shift.
    /// </summary>
    /// <remarks>
    /// This property represents the time at which the shift ended.
    /// </remarks>
    [Required]
    public DateTime EndOfShift { get; set; }

    /// <summary>
    /// Gets the duration of the shift.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the end shift time is earlier than the start shift time.</exception>
    public TimeSpan ShiftDuration
    {
        get
        {
            if (EndOfShift < StartOfShift)
            {
                throw new InvalidOperationException("End shift cannot be earlier than start shift.");
            }

            return EndOfShift - StartOfShift;
        }
    }

}