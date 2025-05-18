namespace ShiftLogger.Brozda.API.Models
{
    /// <summary>
    /// Represents Assigned shift model in the database
    /// </summary>
    public class AssignedShift
    {
        public int Id { get; set; }

        public int WorkerId { get; set; }
        public Worker Worker { get; set; } = null!;
        public int ShiftTypeId { get; set; }
        public ShiftType ShiftType { get; set; } = null!;

        public DateTime Date { get; set; }
    }
}