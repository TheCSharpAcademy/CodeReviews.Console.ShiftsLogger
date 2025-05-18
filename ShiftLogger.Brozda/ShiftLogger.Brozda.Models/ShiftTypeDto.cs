namespace ShiftLogger.Brozda.Models
{
    /// <summary>
    /// Represents a data transfer object for an shift type.
    /// </summary>
    public class ShiftTypeDto
    {
        public int Id { get; set; }
        public int DisplayId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}