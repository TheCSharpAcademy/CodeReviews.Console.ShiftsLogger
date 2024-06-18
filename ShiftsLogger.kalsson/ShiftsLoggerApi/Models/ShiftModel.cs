using System.ComponentModel.DataAnnotations;

namespace ShiftsLoggerApi.Models;

public class ShiftModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the shift.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the start time of the shift.
    /// </summary>
    /// <value>
    /// The start time of the shift.
    /// </value>
    [Required]
    public DateTime StartShift { get; set; }

    /// <summary>
    /// Gets or sets the end time of the shift.
    /// </summary>
    /// <remarks>
    /// This property represents the time at which the shift ended.
    /// </remarks>
    [Required]
    public DateTime EndShift { get; set; }

    /// <summary>
    /// Gets the duration of the shift.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the end shift time is earlier than the start shift time.</exception>
    public TimeSpan ShiftDuration
    {
        get
        {
            if (EndShift < StartShift)
            {
                throw new InvalidOperationException("End shift cannot be earlier than start shift.");
            }

            return EndShift - StartShift;
        }
    }

}