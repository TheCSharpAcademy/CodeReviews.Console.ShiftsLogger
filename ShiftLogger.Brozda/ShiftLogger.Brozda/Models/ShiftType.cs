namespace ShiftLogger.Brozda.API.Models
{
    /// <summary>
    /// Represents ShiftType model in the database
    /// </summary>
    public class ShiftType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}