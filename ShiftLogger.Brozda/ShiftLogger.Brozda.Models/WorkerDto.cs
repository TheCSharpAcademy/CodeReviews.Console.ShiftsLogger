namespace ShiftLogger.Brozda.Models
{
    /// <summary>
    /// Represents a data transfer object for an worker.
    /// </summary>
    public class WorkerDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}