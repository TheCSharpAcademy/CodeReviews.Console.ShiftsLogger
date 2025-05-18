namespace ShiftLogger.Brozda.Models
{
    /// <summary>
    /// Represents a data transfer object for an assigned shift.
    /// </summary>
    public class AssignedShiftDto
    {
        public int Id { get; set; }

        public int WorkerId { get; set; }

        public int ShiftTypeId { get; set; }

        public DateTime Date { get; set; }
    }
}