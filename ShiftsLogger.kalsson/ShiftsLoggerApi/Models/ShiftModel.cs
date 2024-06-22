using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftsLoggerApi.Models
{
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
        public DateTime StartOfShift { get; set; }

        /// <summary>
        /// Gets or sets the end time of the shift.
        /// </summary>
        public DateTime? EndOfShift { get; set; }

        /// <summary>
        /// Gets the duration of the shift.
        /// </summary>
        public TimeSpan? ShiftDuration
        {
            get
            {
                if (EndOfShift.HasValue && EndOfShift.Value >= StartOfShift)
                {
                    return EndOfShift.Value - StartOfShift;
                }
                return null;
            }
        }
    }
}