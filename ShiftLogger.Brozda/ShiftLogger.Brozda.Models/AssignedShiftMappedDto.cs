namespace ShiftLogger.Brozda.Models
{
    /// <summary>
    /// Represents a data transfer object for an assigned shift.
    /// Worker and Shift type is mapped in oposite to <see cref="AssignedShiftDto"/>
    /// </summary>
    public class AssignedShiftMappedDto
    {
        public int Id { get; set; }

        public string WorkerName { get; set; } = null!;

        public string ShiftTypeName { get; set; } = null!;

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}